
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour,IDamageable
{
    [SerializeField]GameObject crateParticle;
    [SerializeField] ItemAndChance[] itemsAndChances; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        CreateLoot();
        Instantiate(crateParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void CreateLoot()
    {
        
            foreach (var item in itemsAndChances)
            {
                if (Random.Range(0, 101) <= item.chance)
                {
                    GameObject o = Instantiate(item.prefab, transform.position, Quaternion.identity);
                    o.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3f, 3f), Random.Range(0f, 5f));
                }
            }
        
        
    }

    [System.Serializable]
    private struct ItemAndChance
    {
        [Range(0,100)]
        public int chance;
        public GameObject prefab;
    }
}
