using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Stat CurrentStat { get; private set; }

	public TextMeshProUGUI statNameText;
	public TextMeshProUGUI statValueText;

	public Action<StatUI> OnPointerEnterEvent;
	public Action<StatUI> OnPointerExitEvent;

	public void SetStat(Stat stat)
	{
		if (CurrentStat != stat)
		{
			if (CurrentStat != null)
				CurrentStat.StatModifiedEvent -= UpdateValueText;

			CurrentStat = stat;
			CurrentStat.StatModifiedEvent += UpdateValueText;
		}

		statNameText.text = CurrentStat.StatName;
		statValueText.text = CurrentStat.CurrentValue.ToString();
	}

	protected void UpdateValueText(Stat stat)
	{
		statValueText.text = stat.CurrentValue.ToString();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnPointerEnterEvent?.Invoke(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnPointerExitEvent?.Invoke(this);
	}
}
