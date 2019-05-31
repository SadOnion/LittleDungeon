using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    [SerializeField]float speed;
    [SerializeField] float range;
    [SerializeField] float playerUpSearch;
    private Vector2 startPos;
    private Vector2 leftCorner;
    private Vector2 rightCorner;
    private State state;
    private Animator anim;
    private Transform player;
    Coroutine c;
    bool goForDestination;
    bool canMove=true;
    private float playerDistanceToAttack=1;
    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
        rightCorner = startPos + Vector2.right * range;
        leftCorner = startPos - Vector2.right * range;
        anim = GetComponent<Animator>();
        state = State.MovingLeft;
        player = FindObjectOfType<PlayerMovement>().transform;
    }
    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            switch (state)
            {
                case State.MovingLeft:
                    anim.SetBool("Moving", true);
                    transform.localScale = new Vector3(-1, 1, 1);
                    body.MovePosition(Vector2.MoveTowards(transform.position, leftCorner, Time.deltaTime * speed));
                    if (transform.position.x == leftCorner.x)
                    {
                        goForDestination = false;
                        
                        StartCoroutine(Wait(3));
                    }
                    if (goForDestination == false)
                    {
                        LookForPlayer();
                    }

                    break;
                case State.MovingRight:
                    anim.SetBool("Moving", true);
                    transform.localScale = Vector3.one;
                    body.MovePosition(Vector2.MoveTowards(transform.position, rightCorner, Time.deltaTime * speed));
                    if (transform.position.x == rightCorner.x)
                    {
                        goForDestination = false;
                        
                        c = StartCoroutine(Wait(3));
                    }
                    if (goForDestination == false)
                    {

                        LookForPlayer();
                    }
                    break;
                case State.Waiting:
                    anim.SetBool("Moving", false);
                   LookForPlayer();
                    break;
                case State.ChasePlayer:
                    anim.SetBool("Moving", true);
                    Vector2 moveDirection = Vector2.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y), Time.deltaTime * speed);
                    if (transform.position.x < leftCorner.x)
                    {
                        goForDestination = true;
                        c = StartCoroutine(Wait(1));
                    }
                    if (transform.position.x > rightCorner.x)
                    {
                        goForDestination = true;
                        c = StartCoroutine(Wait(1));
                    }
                    transform.localScale = new Vector3(Mathf.Sign(moveDirection.x - body.position.x), 1, 1);
                    if (Mathf.Abs(transform.position.x - player.position.x) < playerDistanceToAttack)
                    {
                        anim.SetTrigger("Attack");
                        state = State.Attacking;
                    }
                    else if(goForDestination == false)
                    {
                        body.MovePosition(moveDirection);
                    }



                    break;
                case State.Idle:
                    anim.SetBool("Moving", false);
                    break;
                default:
                    break;

            }
        }
        
        
    }
    private void StopMoving()
    {
        canMove = false;
    }
    private void StartMoving()
    {
        canMove = true;
    }
    private void LookForPlayer()
    {

        if(player.position.x > leftCorner.x-playerDistanceToAttack && player.position.x < rightCorner.x+playerDistanceToAttack && player.position.y-transform.position.y < playerUpSearch)
        {
            state = State.ChasePlayer;
        }
        /*
        if(Vector3.Distance(player.position,transform.position) < playerSearchRadius)
        {
            state = State.ChasePlayer;
        }
        */
    }
    void Attack()
    {
        Collider2D[] coliders =  Physics2D.OverlapBoxAll(transform.position + Vector3.right * transform.localScale.x, Vector2.one, 0);
        foreach (var item in coliders)
        {
            IDamageable damageable = item.GetComponent<IDamageable>();
            if (damageable != null) damageable.TakeDamage(damage);
        }
    }
    public void AttackEnd()
    {
        
        c = StartCoroutine(WaitIdle(1f));
        anim.SetTrigger("FinishedAttack");
    }
    IEnumerator Wait(float waitTime)
    {
        state = State.Waiting;
        yield return new WaitForSeconds(waitTime);

        if (Mathf.Abs(transform.position.x - leftCorner.x) > Mathf.Abs(transform.position.x - rightCorner.x)) state = State.MovingLeft;
        else state = State.MovingRight;
    }
    IEnumerator WaitIdle(float waitTime)
    {
        state = State.Idle;
        yield return new WaitForSeconds(waitTime);

        if (Mathf.Abs(transform.position.x - leftCorner.x) > Mathf.Abs(transform.position.x - rightCorner.x)) state = State.MovingLeft;
        else state = State.MovingRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,transform.position+Vector3.right*range);
        Gizmos.DrawLine(transform.position,transform.position+Vector3.right*-range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position,new Vector3(range+playerDistanceToAttack,playerUpSearch));
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position + Vector3.right * transform.localScale.x, Vector2.one);
    }
    private enum State
    {
        MovingLeft,
        MovingRight,
        Waiting,
        ChasePlayer,
        Attacking,
        Idle,
    }
        
}
