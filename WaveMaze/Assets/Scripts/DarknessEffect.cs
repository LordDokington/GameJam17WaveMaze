using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessEffect : MonoBehaviour {

	public Material material;

	public void TriggerBrightness()
	{
		if( m_brightnessCycleTime > Mathf.PI ) m_brightnessCycleTime = 0f;
	}

	void Awake()
	{
		//Shader shader = new Shader ();
		//material = new Material( Shader.Find("Custom/DarknessEffectShader") );
        //material.hideFlags = HideFlags.DontSave;
	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (m_brightnessCycleTime <= Mathf.PI) 
		{
			m_brightnessCycleTime += Time.deltaTime * m_brightnessCycleSpeed;
			float scale = Mathf.Sin (m_brightnessCycleTime) * 0.5f;

			material.SetFloat ("_Radius", scale);
		}

		if( Input.GetKeyDown(KeyCode.Space) ) TriggerBrightness ();
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

	private float m_brightnessCycleTime = 4;
	private float m_brightnessCycleSpeed = 3f;
}
