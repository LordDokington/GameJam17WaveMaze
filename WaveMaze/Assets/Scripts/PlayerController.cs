using UnityEngine;
using System.Collections;
using WaveMaze;

public class PlayerController : MonoBehaviour
{
	public Material material;
	public int playerIndex = 1;

	public float speed = 15f;

	public float Influence { get { return m_influence * 10; } }
	public float Penumbra { get { return m_penumbra; } }

	public bool IsFlashing { get { return m_brightnessCycleTime <= Mathf.PI; } }
	public bool IsCharging { get { return Input.GetKey (playerIndex == 1 ? KeyCode.Space : KeyCode.Return); } }

	bool HasPressed { get { return Input.GetKeyDown (playerIndex == 1 ? KeyCode.Space : KeyCode.Return); } }
	bool HasReleased { get { return Input.GetKeyUp (playerIndex == 1 ? KeyCode.Space : KeyCode.Return); } }

	float m_penumbra;
	float m_brightnessCycleTime = 4;
	float m_brightnessCycleSpeed = 8f;

	float m_influence;

	Animator animator;

	PlayerController otherPlayer;

	float m_charge;
	float m_chargeDecelerator;

	public float minCharge = 0.1f;
	public float maxCharge = 2.0f;

	public float defaultPenumbra = 0.15f;
	public float minPenumbra = 0.07f;

	public float expandSpeed = 20f;
	public float shrinkSpeed = 0.5f;

	public float shakeStrength = 0.01f;

	void Start () 
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		animator = GetComponent<Animator> ();

		if (playerIndex == 2) 
		{
			animator.playbackTime = animator.recorderStopTime * 0.7f;
		}

		if (players.Length <= 1) 
		{
			otherPlayer = null;
			//Debug.Log ("Other Player null");
		}
		else 
		{
			PlayerController firstPlayer = players [0].GetComponent<PlayerController> ();
			if (firstPlayer.playerIndex != playerIndex) {
				otherPlayer = firstPlayer;
				//Debug.Log ("Other Player " + firstIndex);
			}
			else
			{
				otherPlayer = players [1].GetComponent<PlayerController> ();
				//Debug.Log ("Other Player not " + firstIndex);
			}
		}

		m_penumbra = defaultPenumbra;
		m_charge = minCharge;
		material.SetFloat ("_Radius" + playerIndex.ToString(), 0);
		material.SetFloat ("_Penumbra" + playerIndex.ToString(), defaultPenumbra);	

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
		material.SetVector( "_PlayerPos" + playerIndex.ToString(), new Vector4(playerPos.x, playerPos.y, 0, 0) );

		float x, y;
		if (playerIndex == 1)
        {
            x = Input.GetAxis("HorizontalP1") * speed * Time.deltaTime;
            y = Input.GetAxis("VerticalP1") * speed * Time.deltaTime;
        }
        else
        {
            x = Input.GetAxis("HorizontalP2") * speed * Time.deltaTime;
            y = Input.GetAxis("VerticalP2") * speed * Time.deltaTime;
        }

		transform.Translate( new Vector3(x, y, 0) );

		if (HasPressed) 
		{
			ToChargingAnimation ();
		}

		// flash if either player stopped charging or flash of other player triggered this one
		if (HasReleased) 
		{
			ReleaseFlash (m_charge);
		} else 
		{
			if (IsCharging && otherPlayer.IsFlashing) 
			{
				float playerDist = (otherPlayer.transform.position - transform.position).magnitude;
				if (playerDist < otherPlayer.Influence)
				{
					ReleaseFlash (m_charge);
				}
			}
		}

		if ( IsFlashing )
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
			material.SetFloat ("_Radius" + playerIndex.ToString(), m_influence);
		} 
		else //if( !IsFlashing )
		{
			
			if ( IsCharging )
			{
				ChargeFlash ();	
				m_charge = Mathf.Min (maxCharge, m_charge + 0.3f * Time.deltaTime);
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
		material.SetFloat ("_Penumbra" + playerIndex.ToString(), m_penumbra);

		float shakeAmount = ( m_penumbra / (defaultPenumbra - minPenumbra) ) * shakeStrength;
		material.SetFloat ( "_ShakeX" + playerIndex.ToString(), Utils.RandRange(-shakeAmount, shakeAmount) );
	}

	public void RecoverFlash()
	{
		float penumbraIncrease = 0.4f;
		m_penumbra = Mathf.Lerp (m_penumbra, defaultPenumbra, penumbraIncrease * Time.deltaTime);
		material.SetFloat ("_Penumbra" + playerIndex.ToString(), m_penumbra);
	}

	public void ReleaseFlash(float charge)
	{
		material.SetFloat ( "_ShakeX" + playerIndex.ToString(), 0f );
		m_chargeDecelerator = charge;
		if( !IsFlashing ) m_brightnessCycleTime = 0f;
	}
		
	void ToChargingAnimation()
	{
		animator.CrossFade ("charging", 0.4f);
	}

	void ToIdleAnimation()
	{
		animator.CrossFade ("idle", 0.4f);
	}
}
