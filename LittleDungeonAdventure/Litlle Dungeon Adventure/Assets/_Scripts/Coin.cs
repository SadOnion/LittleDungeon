using System.Collections;
using System.Collections.Generic;
using Kryz.CharacterStats.Examples;
using UnityEngine;

public class Coin : MonoBehaviour, IPickable
{
    [SerializeField] private int value;
    public void PickUp(Character c)
    {
        c.AddMoney(value);
        Destroy(gameObject);
    }
}
