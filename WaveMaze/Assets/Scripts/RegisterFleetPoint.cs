using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterFleetPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponentInParent<ExitArea>().RegFleetpoint = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
