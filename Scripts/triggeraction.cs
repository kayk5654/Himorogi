using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggeraction : MonoBehaviour {

	public static float button;
	public int Enter = 1;
	public int Leave = 2;

	void Start ()   
	{
		GetComponent<AudioSource> ().playOnAwake = false;

    } 





	void OnTriggerEnter(Collider hit){
        if (hit.CompareTag("Player"))
        {
            button = Enter;
            GetComponent<AudioSource>().Play();
        }
	}

	void OnTriggerExit(Collider hit){
        if (hit.CompareTag("Player"))
        {
            button = Leave;
        }
	}



}


