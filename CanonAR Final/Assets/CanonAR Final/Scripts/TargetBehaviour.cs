using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class TargetBehaviour : MonoBehaviour
{
    public enum Axis
    {
        Vertical,
        Horizontal
    }
    public int pointValue;

    public int timeValueInSeconds = 0;

    public float speed;

    public Axis slideAxis;

    public float slideDistance;

    private Material defaultMat;
    private GameObject mainTarget;
    private Text scoreText;
    private Vector3 startPosition;
    private Vector3 positionOne;
    private Vector3 positionTwo;
    private bool ready;
    private bool showHit;

    private float hitTime;

    void Awake()
    {
        ready = false;
        mainTarget = transform.Find("Main").gameObject;
        startPosition = transform.localPosition;
    }

    public void SetUp()
    {

        if (slideAxis == Axis.Horizontal)
        {
            positionOne = new Vector3(startPosition.x - slideDistance, startPosition.y, startPosition.z);
            positionTwo = new Vector3(startPosition.x + slideDistance, startPosition.y, startPosition.z);
        }
        else
        {
            positionOne = new Vector3(startPosition.x, startPosition.y, startPosition.z - slideDistance);
            positionTwo = new Vector3(startPosition.x, startPosition.y, startPosition.z + slideDistance);
        }
        scoreText = transform.Find("TargetScoreCanvas/MainScore").gameObject.GetComponent<Text>();
        scoreText.text = pointValue.ToString();
        ready = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            float speedScaled = speed / slideDistance;
            transform.localPosition = Vector3.Lerp(positionOne, positionTwo, Mathf.PingPong(Time.time * speedScaled, 1));
        }

        if (showHit)
        {
            //set mat color
            mainTarget.GetComponent<Renderer>().material.color = Color.cyan;
            float dif = Time.time - hitTime;
            if (dif >= 0.5)
            {
                // return to default mat color
                mainTarget.GetComponent<Renderer>().material.color = Color.white;
                showHit = false;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "CannonBall")
        {
            showHit = true;
            hitTime = Time.time;
            // cannonball is destroyed by timer in case of miss
            // so if hit, just make inactive. Timer will clean up
            col.gameObject.SetActive(false);


            // Emit event
            CustomEventData data = new CustomEventData("onTargetHit", gameObject);
            EventManager.TriggerEvent(data);
        }
    }
}
