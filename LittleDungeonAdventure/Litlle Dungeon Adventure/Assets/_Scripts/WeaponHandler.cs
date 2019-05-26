using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{


    private Sprite[] weaponStates;
    public int currentState;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponStates != null)
        {
            if (currentState < weaponStates.Length) sr.sprite = weaponStates[currentState];
            else sr.sprite = null;
        }
        
    }

    public void ChangeWeapon(WeaponItem weapon)
    {
        if(weapon != null)
        {
            weaponStates = weapon.weaponStates;
        }
        else
        {
            sr.sprite = null;
            weaponStates = null;
        }
      
    }
    public void ChangeState(int state)
    {

    }
}
