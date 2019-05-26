using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats.Examples;
public class PickableItem : MonoBehaviour,IPickable
{

    [SerializeField] Item item;

    public void PickUp()
    {
        Character c = FindObjectOfType<Character>();
        if (c.inventory.AddItem(item))
        {
            Destroy(gameObject);
        }
    }
}
