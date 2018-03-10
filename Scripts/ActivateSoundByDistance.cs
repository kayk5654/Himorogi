using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSoundByDistance : MonoBehaviour {

    float distanceToActivate;
	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().loop = true;
        distanceToActivate = GetComponent<AudioSource>().maxDistance;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 player = GameObject.Find("FPSController").transform.position;
        float distance = Vector3.Distance(this.transform.position, player);

        if (distance < distanceToActivate)
        {
            GetComponent<AudioSource>().playOnAwake = true;
        }
        else
        {
            GetComponent<AudioSource>().playOnAwake = false;
        }
    }
}
