using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemieChasingBehaviour : MonoBehaviour
{
    private float _speed = 16f;
    private Transform _playerTarget;
    private List<GameObject> _playerGOs;
    private float _triggerRange = 10f;
    private bool _isChasing;
    private float timeToToWalk = 1f;
    private float walkingTimeLeft;
    private List<Transform> _movePoints;

    void Start()
    {
        _playerGOs = GameObject.FindGameObjectsWithTag("Player").ToList();
        walkingTimeLeft = timeToToWalk;
    }


    void Update()
    {
        if (_isChasing)
        {
            //transform.position = 
            walkingTimeLeft = walkingTimeLeft - _speed * Time.deltaTime;
            transform.position = Vector3.Lerp(_movePoints[0].position, _movePoints[1].position, 1 - walkingTimeLeft);
            if (Vector3.Distance(transform.position, _movePoints[1].position) <= 0f)
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
            var dist = Vector3.Distance(transform.position, _playerGOs[0].transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestObjIndex = i;
            }
        }

        if (closestDistance <= _triggerRange)
        {
            _isChasing = true;
            _movePoints.Add(_playerGOs[closestObjIndex].transform);
        }
    }

    private void gotoNextPoint()
    {
        walkingTimeLeft = timeToToWalk;
        //CircleCollider2D
        if (Points.Length == 0)
            return;

        _movePoints.RemoveAt(0);
    }
}
