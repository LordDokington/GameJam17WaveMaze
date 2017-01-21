using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiePatrolingBehaviour : MonoBehaviour
{
    public Transform[] Points;
    public bool ShouldStopAtEnd;
    private int nextIndex = 1;
    private int startIndex = 0;
    private float speed = 0.2f;
    private float timeToToWalk = 1f;
    private float walkingTimeLeft;

    // Use this for initialization
    void Start()
    {
        walkingTimeLeft = timeToToWalk;
    }

    private void gotoNextPoint()
    {
        walkingTimeLeft = timeToToWalk;
        if (Points.Length == 0)
            return;

        startIndex = nextIndex;
        if (nextIndex == Points.Length - 1)
            nextIndex = 0;
        else
            ++nextIndex;        
    }


    // Update is called once per frame
    void Update()
    {
        walkingTimeLeft = walkingTimeLeft - speed * Time.deltaTime;
        if (Points.Length != 0)
        {
            transform.position = Vector3.Lerp(Points[startIndex].position, Points[nextIndex].position, 1 - walkingTimeLeft);
            if (Vector3.Distance(transform.position, Points[nextIndex].position) <= 0f)
                gotoNextPoint();
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
			Destroy(other.gameObject);
	}
}

