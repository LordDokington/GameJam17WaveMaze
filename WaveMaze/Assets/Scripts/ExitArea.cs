using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExitArea : MonoBehaviour {

    public GameObject TriggeredPlayer;
    public GameObject CollectedPlayer;

    public float CollectDistance = 0.8f;
    public float FollowDistance = 1f;
    public float MoveSpeed = 8f;
    public float FleetSpeed = 8.5f;

    bool CollectedMoveStart = false;
    bool CollectedMoveFinisched = false;

    bool FleetMoveStart = false;
    bool FleetMoveFinisched = false;

    int Etappe = 0;

    int nextFleetPointIndex = 0;
    public int FleetPointCount = 0;
    List<GameObject> FleetPointList;
    public GameObject RegFleetpoint{
        set {
            if (FleetPointList == null) {
                FleetPointList = new List<GameObject>();
            }
            FleetPointCount++;
            FleetPointList.Add(value);
            FleetPointList.Sort(delegate (GameObject a, GameObject b) {
                return a.transform.position.x.CompareTo(b.transform.position.x);
            });
            
        }
    }

    public GameObject FinischPoint;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        collectPlayer();
        FleetPlayer();
    }

    void collectPlayer() {
        if (CollectedMoveStart && !CollectedMoveFinisched)
        {
            TriggeredPlayer.GetComponent<PlayerController>().enabled = false;
            CollectedPlayer.GetComponent<PlayerController>().enabled = false;
            float aDistance = Vector3.Distance(TriggeredPlayer.transform.position, CollectedPlayer.transform.position);
            //Debug.Log(aDistance.ToString());
            if (aDistance < CollectDistance)
            {
                CollectedMoveStart = false;
                CollectedMoveFinisched = true;
            }
            if (CollectedMoveStart)
            {
                CollectedPlayer.transform.position += (TriggeredPlayer.transform.position - CollectedPlayer.transform.position).normalized * MoveSpeed * Time.deltaTime;
            }

           if (aDistance < FollowDistance)
           {
                FleetMoveStart = true;
           }
        }

        if (CollectedMoveFinisched)
        {
            CollectedMoveFinisched = true;
        }
    }

    void FleetPlayer()
    {
        //Debug.Log("PlayerController enable " + TriggeredPlayer.GetComponent<PlayerController>().enabled);
       
        if (FleetMoveStart && !FleetMoveFinisched)
        {
            Vector3 atarget;
            if (nextFleetPointIndex < FleetPointList.Count)
            {
                atarget = FleetPointList[nextFleetPointIndex].transform.position;
            }
            else
            {
                atarget = FinischPoint.transform.position;
            }

            float aDistance = Vector3.Distance(TriggeredPlayer.transform.position, atarget);
            Debug.Log(aDistance.ToString() + " " + nextFleetPointIndex.ToString());
            if (aDistance < CollectDistance)
            {
                nextFleetPointIndex++;
                if (atarget == FinischPoint.transform.position)
                {
                    FleetMoveStart = false;
                    FleetMoveFinisched = true;
                }
            }

            if (FleetMoveStart)
            {
                TriggeredPlayer.transform.position += (atarget - TriggeredPlayer.transform.position).normalized * FleetSpeed * Time.deltaTime;
            }

            if (FleetMoveFinisched)
            {
                FleetMoveFinisched = false;
                TriggeredPlayer.GetComponent<PlayerController>().enabled = true;
                CollectedPlayer.GetComponent<PlayerController>().enabled = true;
                CollectedPlayer.GetComponent<CircleCollider2D>().enabled = true;
            }
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
