using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //void test(Collision collision)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string aName = collision.gameObject.name;
        Debug.Log(aName + " triggered EntryArea");
        if (aName.Equals("Player1") || aName.Equals("Player2"))
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
