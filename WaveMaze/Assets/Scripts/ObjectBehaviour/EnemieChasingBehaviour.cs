using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemieChasingBehaviour : MonoBehaviour
{
    private float _speed = 10f;
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
        _movePoints = new List<Vector3>();
        walkingTimeLeft = timeToToWalk;
        _movePoints.Add(transform.position);
    }


    void Update()
    {
        if (_isChasing)
        {
            transform.position += (_movePoints[1] - transform.position).normalized * _speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, _movePoints[1]) <= 0.1f)
            {
                gotoNextPoint();
            }
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
        for (int i = 0; i < _playerGOs.Count; ++i)
        {
            Vector3 posA = transform.position;
            Vector3 posB = _playerGOs[0].transform.position;

            var dist = Vector3.Distance(posA, posB);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestObjIndex = i;
            }
        }

        _playerTarget = _playerGOs[closestObjIndex].transform;
        if (!_isChasing && closestDistance <= _triggerRange)
        {
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
        if(other.gameObject.tag == "Player")
            Destroy(other.gameObject);
    }

}
