using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle;
using UnityEngine;

namespace JainEventAggregator
{

    public static class EventAggregator
    {
        private static Dictionary<Type,List<object>>  _cache = new Dictionary<Type, List<object>>();
        private static bool _eventRaising;


        public static void Register(this object obj, Type type)
        {
            if (!_cache.ContainsKey(type))
            {
                _cache[type] = new List<object>();
            }

            //We only want to register instance if it isn't already registered
            if (!_cache[type].Contains(obj))
            {
                _cache[type].Add(obj);
            }
            
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
            if (_eventRaising == true)
            {
                CheckCoroutineManager();
                CoroutineManager.Instance.StartCoroutine(DelayExecution(message));
                return;
            }

            _eventRaising = true;
            Debug.Log("<b><color=blue>" + typeof(T) + "</color></b>");
            if (_cache.ContainsKey(message.GetType()))
            {
                _cache[message.GetType()].ForEach(x=> NotifyListeners(x,message));
            }
            _eventRaising = false;
        }

        private static IEnumerator DelayExecution<T> (T message)
        {
            yield return null;
            RaiseEvent(message);
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

        private static void CheckCoroutineManager()
        {
            if (GameObject.Find("EventCoroutineManager") != null) return;
            var go = new GameObject();
            go.name = "EventCoroutineManager";
            go.AddComponent<CoroutineManager>();
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
