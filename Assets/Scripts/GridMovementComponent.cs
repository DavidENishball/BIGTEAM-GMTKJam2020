﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GridMovementComponent : MonoBehaviour
{
    public float MoveTime = 0.5f;

    public EMoveResult AttemptMovement(Vector2 Direction)
    {
        if (IsDirectionOccupied(Direction))
        {
            return EMoveResult.FAILURE;
        }
        else
        {
            DoMove(Direction);
            return EMoveResult.SUCCESS;
        }
    }

    public void DoMove(Vector2 Direction)
    {
        Vector2 Movepoint = new Vector2(transform.position.x, transform.position.y) + Direction;
        this.transform.DOMove(Movepoint, MoveTime).SetEase(Ease.OutExpo);
    }

    public bool IsDirectionOccupied(Vector2 Direction)
    {
        Vector3 Movepoint = transform.position + new Vector3(Direction.x, Direction.y);

        Collider[] AllHits = Physics.OverlapSphere(Movepoint, 0.4f);

        if (AllHits.Length > 0)
        {
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}