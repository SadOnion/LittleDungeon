using System.Collections;
using System.Collections.Generic;
using Kryz.CharacterStats.Examples;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractible
{

    [SerializeField] GameObject[] content;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public virtual void Interact(Character c)
    {
        anim.SetTrigger("Unlock");
    }

    public void DropContent()
    {
        foreach (var item in content)
        {
            GameObject o = Instantiate(item, transform.position, Quaternion.identity);
            Rigidbody2D body = o.GetComponent<Rigidbody2D>();
            if (body != null) body.velocity = new Vector2(Random.Range(-3f, 3f), Random.Range(0f, 5f));
        }
    }

}
