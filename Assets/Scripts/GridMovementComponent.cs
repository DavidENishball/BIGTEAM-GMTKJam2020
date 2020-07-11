using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GridMovementComponent : MonoBehaviour
{
    public float MoveTime = 0.5f;
    public void DoMove(Vector2 Direction)
    {
        Vector2 Movepoint = new Vector2(transform.position.x, transform.position.y) + Direction;
        this.transform.DOMove(Movepoint, MoveTime).SetEase(Ease.OutExpo);
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
