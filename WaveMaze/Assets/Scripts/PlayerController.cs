using UnityEngine;
using System.Collections;
using WaveMaze;

public class PlayerController : MonoBehaviour
{
	public Material material;
	public int playerIndex = 1;

	public float speed = 15f;

	private float fuseDistance = 2.0f;

	public float Influence { get { return (m_influence + 0.5f * m_penumbra) * 10; } }

	public bool IsFlashing { get { return m_brightnessCycleTime <= Mathf.PI; } }
	public bool IsCharging { get { return Input.GetKey (playerIndex == 1 ? KeyCode.Space : KeyCode.Return); } }

	public bool AreFused 
	{ 
		get { 
			float playerDistSqr = (otherPlayer.transform.position - transform.position).sqrMagnitude;
			// fusion takes place
			return (playerDistSqr < fuseDistance * fuseDistance);
		}
	}

	float DefaultPenumbra { get { return AreFused ? 2.6f * defaultPenumbra : defaultPenumbra; } }
	float MinPenumbra { get { return AreFused ? 2 * minPenumbra : minPenumbra; } }
	float PenumbraIncrease { get { return AreFused ? 1.0f : 0.4f; } }
	float PenumbraDecrease { get { return AreFused ? 0.4f : 1.0f; } }

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

    public bool Can_Input = true;

    void Start () 
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		animator = GetComponent<Animator> ();

		if (players.Length <= 1) 
		{
			otherPlayer = null;
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
    void Update()
    {
        float playerDist = (otherPlayer.transform.position - transform.position).magnitude;

        // fusion takes place
        if (AreFused)
        {
            ToIdleAnimation();
            // player 1 sets position
            if (playerIndex == 1)
            {
                Vector3 centerPos = (NormalizedPos() + otherPlayer.NormalizedPos()) / 2;
                material.SetVector("_PlayerPos1", new Vector4(centerPos.x, centerPos.y, 0, 0));
            }
            // player2 sets his position somewhere we wont notice
            else
            {
                material.SetVector("_PlayerPos2", new Vector4(-300, -300, 0, 0));
            }
        }
        else
        {
            Vector2 playerPos = NormalizedPos();
            material.SetVector("_PlayerPos" + playerIndex.ToString(), new Vector4(playerPos.x, playerPos.y, 0, 0));
        }
        if (Can_Input){ 
            // can only move if not charging
            if (!IsCharging)
            {
                float x, y;
                if (playerIndex == 1) {
                    x = Input.GetAxis("HorizontalP1") * speed * Time.deltaTime;
                    y = Input.GetAxis("VerticalP1") * speed * Time.deltaTime;
                } else {
                    x = Input.GetAxis("HorizontalP2") * speed * Time.deltaTime;
                    y = Input.GetAxis("VerticalP2") * speed * Time.deltaTime;
                }

                transform.Translate(new Vector3(x, y, 0));
            }


            // flash if either player stopped charging or flash of other player triggered this one
            if (HasReleased)
            {
                ReleaseFlash();
            } else
            {
                if (IsCharging && otherPlayer.IsFlashing)
                {
                    if (playerDist < otherPlayer.Influence)
                    {
                        Invoke("ReleaseFlash", 0.3f);
                    }
                }
            }

            if (IsFlashing)
            {
                if (m_brightnessCycleTime >= Mathf.PI / 2)
                {
                    m_brightnessCycleTime += Time.deltaTime * shrinkSpeed;
                }
                else
                {
                    m_brightnessCycleTime += Time.deltaTime * expandSpeed;
                }
                m_influence = Mathf.Sin(m_brightnessCycleTime) * m_chargeDecelerator;
                material.SetFloat("_Radius" + playerIndex.ToString(), m_influence);
            }
            else //if( !IsFlashing )
            {
                if (IsCharging && !AreFused)
                {
                    ChargeFlash();
                    m_charge = Mathf.Min(maxCharge, m_charge + 0.3f * Time.deltaTime);
                }
                else
                {
                    RecoverFlash();
                    m_charge = minCharge;
                }
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
		ToChargingAnimation ();
		m_penumbra = Mathf.Lerp (m_penumbra, MinPenumbra, PenumbraDecrease * Time.deltaTime);
		material.SetFloat ("_Penumbra" + playerIndex.ToString(), m_penumbra);

		float shakeAmount = ( m_penumbra / (defaultPenumbra - minPenumbra) ) * shakeStrength;
		material.SetFloat ( "_ShakeX" + playerIndex.ToString(), Utils.RandRange(-shakeAmount, shakeAmount) );
	}

	public void RecoverFlash()
	{
		m_penumbra = Mathf.Lerp (m_penumbra, DefaultPenumbra, PenumbraIncrease * Time.deltaTime);
		material.SetFloat ("_Penumbra" + playerIndex.ToString(), m_penumbra);
	}

	public void ReleaseFlash()
	{
		if (IsFlashing)
			return;
		ToIdleAnimation ();
		material.SetFloat ( "_ShakeX" + playerIndex.ToString(), 0f );
		m_chargeDecelerator = m_charge;
		if( !IsFlashing ) m_brightnessCycleTime = 0f;
	}
		
	void ToChargingAnimation()
	{
		animator.SetBool ("Charge", true);
	}

	void ToIdleAnimation()
	{
		animator.SetBool ("Charge", false);
	}
}
