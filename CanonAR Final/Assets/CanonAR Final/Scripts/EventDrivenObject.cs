using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDrivenObject : MonoBehaviour
{

    private Dictionary<string, UnityAction<CustomEventData>> eventDictionary = new Dictionary<string, UnityAction<CustomEventData>>();

    void OnEnable()
    {
        foreach (KeyValuePair<string, UnityAction<CustomEventData>> entry in eventDictionary)
        {
            EventManager.StartListening(entry.Key, entry.Value);
        }
    }

    void OnDisable()
    {
        foreach (KeyValuePair<string, UnityAction<CustomEventData>> entry in eventDictionary)
        {
            EventManager.StopListening(entry.Key, entry.Value);
        }
    }

      protected void RegisterEvent (string eventName, Action<CustomEventData> func)
    {
        UnityAction<CustomEventData> action = new UnityAction<CustomEventData>(func);
        eventDictionary.Add(eventName, action);
    }
}
