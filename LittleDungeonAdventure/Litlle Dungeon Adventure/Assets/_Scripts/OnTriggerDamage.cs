using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDamage : MonoBehaviour
{
    [SerializeField] float damage;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null) damageable.TakeDamage(damage);
            
        
    }
 
}
