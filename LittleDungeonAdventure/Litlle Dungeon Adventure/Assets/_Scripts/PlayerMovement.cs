using System;
using Kryz.CharacterStats.Examples;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,IDamageable
{
    public int speed;
    public float jumpHeight;
    public GameObject invPanel;

    Rigidbody2D body;
    Collider2D box;
    Animator anim;
    Character character;
    private Vector2 attackSize = new Vector2(1, .4f);
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAnimation();
        HandleJumping();
        OtherInput();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IPickable item = collision.gameObject.GetComponent<IPickable>();
        if (item != null) item.PickUp(character);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickable item = collision.gameObject.GetComponent<IPickable>();
        if (item != null) item.PickUp(character);
    }
    private void OtherInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            invPanel.SetActive(!invPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D[] col = Physics2D.OverlapBoxAll(transform.position,Vector2.one,0);
            foreach (var item in col)
            {
                IInteractible interactible = item.gameObject.GetComponent<IInteractible>();
                if (interactible != null) interactible.Interact(character);
            }
           
            
           
        }
    }

    private void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCapsuleAll(transform.position + Vector3.right * transform.localScale.x, attackSize, CapsuleDirection2D.Horizontal, 0); ;
        foreach (var item in hitColliders)
        {
            IDamageable obj = item.GetComponent<IDamageable>();
            if (obj != null) obj.TakeDamage(character.Damage.Value);
            Debug.Log("Attack fo " + character.Damage.Value);
        }
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
        Bounds bounds = box.bounds;
        RaycastHit2D info = Physics2D.Raycast(bounds.min, Vector2.down, .2f);
        RaycastHit2D info2 = Physics2D.Raycast(new Vector2(bounds.max.x, bounds.min.y), Vector2.down, .2f);
        if(info.collider != null)
        {
            if (info.collider.isTrigger == false)
            {
                
                return true;
            }

        }
        if (info2.collider != null)
        {
            if (info2.collider.isTrigger == false)
            {
               
                return true;
            }
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
        anim.SetBool("OnGround", CanJump());
        anim.SetFloat("yVelocity", body.velocity.y);
        if (Mathf.Abs(body.velocity.x) > 0)
        {
            anim.SetBool("IsWalking", true);
        }else anim.SetBool("IsWalking", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.right * transform.localScale.x, Vector3.one);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position,Vector3.one);
    }

    public void TakeDamage(float amount)
    {
        throw new NotImplementedException();
    }
}
