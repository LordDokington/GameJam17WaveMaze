using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveMaze {

public class DarknessEffect : MonoBehaviour {

	public Material material;

	void Awake()
	{
		//Shader shader = new Shader ();
		//material = new Material( Shader.Find("Custom/DarknessEffectShader") );
        //material.hideFlags = HideFlags.DontSave;
	}

	// Use this for initialization
	void Start () 
	{
		material.SetFloat ("_Radius", 0);
		material.SetFloat ("_Penumbra", penumbra);	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_brightnessCycleTime <= Mathf.PI) 
		{
			if (m_brightnessCycleTime >= Mathf.PI / 2) {
				m_brightnessCycleTime += Time.deltaTime * 0.2f;
			}
			else 
			{
				m_brightnessCycleTime += Time.deltaTime * m_brightnessCycleSpeed;
			}
			float scale = Mathf.Sin (m_brightnessCycleTime) * m_charge;

			material.SetFloat ("_Radius", scale);
		} 

		if (Input.GetKey (KeyCode.Space)) 
		{
			ChargeFlash ();	
			m_charge += 0.3f * Time.deltaTime;
		}
		else 
		{
			RecoverFlash ();
			m_charge -= 0.6f * Time.deltaTime; 
			m_charge = Mathf.Max (0.1f, m_charge);
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			ReleaseFlash ();
		}
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

	public void ChargeFlash()
	{
		float penumbraDecrease = 1.0f;
		penumbra = Mathf.Lerp (penumbra, 0.1f, penumbraDecrease * Time.deltaTime);
		material.SetFloat ("_Penumbra", penumbra);

		float shakeAmount = (1.0f - penumbra / (defaultPenumbra - 0.1f)) * 0.05f;
		material.SetFloat ( "_ShakeX", Utils.RandRange(-shakeAmount, shakeAmount) );
	}

	public void RecoverFlash()
	{
		float penumbraIncrease = 0.4f;
		penumbra = Mathf.Lerp (penumbra, defaultPenumbra, penumbraIncrease * Time.deltaTime);
		material.SetFloat ("_Penumbra", penumbra);
	}

	public void ReleaseFlash()
	{
		if( m_brightnessCycleTime > Mathf.PI ) m_brightnessCycleTime = 0f;
	}

	float penumbra = defaultPenumbra;
	float m_brightnessCycleTime = 4;
	float m_brightnessCycleSpeed = 8f;

	float m_charge = 0.1f;

	const float defaultPenumbra = 0.3f;
}

}