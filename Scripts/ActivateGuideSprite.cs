using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGuideSprite : MonoBehaviour {

    Sprite sprite; 
    Vector3 player;
    public float distanceToActivate = 10.0f;
    public float fadeTime = 1.0f;
    float Alpha;
    // Use this for initialization
    void Start()
    {
        
        Alpha = 0;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alpha);
    }
    
    void Update()
    {
		player = GameObject.Find("FPSController").transform.position;
        float distance = Vector3.Distance(this.transform.position, player);
        if (distance < distanceToActivate)
        {
            StartCoroutine(ShowSprite());
        }
        else
        {
            StartCoroutine(HideSprite());
        }
        
    }

    IEnumerator ShowSprite()
    {
        yield return new WaitForSeconds(1.0f);
        while (Alpha < 1.0f)
        {
            Alpha += Time.deltaTime * (1.0f / fadeTime);
        }
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alpha);
    }

    IEnumerator HideSprite()
    {
        yield return new WaitForSeconds(1.0f);
        while (Alpha > 0.0f)
        {
            Alpha -= Time.deltaTime * (1.0f / fadeTime);
        }
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alpha);
    }
}
