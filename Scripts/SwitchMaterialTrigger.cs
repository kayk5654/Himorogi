using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMaterialTrigger : MonoBehaviour {
	public int endMaterial = 0;
	GameObject[] transparentObjects;
	GameObject[] outlineObjects;
	// Use this for initialization
	void Start () {
		outlineObjects = GameObject.FindGameObjectsWithTag("outline");
		transparentObjects = GameObject.FindGameObjectsWithTag("transparent");

        endMaterial = Random.Range(0, 2);
        if (endMaterial == 0)
        {
            GameObject[] standardObjects = GameObject.FindGameObjectsWithTag("standard");
            for (int i = 0; i < standardObjects.Length; i++)
            {
                // switch rendering mode to "fade"
                Material mat = standardObjects[i].GetComponent<Renderer>().material;
                mat.SetFloat("_Mode", 2.0f);
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHAMULTIPLY_ON");
                mat.renderQueue = 3000;
            }

            for (int i = 0; i < transparentObjects.Length; i++)
            {
                transparentObjects[i].SetActive(true);
            }
            for (int j = 0; j < outlineObjects.Length; j++)
            {
                outlineObjects[j].SetActive(false);
            }

        }
        else if (endMaterial == 1)
        {
            GameObject[] standardObjects = GameObject.FindGameObjectsWithTag("standard");
            for (int i = 0; i < standardObjects.Length; i++)
            {
                // switch rendering mode to "cut out"
                Material mat = standardObjects[i].GetComponent<Renderer>().material;
                mat.SetFloat("_Mode", 1.0f);
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.EnableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAMULTIPLY_ON");
                mat.renderQueue = 2450;
            }

            for (int i = 0; i < transparentObjects.Length; i++)
            {
                transparentObjects[i].SetActive(false);
            }
            for (int j = 0; j < outlineObjects.Length; j++)
            {
                outlineObjects[j].SetActive(true);
            }

            
        }
        else
        {
            GameObject[] standardObjects = GameObject.FindGameObjectsWithTag("standard");
            for (int i = 0; i < standardObjects.Length; i++)
            {
                // switch rendering mode to "fade"
                Material mat = standardObjects[i].GetComponent<Renderer>().material;
                mat.SetFloat("_Mode", 2.0f);
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHAMULTIPLY_ON");
                mat.renderQueue = 3000;
            }

            for (int i = 0; i < transparentObjects.Length; i++)
            {
                transparentObjects[i].SetActive(true);
            }
            for (int j = 0; j < outlineObjects.Length; j++)
            {
                outlineObjects[j].SetActive(false);
            }
        }
    }
	void OnTriggerEnter(Collider other){
		
		if (other.CompareTag("MainCamera") || other.CompareTag("Player"))
        {
			Debug.Log ("Enter");
			endMaterial = Random.Range (0, 2);
			if (endMaterial == 0) {
				for(int i=0; i< transparentObjects.Length; i++){
					transparentObjects[i].SetActive(true);
					outlineObjects[i].SetActive(false);
				}
			} else if (endMaterial == 1) {
				for(int j=0; j<outlineObjects.Length; j++){
					outlineObjects[j].SetActive(true);
					transparentObjects[j].SetActive(false);
				}
			}
		}
	}

	void OnTriggerExit(Collider other){
		
		if (other.CompareTag("MainCamera")){
			Debug.Log ("Exit");
			for(int i=0; i< transparentObjects.Length; i++){
				transparentObjects[i].SetActive(false);
			}
			for(int j=0; j< outlineObjects.Length; j++){
				outlineObjects[j].SetActive(false);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("materials: " + endMaterial);
	}
}
