using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEventAggregator
{
    public class BaseClass : MonoBehaviour
    {
        private Type[] _typeCache;

        void Start()
        {
            GetListenerTypes().Each(x => EventAggregator.Register(this, x));
        }

        void OnDestroy()
        {
            GetListenerTypes().Each(x => EventAggregator.UnRegister(this, x));
        }

        IEnumerable<Type> GetListenerTypes()
        {
            if (_typeCache != null) return _typeCache;

            var types = GetType()
                        .GetInterfaces()
                        .Where(x => x.IsGenericType)
                        .Where(x => x.GetGenericTypeDefinition() == typeof(IListener<>))
                        .Select(x => x.GetGenericArguments())
                        .First();

            _typeCache = types;

            return types;
        }
    }
}
