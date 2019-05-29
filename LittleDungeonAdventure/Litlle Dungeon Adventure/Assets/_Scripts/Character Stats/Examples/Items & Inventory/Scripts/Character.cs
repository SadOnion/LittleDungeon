using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
	public class Character : MonoBehaviour
	{
        public CharacterStat Damage;
        public CharacterStat Armor;

		public Inventory inventory;
		[SerializeField] EquipmentPanel equipmentPanel;
		[SerializeField] StatPanel statPanel;

        private int money;
		private void Awake()
		{
			statPanel.SetStats(Damage,Armor);
			statPanel.UpdateStatValues();

			inventory.OnItemRightClickedEvent += EquipFromInventory;
			equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;
		}

		private void EquipFromInventory(Item item)
		{
			if (item is EquippableItem)
			{
				Equip((EquippableItem)item);
			}
		}

		private void UnequipFromEquipPanel(Item item)
		{
			if (item is EquippableItem)
			{
				Unequip((EquippableItem)item);
			}
		}

		public void Equip(EquippableItem item)
		{
			if (inventory.RemoveItem(item))
			{
				EquippableItem previousItem;
				if (equipmentPanel.AddItem(item, out previousItem))
				{
					if (previousItem != null)
					{
						inventory.AddItem(previousItem);
						previousItem.Unequip(this);
						statPanel.UpdateStatValues();
					}
					item.Equip(this);
					statPanel.UpdateStatValues();
				}
				else
				{
					inventory.AddItem(item);
				}
			}
		}

		public void Unequip(EquippableItem item)
		{
			if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
			{
				item.Unequip(this);
				statPanel.UpdateStatValues();
				inventory.AddItem(item);
			}
		}

        public void AddMoney(int amount)
        {
            money += amount;
        }

        public bool TakeMoney(int amount)
        {
            if (money - amount < 0) return false;
            else
            {
                money -= amount;
                return true;
            }
        }
        public int GetMoney() { return money; }
        
    }
}
