using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMaterialsTriggerBased : MonoBehaviour {

    public bool start = false;
    public float fadeTime = 0.5f;
    bool trigger = false;
    Renderer rend;
    float lerpVal;


    // Use this for initialization
    void Start()
    {

        if (start)
        {
            lerpVal = 1.0f;
        }
        else
        {
            lerpVal = 0.0f;
        }

        setShaderAlpha(lerpVal);
    }

    void setShaderAlpha(float newAlpha)
    {
        Material mat = GetComponent<Renderer>().material;
        Shader shader = mat.shader;

        Shader standard = Shader.Find("Standard");
        Shader transparent = Shader.Find("Custom/transparent");
        Shader outlines = Shader.Find("Custom/ToonTest01");

        if (shader == standard)
        {
            Vector4 color = mat.GetColor("_Color");
            color.Set(color.x, color.y, color.z, newAlpha);
            mat.SetColor("_Color", color);
        }
        else if (shader == transparent)
        {
            mat.SetFloat("_AlphaMul", newAlpha);
        }
        else if (shader == outlines)
        {
            Vector4 color = mat.GetColor("_OutlineColor");
            color.Set(color.x, color.y, color.z, newAlpha);
            mat.SetColor("_OutlineColor", color);
        }
        else
        {
            Vector4 color = mat.GetColor("_Color");
            color.Set(color.x, color.y, color.z, newAlpha);
            mat.SetColor("_Color", color);
        }
    }

    float targetAlpha()
    {
        Material mat = GetComponent<Renderer>().material;
        Shader shader = mat.shader;

        Shader standard = Shader.Find("Standard");
        Shader transparent = Shader.Find("Custom/transparent");
        Shader outlines = Shader.Find("Custom/ToonTest01");

        float targetAlpha = 1.0f;

        if (shader == standard)
        {
            targetAlpha = 1.0f;
        }
        else if (shader == transparent)
        {
            targetAlpha = 1.8f;
        }
        else if (shader == outlines)
        {
            targetAlpha = 1.0f;
        }

        return targetAlpha;
    }

    void fadeIn(float val)
    {

        float alpha = Mathf.Lerp(0, targetAlpha(), val);
        setShaderAlpha(alpha);
    }

    void fadeOut(float val)
    {
        float alpha = Mathf.Lerp(targetAlpha(), 0, val);
        setShaderAlpha(alpha);
    }

    IEnumerator FadeSequence (float _fadeTime) {
        yield return new WaitForSeconds(0.0f);
        float fadeSpeed = 1.0f / _fadeTime;
        if (start)
        {
            lerpVal += Time.deltaTime * fadeSpeed * -1.0f;
            fadeOut(lerpVal);
            if(lerpVal == 0.0f)
            {
                start = false;
            }
        } else
        {
            lerpVal += Time.deltaTime * fadeSpeed;
            fadeIn(lerpVal);
            if (lerpVal == 1.0f)
            {
                start = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(FadeSequence(fadeTime));
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(FadeSequence(fadeTime));
    }

    
}
