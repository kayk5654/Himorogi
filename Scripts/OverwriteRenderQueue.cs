using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverwriteRenderQueue : MonoBehaviour
{

    public int overwrittenRednerQueue = 2001;
    private Vector3 playerPos;
    GameObject[] standardObjects;
    // Use this for initialization
    void Start()
    {
        standardObjects = GameObject.FindGameObjectsWithTag("standard");
        for (int i = 0; i < standardObjects.Length; i++)
        {
            Renderer rend = standardObjects[i].GetComponent<Renderer>();
            rend.material.renderQueue = overwrittenRednerQueue;
            rend.material.SetInt("_ZWrite", 1);
        }
        playerPos = GameObject.Find("FPSController").transform.position;

    }

    // get the position of viewpoint, and sort the render queue of all objects and particles by the disntace from the viewpoint.
    // and, keep updating.
    // use "Material.RenderQueue"
    void Update()
    {
        for (int i = 0; i < standardObjects.Length; i++)
        {
            Renderer rend = standardObjects[i].GetComponent<Renderer>();
            rend.material.renderQueue = overwrittenRednerQueue;
        }
    }
}
