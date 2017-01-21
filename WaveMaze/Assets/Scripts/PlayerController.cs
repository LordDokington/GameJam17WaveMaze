using UnityEngine;
using System.Collections;
using WaveMaze;

public class PlayerController : MonoBehaviour
{
	public float speed = 15f;
    public bool IsPlayerOne = true;
    LevelData CurrentLevelData;

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

            transform.Translate( new Vector3(x, y, 0) );
        }
	}

    public void spawnPlayerOne()
    {
        CurrentLevelData = GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevelData;
        Vector2 aVector = CurrentLevelData.SpornPlayer1;
        this.transform.position = new Vector3( aVector.x, aVector.y, 0);
    }

	///////////////////////
}
