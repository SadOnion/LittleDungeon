using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats.Examples;
[CreateAssetMenu()]
public class WeaponItem : EquippableItem
{
    public Sprite[] weaponStates;


    public override void Equip(Character c)
    {
        base.Equip(c);
        c.GetComponentInChildren<WeaponHandler>().ChangeWeapon(this);
    }

    public override void Unequip(Character c)
    {
        base.Unequip(c);
        c.GetComponentInChildren<WeaponHandler>().ChangeWeapon(null);
    }
}
