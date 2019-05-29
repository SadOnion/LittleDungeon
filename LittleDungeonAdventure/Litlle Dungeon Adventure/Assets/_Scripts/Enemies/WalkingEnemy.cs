using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    public float speed;
    private int direction;

    protected override void Start()
    {
        base.Start();
        direction = 1;
        transform.Translate(transform.position + Vector3.right*5);
    }
    // Update is called once per frame
    void Update()
    {
        if (ObstacleNear())
        {
            direction *= -1;
            
        }
        
        //body.velocity = new Vector2(speed * Time.deltaTime*direction,body.velocity.y);
    }

    private bool ObstacleNear()
    {
        RaycastHit2D info = Physics2D.Raycast(transform.position, Vector2.right * direction, .5f);
        if(info.collider != null)
        {
            if (info.collider.isTrigger == false) return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * .5f);
    }
}
