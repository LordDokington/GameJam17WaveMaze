using UnityEngine;
using System.Collections;
using WaveMaze;

public class PlayerController : MonoBehaviour
{
	public Material material;

	//LevelData CurrentLevelData;

	public float Influence { get { return m_influence; } }
	public float Penumbra { get { return m_penumbra; } }

	public float speed = 15f;
    public bool IsPlayerOne = true;

	float m_penumbra;
	float m_brightnessCycleTime = 4;
	float m_brightnessCycleSpeed = 8f;

	float m_influence;

	float m_charge;
	float m_chargeDecelerator;

	public float minCharge = 0.1f;

	public float defaultPenumbra = 0.15f;
	public float minPenumbra = 0.07f;

	public float expandSpeed = 20f;
	public float shrinkSpeed = 0.5f;

	public float shakeStrength = 0.01f;

	void Start () 
	{
		m_penumbra = defaultPenumbra;
		m_charge = minCharge;
		material.SetFloat ("_Radius", 0);
		material.SetFloat ("_Penumbra", defaultPenumbra);	

		spawnPlayerOne();
	}

	Vector2 NormalizedPos()
	{
		GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
		Vector2 transformedPos = ( (Vector2) (transform.position - cam.transform.position) + new Vector2 (5, 5)) / 10f;
		transformedPos.y = 1 - transformedPos.y;
		return transformedPos;
	}

	// Update is called once per frame
	void Update () 
	{
		Vector2 playerPos = NormalizedPos();
		material.SetVector( "_PlayerPos", new Vector4(playerPos.x, playerPos.y, 0, 0) );

        if (IsPlayerOne)
        {
            float x = Input.GetAxis("HorizontalP1") * speed * Time.deltaTime;
            float y = Input.GetAxis("VerticalP1") * speed * Time.deltaTime;

            transform.Translate(new Vector3(x, y, 0));
        }
        else
        {
            float x = Input.GetAxis("HorizontalP2") * speed * Time.deltaTime;
            float y = Input.GetAxis("VerticalP2") * speed * Time.deltaTime;

            transform.Translate( new Vector3(x, y, 0) );
        }

		if (Input.GetKeyUp (KeyCode.Space)) {
			ReleaseFlash (m_charge);
		}

		if (m_brightnessCycleTime <= Mathf.PI) 
		{
			if (m_brightnessCycleTime >= Mathf.PI / 2)
			{
				m_brightnessCycleTime += Time.deltaTime * shrinkSpeed;
			}
			else
			{
				m_brightnessCycleTime += Time.deltaTime * expandSpeed;
			}
			m_influence = Mathf.Sin (m_brightnessCycleTime) * m_chargeDecelerator;
			material.SetFloat ("_Radius", m_influence);
		} 
		else //if(m_brightnessCycleTime > Mathf.PI)
		{
			if (Input.GetKey (KeyCode.Space))
			{
				ChargeFlash ();	
				m_charge += 0.3f * Time.deltaTime;
			} 
			else 
			{
				RecoverFlash ();
				m_charge = minCharge;
			}
		}
	}

    public void spawnPlayerOne()
    {
        //CurrentLevelData = GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevelData;
        //Vector2 aVector = CurrentLevelData.SpornPlayer1;
        //this.transform.position = new Vector3( aVector.x, aVector.y, 0);
    }

	///////////////////////

	public void ChargeFlash()
	{
		float penumbraDecrease = 1.0f;
		m_penumbra = Mathf.Lerp (m_penumbra, minPenumbra, penumbraDecrease * Time.deltaTime);
		material.SetFloat ("_Penumbra", m_penumbra);

		float shakeAmount = ( m_penumbra / (defaultPenumbra - minPenumbra) ) * shakeStrength;
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
}
