using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour,IDamageable
{
    public float maxHp;
    private float currentHp;
    [SerializeField]float damage;
    protected Rigidbody2D body;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHp = maxHp;
        body = GetComponent<Rigidbody2D>();
    }


    public virtual void TakeDamage(float amount)
    {
        currentHp -= amount;
        if (currentHp <= 0) Destroy(gameObject);
    }
    
}
