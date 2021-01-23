using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetContainerBehaviour : EventDrivenObject {
    
    void Awake()
    {
        RegisterEvent("onGameStart", ActivateTargets);
        RegisterEvent("onGameExit", DeactivateTargets);
        RegisterEvent("onGameOver", DeactivateTargets);
        RegisterEvent("onImageTargetLost", DeactivateTargets);
    }

    void ActivateTargets(CustomEventData data)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            TargetBehaviour tb = child.gameObject.GetComponent<TargetBehaviour>();
            tb.SetUp();
        }
    }

    void DeactivateTargets(CustomEventData data)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

}
