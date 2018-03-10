using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSkybox : MonoBehaviour {

    public Material mat1;
    public Material mat2;
    public Material mat3;
    Material matOut;
    float lerpVal = 0.0f;
    GameObject player;

    // Use this for initialization
    void Start () {
        matOut = mat1;
        RenderSettings.skybox = matOut;
        //player = GameObject.Find("Camera (eye)");
		player = GameObject.Find("FPSController");
    }
    

    // Update is called once per frame
    void Update () {
        DynamicGI.UpdateEnvironment();

        Vector3 playerPos = player.transform.position;

        float distX = Mathf.Abs(playerPos.x);
        float distZ = Mathf.Abs(playerPos.z);

        lerpVal = Mathf.InverseLerp(18.0f, 14.0f, Mathf.Max(distX, distZ));

        //matOut.Lerp(mat1, mat2, lerpVal);
        
        if (Mathf.Abs(playerPos.x) < 15.5f && Mathf.Abs(playerPos.z) < 15.5f)
        {
            int matNumber = GameObject.Find("FadeManager").GetComponent < SwitchMaterialTrigger > ().endMaterial;
            if (matNumber == 0)
            {
                matOut = mat2;
                RenderSettings.fogColor = new Color(0.32f, 0.25f, 0.18f, 1.0f);
                RenderSettings.fogDensity = 0.01f;
            }
            else {
                matOut = mat3;
                RenderSettings.fogColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                RenderSettings.fogDensity = 0.045f;
            }
            
        }
        else {
            matOut = mat1;
            RenderSettings.fogColor = new Color(0.05f, 0.05f, 0.05f, 1.0f);
            RenderSettings.fogDensity = 0.045f;
        }

        RenderSettings.skybox = matOut;
	}
}
