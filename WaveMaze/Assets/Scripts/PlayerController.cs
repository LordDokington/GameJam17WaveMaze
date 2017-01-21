using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed = 15f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;

		transform.Translate (new Vector3 (x, y, 0));	
	}
}
