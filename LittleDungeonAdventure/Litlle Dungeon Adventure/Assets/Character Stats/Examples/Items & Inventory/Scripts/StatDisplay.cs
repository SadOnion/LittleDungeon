using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Kryz.CharacterStats.Examples
{
	public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public TextMeshProUGUI NameText;
		public TextMeshProUGUI ValueText;

		[NonSerialized]
		public CharacterStat Stat;

		private void OnValidate()
		{
            TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
			NameText = texts[0];
			ValueText = texts[1];
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			StatTooltip.Instance.ShowTooltip(Stat, NameText.text);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			StatTooltip.Instance.HideTooltip();
		}
	}
}
