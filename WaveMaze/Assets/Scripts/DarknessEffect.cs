using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveMaze {

public class DarknessEffect : MonoBehaviour {

	public Material material;

	private GameObject player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		//Shader shader = new Shader ();
		//material = new Material( Shader.Find("Custom/DarknessEffectShader") );
        //material.hideFlags = HideFlags.DontSave;
	}

	// Use this for initialization
	void Start () 
	{
		m_penumbra = defaultPenumbra;
		m_charge = minCharge;
		material.SetFloat ("_Radius", 0);
		material.SetFloat ("_Penumbra", defaultPenumbra);	
	}
	
	Vector2 NormalizedPlayerPos(Vector2 playerPos)
	{
		Vector3 transformedPos =  (playerPos + new Vector2 (5, 5)) / 10f;
		transformedPos.y = 1 - transformedPos.y;
		return transformedPos;
	}

	// Update is called once per frame
	void Update () 
	{
		Vector2 playerPos = NormalizedPlayerPos (player.transform.position);
		material.SetVector( "_PlayerPos", new Vector4(playerPos.x, playerPos.y, 0, 0) );

		if (Input.GetKeyUp (KeyCode.Space)) {
			ReleaseFlash (m_charge);
		}

		if (m_brightnessCycleTime <= Mathf.PI) 
		{
			if (m_brightnessCycleTime >= Mathf.PI / 2) {
				m_brightnessCycleTime += Time.deltaTime * 0.7f;
			}
			else
			{
				m_brightnessCycleTime += Time.deltaTime * m_brightnessCycleSpeed;
			}
			float scale = Mathf.Sin (m_brightnessCycleTime) * m_chargeDecelerator;
			material.SetFloat ("_Radius", scale);
		} 
		else //if(m_brightnessCycleTime > Mathf.PI)
		{
			if (Input.GetKey (KeyCode.Space))
			{
				ChargeFlash ();	
				m_charge += 0.03f * Time.deltaTime;
			} 
			else 
			{
				RecoverFlash ();
				m_charge = minCharge;
			}
		}
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

	public void ChargeFlash()
	{
		float penumbraDecrease = 1.0f;
		m_penumbra = Mathf.Lerp (m_penumbra, 0.1f, penumbraDecrease * Time.deltaTime);
		material.SetFloat ("_Penumbra", m_penumbra);

		float shakeAmount = (1.0f - m_penumbra / (defaultPenumbra - 0.1f)) * 0.05f;
		material.SetFloat ( "_ShakeX", Utils.RandRange(-shakeAmount, shakeAmount) );
	}

	public void RecoverFlash()
	{
		float penumbraIncrease = 0.4f;
		m_penumbra = Mathf.Lerp (m_penumbra, defaultPenumbra, penumbraIncrease * Time.deltaTime);
		material.SetFloat ("_Penumbra", m_penumbra);
	}

	public void ReleaseFlash(float charge)
	{
		material.SetFloat ( "_ShakeX", 0f );
		m_chargeDecelerator = charge;
		if( m_brightnessCycleTime > Mathf.PI ) m_brightnessCycleTime = 0f;
	}

	float m_penumbra;
	float m_brightnessCycleTime = 4;
	float m_brightnessCycleSpeed = 8f;

	float m_charge;
	float m_chargeDecelerator;

	public float minCharge = 0.1f;
	public float defaultPenumbra = 0.1f;
}

}