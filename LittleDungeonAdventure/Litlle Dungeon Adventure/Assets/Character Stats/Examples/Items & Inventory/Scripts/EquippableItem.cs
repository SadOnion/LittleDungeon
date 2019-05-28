using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
	public enum EquipmentType
	{
		Helmet,
		Chest,
		Pants,
		Boots,
		Weapon,
		Accessory1,
		Accessory2,
	}

	[CreateAssetMenu]
	public class EquippableItem : Item
	{
        public int DamageBonus;
        [Space]
        public float DamagePercentBouns;
		[Space]
        public int ArmorBonus;
        [Space]
        public float ArmorPercentBouns;
        [Space]
        public EquipmentType EquipmentType;

		public virtual void Equip(Character c)
		{
			if (DamageBonus != 0)
				c.Damage.AddModifier(new StatModifier(DamageBonus, StatModType.Flat, this));
            if (ArmorBonus != 0)
                c.Armor.AddModifier(new StatModifier(ArmorBonus, StatModType.Flat, this));

            if (DamagePercentBouns != 0)
				c.Damage.AddModifier(new StatModifier(DamagePercentBouns, StatModType.PercentMult, this));
            if (ArmorPercentBouns != 0)
                c.Armor.AddModifier(new StatModifier(ArmorPercentBouns, StatModType.PercentMult, this));

        }

		public virtual void Unequip(Character c)
		{
            c.Damage.RemoveAllModifiersFromSource(this);
            c.Armor.RemoveAllModifiersFromSource(this);
		}
	}
}