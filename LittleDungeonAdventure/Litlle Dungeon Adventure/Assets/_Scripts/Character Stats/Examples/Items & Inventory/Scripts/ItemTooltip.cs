using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
namespace Kryz.CharacterStats.Examples
{
	public class ItemTooltip : MonoBehaviour
	{
		public static ItemTooltip Instance;

		[SerializeField] TextMeshProUGUI nameText;
		[SerializeField] TextMeshProUGUI slotTypeText;
		[SerializeField] TextMeshProUGUI statsText;

		private StringBuilder sb = new StringBuilder();

		private void Awake()
		{
			if (Instance == null) {
				Instance = this;
			} else {
				Destroy(this);
			}
			gameObject.SetActive(false);
		}

		public void ShowTooltip(Item itemToShow)
		{
			if (!(itemToShow is EquippableItem)) {
				return;
			}
            

			EquippableItem item = (EquippableItem)itemToShow;

			gameObject.SetActive(true);

			nameText.text = item.ItemName;
			slotTypeText.text = item.EquipmentType.ToString();

			sb.Length = 0;

			AddStatText(item.DamageBonus, " Damage");
			

			AddStatText(item.DamagePercentBouns * 100, "% Damage");
			

			statsText.text = sb.ToString();
		}

		public void HideTooltip()
		{
			gameObject.SetActive(false);
		}

		private void AddStatText(float statBonus, string statName)
		{
			if (statBonus != 0)
			{
				if (sb.Length > 0)
					sb.AppendLine();

				if (statBonus > 0)
					sb.Append("+");

				sb.Append(statBonus);
				sb.Append(statName);
			}
		}
	}
}
