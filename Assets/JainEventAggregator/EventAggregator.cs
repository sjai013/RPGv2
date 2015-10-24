using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JainEventAggregator
{

    public static class EventAggregator
    {
        private static readonly Dictionary<Type,List<object>>  _cache = new Dictionary<Type, List<object>>();

        public static void Register(this object obj, Type type)
        {
            if (!_cache.ContainsKey(type))
            {
                _cache[type] = new List<object>();
            }

            _cache[type].Add(obj);
        }

        public static void Register<T>(this object obj)
        {
            Register(obj, typeof(T));
        }

        public static void Unregister<T>(this object obj)
        {
            Unregister(obj, typeof(T));
        }

        public static void Unregister(this object obj, Type type)
        {
            if (_cache.ContainsKey(type))
            {
                _cache[type].Remove(obj);
            }
        }

        public static void RaiseEvent<T>(T message)
        {          
            if (_cache.ContainsKey(message.GetType()))
            {
                _cache[message.GetType()].ForEach(x=> NotifyListeners(x,message));
            }
        }

        private static void NotifyListeners<T>(object listener, T message)
        {
            var x = listener as IListener<T>;

            if (x == null)
            {
                Debug.LogWarning("Could not find implementation of " + message.GetType() + " in listener " + listener);
                return;
            }
                
            x.Handle(message);
        }

        public static void RegisterAllListeners(this object obj)
        {
            var types = GetImplementedListeners(obj);
            foreach (var type in types)
            {
                Register(obj,type);
            }
        }

        public static void UnregisterAllListeners(this object obj)
        {
            var types = GetImplementedListeners(obj);
            foreach (var type in types)
            {
                Unregister(obj,type);
            }
        }

        private static IEnumerable<Type> GetImplementedListeners(object obj)
        {
            var types = obj.GetType().GetInterfaces()
                        .Where(x => x.IsGenericType)
                        .Where(x => x.GetGenericTypeDefinition() == typeof(IListener<>))
                        .Select(x => x.GetGenericArguments()).ToList();


            var t = types.ToList().Select(type => type.First()).ToList();

            return t;
        }
    }


}
