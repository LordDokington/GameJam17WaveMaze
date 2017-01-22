using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryArea : MonoBehaviour {
    public bool player1Througth = false;
    public bool player2Througth = false;

    public GameObject player1;
    public GameObject player2;

    public bool canCheck = false;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        if (canCheck) {
            if (checkPosition()) {
                blockThis();
            }
            canCheck = !(player1Througth && player2Througth);
        }

    }

    //void test(Collision collision)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string aName = collision.gameObject.name;
        Debug.Log(aName + " triggered EntryArea");
        if (aName.Equals("Player1") || aName.Equals("Player2"))
        {
            if (!canCheck) {
                player1 = GameObject.Find("Player1");
                player2 = GameObject.Find("Player2");
            }
            canCheck = true;

            // this.GetComponent<BoxCollider2D>().isTrigger = false;
            //this.GetComponent<BoxCollider2D>().enabled = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string aName = collision.gameObject.name;
        Debug.Log(aName + " triggered EntryArea");
        if (aName.Equals("Player1") || aName.Equals("Player2"))
        {
            if (checkPosition()){
                blockThis();
            }
            // this.GetComponent<BoxCollider2D>().isTrigger = false;
            //this.GetComponent<BoxCollider2D>().enabled = false;
            
        }
    }

    void blockThis() {
        this.GetComponent<BoxCollider2D>().isTrigger = !(player1Througth && player2Througth);
    }

    bool checkPosition() {
        bool areturn = false;
        player1Througth = this.transform.position.x < player1.transform.position.x;
        player2Througth = this.transform.position.x < player2.transform.position.x;

        areturn = player1Througth && player2Througth;

        return areturn;
    }
}
