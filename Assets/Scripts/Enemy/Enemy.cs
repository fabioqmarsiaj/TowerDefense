﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action OnEndReached;

    [SerializeField]
    private float moveSpeed = 3f;

    public Waypoint Waypoint { get; set; }

    public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);

    private int _currentWaypointIndex;

    private void Start()
    {
        _currentWaypointIndex = 0;
    }

    private void Update()
    {
        Move();
        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, moveSpeed * Time.deltaTime);
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            return true;
        }
        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            ReturnEnemyToPool();
        }
    }

    private void ReturnEnemyToPool()
    {
        OnEndReached?.Invoke();

        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }
}