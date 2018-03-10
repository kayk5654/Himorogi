using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour {
    public Sprite[] sprites;
    int sequenceNum;
    
    void Start () {
        sequenceNum = 0;
        GetComponent<SpriteRenderer>().sprite = sprites[sequenceNum];
    }
	
	
	void Update () {
        SteamVR_TrackedObject trackedObject = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedObject>();
        var device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
            if (sequenceNum >= sprites.Length - 1)
            {
                gameObject.SetActive(false);
                device.TriggerHapticPulse(1000);
            }
            else
            {
                sequenceNum += 1;
                device.TriggerHapticPulse(1000);
            }
            GetComponent<SpriteRenderer>().sprite = sprites[sequenceNum];
        }
    }
}
