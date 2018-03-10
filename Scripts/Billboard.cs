using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	public Sprite[] sprites;
	public int sequenceNum;

	void Start(){
		sequenceNum = 0;
		GetComponent<SpriteRenderer> ().sprite = sprites[sequenceNum];
	}

	void FixedUpdate () {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        /*
        Vector3 lookAt = Quaternion.eulerAngle(transform.rotation);
        float xrot = Mathf.Clamp(lookAt.x, 0.0f, 45);
        transform.rotation = Quaternion.Euler(xrot, lookAt.y, lookAt.z);

    */
        if (sequenceNum >= sprites.Length-1){
			sequenceNum = 0;
		} else {
			sequenceNum += 1;
		}
		GetComponent<SpriteRenderer> ().sprite = sprites [sequenceNum];
	}
}
