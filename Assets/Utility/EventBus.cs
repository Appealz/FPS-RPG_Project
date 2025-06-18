using System.Collections.Generic;
using System;
using UnityEngine;

public static class EventBus
{
    private static Dictionary<Type, Delegate> eventTable = new Dictionary<Type, Delegate>();
    public static void Subscribe<T>(Action<T> newMethod)
    {
        if (eventTable.TryGetValue(typeof(T), out var existMethod))
        {
            eventTable[typeof(T)] = Delegate.Combine(existMethod, newMethod);
        }
        else
        {
            eventTable[typeof(T)] = newMethod;
        }
    }
    public static void UnSubscribe<T>(Action<T> removeMethod)
    {
        if (eventTable.TryGetValue(typeof(T), out var existMethod))
        {
            var newDelegate = Delegate.Remove(existMethod, removeMethod);
            if (newDelegate == null)
            {
                eventTable.Remove(typeof(T));
            }
            else
            {
                eventTable[typeof(T)] = newDelegate;
            }
        }
    }
    public static void Publish<T>(T type)
    {
        if (eventTable.TryGetValue(typeof(T), out var method))
        {
            (method as Action<T>)?.Invoke(type);
        }
    }
}
