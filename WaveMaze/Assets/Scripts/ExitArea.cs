using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArea : MonoBehaviour {

    public GameObject TriggeredPlayer;
    public GameObject CollectedPlayer;

    public float Distance = 0.8f;
    public float MoveSpeed = 8f;

    bool CollectedMoveStart = false;
    bool CollectedMoveFinisched = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (CollectedMoveStart && !CollectedMoveFinisched)
        {
            float aDistance = Vector3.Distance(TriggeredPlayer.transform.position, CollectedPlayer.transform.position);
                Debug.Log(aDistance.ToString());
            if ( aDistance < Distance)
            {
                CollectedMoveStart = false;
                CollectedMoveFinisched = true;
            }
            if (CollectedMoveStart)
            {
                CollectedPlayer.transform.position += (TriggeredPlayer.transform.position - CollectedPlayer.transform.position).normalized * MoveSpeed * Time.deltaTime;
            }
        }

        if (CollectedMoveFinisched)
        {
            StartTransition();
        }

    }
    //void Test(Collision collision)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string aName = collision.gameObject.name;
        Debug.Log(aName + "triggered ExitArea ");
        if ( aName.Equals("Player1") || aName.Equals("Player2") )
        {
            TriggeredPlayer = GameObject.Find(aName);
            this.GetComponent<BoxCollider2D>().enabled = false;
            CollectPlayer();
        }
    }

    void CollectPlayer()
    {
        string aPlayerName = TriggeredPlayer.gameObject.name == "Player1" ? "Player2" : "Player1";
        CollectedPlayer = GameObject.Find(aPlayerName);
        CollectedPlayer.GetComponent<CircleCollider2D>().enabled = false;
        CollectedMoveStart = true;
    }

    void StartTransition()
    {

    }
}
