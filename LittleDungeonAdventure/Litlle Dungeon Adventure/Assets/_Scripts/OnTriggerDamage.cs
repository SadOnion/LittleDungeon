using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDamage : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float deltaTime;
    private float time;

    private void Start()
    {
        time = deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(time <= 0)
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null) damageable.TakeDamage(damage);
            time = deltaTime;
        }
        time -= Time.deltaTime;
    }
}
