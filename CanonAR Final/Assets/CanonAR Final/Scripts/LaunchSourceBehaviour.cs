using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSourceBehaviour : MonoBehaviour
{
    public float velocity;
    public GameObject cannonBallPrefab;
    public Transform cannonBallParent;
    public float fireRate;
    private float nextFire;
    // Use this for initialization
    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            GameObject cannonBall = (GameObject)Instantiate(
                cannonBallPrefab,
                transform.position,
                transform.rotation,
                cannonBallParent);

            GetComponent<ParticleSystem>().Emit(30);
            cannonBall.GetComponent<Rigidbody>().velocity = transform.forward * velocity;

            CustomEventData data = new CustomEventData("onCannonFired", gameObject);
            EventManager.TriggerEvent(data);

            Destroy(cannonBall, 5.0f);
        }
    }
}
