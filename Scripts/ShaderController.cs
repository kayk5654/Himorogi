using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour {
	public Color colorStart = Color.magenta;
	public Color colorEnd = Color.cyan;
	public float duration = 10.0f;
	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().material.SetColor ("_BaseColor", colorStart);
	}
	void Update() {
		float lerp = Mathf.PingPong (Time.time, duration) / duration;
		GetComponent<MeshRenderer> ().material.SetColor("_BaseColor", Color.Lerp (colorStart, colorEnd, lerp));
	}

}
