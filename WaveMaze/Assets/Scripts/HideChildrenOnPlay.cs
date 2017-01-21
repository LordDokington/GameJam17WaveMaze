using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideChildrenOnPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (Transform t in transform) transform.gameObject.SetActive (false);
	}
}
