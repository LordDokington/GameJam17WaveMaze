using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemieChasingBehaviour : MonoBehaviour
{
    private float _speed = 1f;
    private Transform _playerTarget;
    private List<GameObject> _playerGOs;
    private float _triggerRange = 5f;
    private bool _isChasing = false;
    private float timeToToWalk = 1f;
    private float walkingTimeLeft;
    private List<Vector3> _movePoints;

    void Start()
    {
        _playerGOs = GameObject.FindGameObjectsWithTag("Player").ToList();
        _playerGOs.Add(GameObject.FindGameObjectWithTag("Player"));
        _movePoints = new List<Vector3>();
        walkingTimeLeft = timeToToWalk;
        _movePoints.Add(transform.position);
    }


    void Update()
    {
        if (_isChasing)
        {
            //float x = _movePoints[1].x- _movePoints[0].x * _speed * Time.deltaTime;
            //float y = _movePoints[1].y - _movePoints[0].y * _speed * Time.deltaTime;

            //transform.Translate(new Vector3(x, y, 0));

            //transform.position += (_movePoints[1].position - transform.position).normalized * _speed * Time.deltaTime;
            //transform.Translate(new Vector3(0f, 0f, 1f) * _speed * Time.deltaTime);
            //transform.position = 
            walkingTimeLeft = walkingTimeLeft - _speed * Time.deltaTime;

            transform.position = Vector3.Lerp(_movePoints[0], _movePoints[1], 1 - walkingTimeLeft);
            if (Vector3.Distance(transform.position, _movePoints[1]) <= 0f)
                gotoNextPoint();
        }
        else
        { 
            isPlayerInRange();
        }
    }

    private void isPlayerInRange()
    {

        int closestObjIndex = 0;
        float closestDistance = float.MaxValue;
        for (int i = 0; i < _playerGOs.Count - 1; ++i)
        {
            Vector3 posA = transform.position;
            Vector3 posB = _playerGOs[0].transform.position;

            var dist = Vector3.Distance(posA, posB);
            Debug.Log(dist);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestObjIndex = i;
            }
        }

        //Debug.Log(closestDistance);

        _playerTarget = _playerGOs[closestObjIndex].transform;
        if (!_isChasing && closestDistance <= _triggerRange)
        {
            Debug.Log("The chase is on");
            _isChasing = true;
            _movePoints.Add(_playerGOs[closestObjIndex].transform.position);
        }
    }

    private void gotoNextPoint()
    {
        walkingTimeLeft = timeToToWalk;
        isPlayerInRange();
        _movePoints.RemoveAt(0);
        _movePoints.Add(_playerTarget.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if(other.gameObject.tag == "Player")
            Destroy(other.gameObject);
    }
}
