using System.Collections;
using System.Collections.Generic;
using Kryz.CharacterStats.Examples;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractible
{

    [SerializeField] GameObject[] content;

    public void Interact(Character c)
    {
        Debug.Log("interacting with" + c.ToString());
        foreach (var item in content)
        {
            GameObject o = Instantiate(item, transform.position, Quaternion.identity);
            Rigidbody2D body = o.GetComponent<Rigidbody2D>();
            if (body != null) body.velocity = new Vector2(Random.Range(0f, 5f), Random.Range(0f, 5f));
        }
    }

}
