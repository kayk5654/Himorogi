using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMaterials : MonoBehaviour {
	public bool start = false;
	Renderer rend;
	GameObject player;
	Vector3 playerPos;
	float lerpVal;


	// Use this for initialization
	void Start () {

        player = GameObject.Find ("FPSController");
        //player = GameObject.Find("Camera (eye)");

        if (start) {
			lerpVal = 1;
		} else {
			lerpVal = 0;
		}

	}

	void setShaderAlpha(float newAlpha){
		Material mat = GetComponent<Renderer> ().material;
		Shader shader = mat.shader;

		Shader standard = Shader.Find ("Standard");
		Shader transparent = Shader.Find ("Custom/transparent");
		Shader outlines = Shader.Find ("Custom/ToonTest01");

		if (shader == standard) {
			Vector4 color = mat.GetColor ("_Color");
			color.Set (color.x, color.y, color.z, newAlpha);
			mat.SetColor ("_Color", color);
		} else if (shader == transparent) {
			mat.SetFloat ("_AlphaMul", newAlpha);
		} else if (shader == outlines) {
			Vector4 color = mat.GetColor ("_OutlineColor");
			color.Set (color.x, color.y, color.z, newAlpha);
			mat.SetColor("_OutlineColor", color);
		} else {
			Vector4 color = mat.GetColor ("_Color");
			color.Set (color.x, color.y, color.z, newAlpha);
			mat.SetColor ("_Color", color);
		}
	}

	float targetAlpha () {
		Material mat = GetComponent<Renderer> ().material;
		Shader shader = mat.shader;

		Shader standard = Shader.Find ("Standard");
		Shader transparent = Shader.Find ("Custom/transparent");
		Shader outlines = Shader.Find ("Custom/ToonTest01");

		float targetAlpha = 1;

		if (shader == standard) {
			targetAlpha = 1.0f;
		} else if (shader == transparent) {
			targetAlpha = 1.8f;
		} else if (shader == outlines) {
			targetAlpha = 1.0f;
		}

		return targetAlpha;
	}

	void fadeIn (float val) {
		
		float alpha = Mathf.Lerp (0, targetAlpha(), val);
		setShaderAlpha (alpha);
	}

	void fadeOut(float val) {
		float alpha = Mathf.Lerp (targetAlpha(), 0, val);
		setShaderAlpha (alpha);
	}

	// Update is called once per frame
	void Update () {
		playerPos = player.transform.position;
        
		float distX = Mathf.Abs (playerPos.x);
		float distZ = Mathf.Abs (playerPos.z);

		lerpVal = Mathf.InverseLerp (18.0f, 14.0f, Mathf.Max(distX, distZ));
		if (start) {
			fadeOut (lerpVal);
		} else {
			fadeIn (lerpVal);
		}

	}


}
