using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed = 15f;
    public bool IsPlayerOne = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (IsPlayerOne)
        {
            float x = Input.GetAxis("HorizontalP1") * speed * Time.deltaTime;
            float y = Input.GetAxis("VerticalP1") * speed * Time.deltaTime;

            transform.Translate(new Vector3(x, y, 0));
        }
        else
        {
            float x = Input.GetAxis("HorizontalP2") * speed * Time.deltaTime;
            float y = Input.GetAxis("VerticalP2") * speed * Time.deltaTime;

            transform.Translate(new Vector3(x, y, 0));
        }
	}
}
