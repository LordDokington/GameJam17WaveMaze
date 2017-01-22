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

    void test(Collision collision)
    //private void OnTriggerEnter(Collision collision)
    {
        string aName = collision.gameObject.name;
        if (aName.Equals("Player1") || aName.Equals("Player2"))
        {
            Debug.Log(this.name + "EntryArea triggered");
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
