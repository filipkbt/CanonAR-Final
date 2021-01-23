using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEventData
{

    public string eventName;

    public GameObject obj;

    public CustomEventData()
    {

    }

    public CustomEventData(string name, GameObject gameObject)
    {
        eventName = name;
        obj = gameObject;
    }
}
