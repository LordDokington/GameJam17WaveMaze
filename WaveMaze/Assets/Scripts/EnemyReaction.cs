using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReaction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
		cam.GetComponent<EnemyLightReaction>().EnemyPosition1 = transform.position;
		
	}
}
