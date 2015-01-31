using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UnityEventAggregator
{
    public static class EventAggregator
    {
        private static readonly Dictionary<Type, List<object>> _cache = new Dictionary<Type, List<object>>();

        /// <summary>
        /// Register the game object to listen for events of type T.
        /// </summary>
        /// <typeparam name="T">The type of event being listened for.</typeparam>
        /// <param name="obj"></param>
        public static void Register<T>(this object obj)
        {
            if (!_cache.ContainsKey(typeof(T))) _cache[typeof(T)] = new List<object>();
            _cache[typeof(T)].Add(obj);
        }

        /// <summary>
        /// Removes the registration for listening to events of type T.
        /// </summary>
        /// <typeparam name="T">The type of event to no longer listen for.</typeparam>
        /// <param name="obj"></param>
        public static void UnRegister<T>(this object obj)
        {
            if (!_cache.ContainsKey(typeof(T))) return;
            _cache[typeof(T)].Remove(obj);
        }

        /// <summary>
        /// Register the game object to listen for events of type T.
        /// </summary>
        /// <typeparam name="T">The type of event being listened for.</typeparam>
        /// <param name="obj"></param>
        /// <param name="listener"></param>
        public static void Register(object obj, Type listener)
        {
            if (!_cache.ContainsKey(listener))
                _cache[listener] = new List<object>();

            _cache[listener].Add(obj);
        }

        /// <summary>
        /// Removes the registration for listening to events of type T.
        /// </summary>
        /// <typeparam name="T">The type of event to no longer listen for.</typeparam>
        /// <param name="obj"></param>
        /// <param name="listener"></param>
        public static void UnRegister(object obj, Type listener)
        {
            if (!_cache.ContainsKey(listener)) return;
            _cache[listener].Remove(obj);
        }

        /// <summary>
        /// Notifies all listeners of event type T.
        /// </summary>
        /// <typeparam name="T">The type of event being notified.</typeparam>
        /// <param name="message"></param>
        public static void SendMessage<T>(T message)
        {
            if (!_cache.ContainsKey(message.GetType())) return;
            _cache[message.GetType()].Each(x => ((IListener<T>)x).Handle(message));
        }

        /// <summary>
        /// Creates the cache for objects that listen to each event
        /// </summary>
        /// <typeparam name="T">Searches through all active GameObjects for those listening for event T and auto registers them.</typeparam>
        public static void UpdateCache<T>()
        {
            var type = typeof(IListener<T>);
            var list = new List<object>();

            // Get all types of IListener<T>
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(type))
                .Each(x =>
                {
                    // Add existing unity objects that listen for event
                    GameObject.FindObjectsOfType<MonoBehaviour>()
                    .Where(t => t.GetType() == x)
                    .Each(list.Add);
                });

            _cache[typeof(T)] = list;
        }
    }
}