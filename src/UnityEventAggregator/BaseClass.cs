using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEventAggregator
{
    public class BaseClass : MonoBehaviour
    {
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
            return GetType()
                    .GetInterfaces()
                    .Where(x => x.IsGenericType)
                    .Where(x => x.GetGenericTypeDefinition() == typeof(IListener<>))
                    .Select(x => x.GetGenericArguments())
                    .First();
        }
    }
}
