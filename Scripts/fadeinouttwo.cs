using UnityEngine;
using System.Collections;

public class fadeinouttwo : MonoBehaviour
{

	// publically editable speed
	public float fadeDelay = 0.0f; 
	public float fadeTime = 0.5f; 
	public int Enter = 1;
	public int Leave = 2;
	public bool fadeInOnStart = false; 
	public bool fadeOutOnStart = false;
	private bool logInitialFadeSequence = false; 

	//	public float timeRemaining = 20; 


	// store colours
	private Color[] colors; 

	// allow automatic fading on the start of the scene
	IEnumerator Start ()
	{
		//yield return null; 
		yield return new WaitForSeconds (fadeDelay); 

		if (fadeInOnStart)
		{
			logInitialFadeSequence = true; 
			FadeIn (); 
		}

		if (fadeOutOnStart)
		{
			FadeOut (fadeTime); 
		}
	}




	// check the alpha value of most opaque object
	float MaxAlpha()
	{
		float maxAlpha = 0.0f; 
		Renderer[] rendererObjects = GetComponentsInChildren<Renderer>(); 
		foreach (Renderer item in rendererObjects)
		{
			
            if(item.material.shader == Shader.Find("Standard"))
            {
                maxAlpha = Mathf.Max(maxAlpha, item.material.color.a);
            } else if (item.material.shader == Shader.Find("Custom/transparent"))
            {
                maxAlpha = Mathf.Max(maxAlpha, item.material.GetFloat("_AlphaMul"));
            }
		}
		return maxAlpha; 
	}

	// fade sequence
	IEnumerator FadeSequence (float fadingOutTime)
	{
		// log fading direction, then precalculate fading speed as a multiplier
		bool fadingOut = (fadingOutTime < 0.0f);
		float fadingOutSpeed = 1.0f / fadingOutTime; 

		// grab all child objects
		Renderer[] rendererObjects = GetComponentsInChildren<Renderer>(); 
		if (colors == null)
		{
			//create a cache of colors if necessary
			colors = new Color[rendererObjects.Length]; 

			// store the original colours for all child objects
			for (int i = 0; i < rendererObjects.Length; i++)
			{
                if (rendererObjects[i].material.shader == Shader.Find("Standard"))
                {
                    colors[i] = rendererObjects[i].material.color;
                } else if (rendererObjects[i].material.shader == Shader.Find("Custom/transparent"))
                {
                    colors[i] = new Vector4(0, 0, 0, rendererObjects[i].material.GetFloat("_AlphaMul"));
                }
			}
		}

		// make all objects visible
		for (int i = 0; i < rendererObjects.Length; i++)
		{
			rendererObjects[i].enabled = true;
		}


		// get current max alpha
		float alphaValue = MaxAlpha();  


		// This is a special case for objects that are set to fade in on start. 
		// it will treat them as alpha 0, despite them not being so. 
		if (logInitialFadeSequence && !fadingOut)
		{
			alphaValue = 0.0f; 
			logInitialFadeSequence = false; 
		}

		// iterate to change alpha value 
		while ( (alphaValue >= 0.0f && fadingOut) || (alphaValue <= 1.0f && !fadingOut)) 
		{
			alphaValue += Time.deltaTime * fadingOutSpeed; 

			for (int i = 0; i < rendererObjects.Length; i++)
			{
				Color newColor = (colors != null ? colors[i] : rendererObjects[i].material.color);
				newColor.a = Mathf.Min ( newColor.a, alphaValue ); 
				newColor.a = Mathf.Clamp (newColor.a, 0.0f, 1.0f); 				
				//rendererObjects[i].material.SetColor("_Color", newColor) ; 
                if (rendererObjects[i].material.shader == Shader.Find("Custom/transparent")){
                    rendererObjects[i].material.SetFloat("_AlphaMul", newColor.a);
                } else {
                    rendererObjects[i].material.SetColor("_Color", newColor);
                }
            }

			yield return null; 
		}

		// turn objects off after fading out
		if (fadingOut)
		{
			for (int i = 0; i < rendererObjects.Length; i++)
			{
				rendererObjects[i].enabled = false; 
			}
		}



		//Debug.Log ("fade sequence end : " + fadingOut); 

	}


	void FadeIn ()
	{
		FadeIn (fadeTime); 
	}

	void FadeOut ()
	{
		FadeOut (fadeTime); 		
	}

	void FadeIn (float newFadeTime)
	{
		StopAllCoroutines(); 
		StartCoroutine("FadeSequence", newFadeTime); 
	}

	void FadeOut (float newFadeTime)
	{
		StopAllCoroutines(); 
		StartCoroutine("FadeSequence", -newFadeTime); 
	}



	void Update()
	{

		//		timeRemaining -= Time.deltaTime;
		//		checker = Mathf.FloorToInt(timeRemaining);



		if (triggeraction.button == Leave)
		{
			FadeOut();
		}
		if (triggeraction.button == Enter) 
		{
			FadeIn(); 
		}
	}




	//	void OnGUI(){
	//		if(checker > 0){

	//			GUI.Label(new Rect(100, 100, 200, 100), "Time Remaining : "+checker);
	//		}
	//		else{
	//			timeRemaining = 10;
	//		}
	//	}



}