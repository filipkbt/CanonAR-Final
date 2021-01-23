using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectContainerBehaviour : MonoBehaviour {
    public float pokeForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;

            if(Physics.Raycast(raycast, out raycastHit))
            {
                GameObject obj = raycastHit.collider.gameObject;
                string eventName = "on" + obj.tag + "Tapped";
                CustomEventData data = new CustomEventData(eventName, obj);
            }
        }
	}
}
