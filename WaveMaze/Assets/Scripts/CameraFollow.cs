using UnityEngine;
using System.Collections;

namespace WaveMaze 
{

public class CameraFollow : MonoBehaviour 
{
	public GameObject target;

	public float interpolationSpeed = 0.14f;

	// Use this for initialization
	void Start ()
	{
		int wallCount = 10;
		GameObject wallPrefab = Resources.Load ("Prefabs/Wall") as GameObject;
		
		for (int i = 0; i < wallCount; ++i) 
		{
			float randX = Utils.RandRange (-40, 40);
			float randY = Utils.RandRange (-40, 40);

			GameObject wall = Instantiate ( wallPrefab, new Vector3 (randX, randY, 0), Quaternion.identity );
			wall.transform.Rotate (0, 0, Random.value * 360);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 deltaVec = target.transform.position - transform.position;
		
		if (deltaVec.magnitude < 0.02f)
			return;

		float transformZ = transform.position.z;
		
		Vector3 newPos = Utils.Lerp( transform.position, target.transform.position, interpolationSpeed);
		newPos.z = transformZ;
		transform.position = newPos;
	}
}
}
