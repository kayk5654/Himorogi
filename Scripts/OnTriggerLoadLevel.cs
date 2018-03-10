using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnTriggerLoadLevel : MonoBehaviour {

	[SerializeField] public string LevelToLoad;

	// fading image plane settings
	public Image black;
	public Animator anim;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("MainCamera") || other.CompareTag("Player")) {
            StartCoroutine (Fading ());
			/*
            // get the input from left controller
            SteamVR_TrackedObject trackedObjectL = GameObject.Find("Controller (left)").GetComponent<SteamVR_TrackedObject>();
            var deviceL = SteamVR_Controller.Input((int)trackedObjectL.index);
            // get the input from right controller
            SteamVR_TrackedObject trackedObjectR = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedObject>();
            var deviceR = SteamVR_Controller.Input((int)trackedObjectR.index);

            // add haptics on the controllers
            deviceL.TriggerHapticPulse(3999);
            deviceR.TriggerHapticPulse(3999);
            SteamVR_LoadLevel.Begin(LevelToLoad);
			*/
        }
	}

	IEnumerator Fading() {
        
        anim.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
		SceneManager.LoadScene (LevelToLoad, LoadSceneMode.Single);
	}
}
