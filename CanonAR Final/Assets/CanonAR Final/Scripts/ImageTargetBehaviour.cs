using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTargetBehaviour : EventDrivenObject, ITrackableEventHandler
{
    private GameObject imageTargetCanvas;
    private GameObject gameObjectContainer;
    //private GameObject featuredOrbCanvas;

    private TrackableBehaviour mTrackableBehaviour;

    void Awake() // this is where we register our events
    {
        RegisterEvent("onGameStart", OnGameStart);
        RegisterEvent("onGameOver", OnGameOver);
        RegisterEvent("onGameExit", OnGameExit);
    }

    void Start()
    {

        imageTargetCanvas = transform.Find("ImageTargetCanvas").gameObject;
        gameObjectContainer = transform.Find("GameObjectContainer").gameObject;

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        CustomEventData eventData = new CustomEventData();
        eventData.obj = transform.gameObject;
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            eventData.eventName = "onImageTargetDetected";
            EventManager.TriggerEvent(eventData);
        }
        else
        {
            eventData.eventName = "onImageTargetLost";
            EventManager.TriggerEvent(eventData);
            ActivateElements();
        }
    }

    void OnGameStart(CustomEventData data)
    {
        DeactivateElements();
    }

    void OnGameOver(CustomEventData data)
    {
        ActivateElements();
    }

    void OnGameExit(CustomEventData data)
    {
        ActivateElements();
    }


    void ActivateElements()
    {
        imageTargetCanvas.SetActive(true);
        gameObjectContainer.SetActive(true);
    }


    void DeactivateElements()
    {
        imageTargetCanvas.SetActive(false);
        gameObjectContainer.SetActive(false);
    }
}
