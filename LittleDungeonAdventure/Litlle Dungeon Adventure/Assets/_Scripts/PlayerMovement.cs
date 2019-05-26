using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    public float jumpHeight;
    public GameObject invPanel;

    Rigidbody2D body;
    Collider2D collider;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAnimation();
        HandleJumping();
        OtherInput();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickable item = collision.gameObject.GetComponent<IPickable>();
        if (item != null) item.PickUp();
    }
    private void OtherInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            invPanel.SetActive(!invPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.R)) anim.SetTrigger("Attack");
    }

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanJump())
            {
                Jump();
            }
        }
        
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        anim.SetTrigger("Jump");
    }

    private bool CanJump()
    {
        Bounds bounds = collider.bounds;
        RaycastHit2D info = Physics2D.Raycast(bounds.min, Vector2.down, .2f);
        RaycastHit2D info2 = Physics2D.Raycast(new Vector2(bounds.max.x, bounds.min.y), Vector2.down, .2f);
        if(info.collider != null)
        {
            if (info.collider.isTrigger == false) return true;
        }
        if (info2.collider != null)
        {
            if (info2.collider.isTrigger == false) return true;
        }
        return false;
    }

    private void HandleMovement()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        ChangeLocalScale(xAxis);
        body.velocity = new Vector2( xAxis* Time.deltaTime * speed, body.velocity.y);
    }

    private void ChangeLocalScale(float xAxis)
    {
        if (xAxis == 1 && transform.localScale.x != 1)
        {
            transform.localScale = Vector3.one;
        }
        if (xAxis == -1 && transform.localScale.x != -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void HandleAnimation()
    {
        if (body.velocity.y == 0 && CanJump()) anim.SetBool("OnGround", true);
        else anim.SetBool("OnGround", false);
        anim.SetFloat("yVelocity", body.velocity.y);
        if (Mathf.Abs(body.velocity.x) > 0)
        {
            anim.SetBool("IsWalking", true);
        }else anim.SetBool("IsWalking", false);
    }
}
