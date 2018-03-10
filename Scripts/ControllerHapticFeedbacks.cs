using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHapticFeedbacks : MonoBehaviour {

    Vector3 temp;
    public float threshold = 0.005f;
	// Use this for initialization
	void Start () {
        temp = gameObject.transform.position;
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("touchable")||other.CompareTag("standard"))
        {
            
            
            // get the input from left controller
            SteamVR_TrackedObject trackedObject = GetComponent<SteamVR_TrackedObject>();
            var device = SteamVR_Controller.Input((int)trackedObject.index);

            //device.TriggerHapticPulse(500);
            
            float distance = Vector3.Distance(temp, gameObject.transform.position);
            if (distance> threshold) {
                device.TriggerHapticPulse(500);
            }
            
        }

    }

    // Update is called once per frame
    void Update () {
        temp = gameObject.transform.position;
    }
}
