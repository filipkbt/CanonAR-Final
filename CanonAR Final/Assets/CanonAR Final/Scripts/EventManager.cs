using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

  private Dictionary <string, CustomEvent> eventDictionary;

  private static EventManager eventManager;

  public static EventManager instance {
    get {
      if (!eventManager) {
          eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

          if (!eventManager) {
              Debug.Log("There needs to be one active EventManger script on a GameObject in your scene.");
          } else {
              eventManager.Init(); 
          }
      }

      return eventManager;
    }
  }

  void Init (){
    if (eventDictionary == null) {
        eventDictionary = new Dictionary<string, CustomEvent>();
    }
  }

  public static void StartListening (string eventName, UnityAction<CustomEventData> listener) {
   CustomEvent thisEvent = null;
    if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
        thisEvent.AddListener(listener);
    } else {
        thisEvent = new CustomEvent();
        thisEvent.AddListener(listener);
        instance.eventDictionary.Add(eventName, thisEvent);
    }
  }

  public static void StopListening(string eventName, UnityAction<CustomEventData> listener) {
    if (eventManager == null) return;
        CustomEvent thisEvent = null;
    if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
        thisEvent.RemoveListener(listener);
    }
  }

  public static void TriggerEvent(CustomEventData eventData) {
        CustomEvent thisEvent = null;
    DebugLog.WriteLog ("Event Broadcasted: " + eventData.eventName);
        DebugLog.WriteLog("test");
    if (instance.eventDictionary.TryGetValue(eventData.eventName, out thisEvent)) {
        thisEvent.Invoke(eventData);
    }
  }
}