using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageTargetCanvasBehaviour : MonoBehaviour, IPointerClickHandler {
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameObject obj = pointerEventData.pointerCurrentRaycast.gameObject;
        if(obj != null)
        {
            string eventName = "on" + obj.tag + "Tapped";
            CustomEventData eventData = new CustomEventData(eventName, obj);
            EventManager.TriggerEvent(eventData);
        }
    }

}
