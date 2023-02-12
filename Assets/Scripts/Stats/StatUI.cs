using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
	protected Stat CurrentStat = null;

	public TextMeshProUGUI statNameText;
	public TextMeshProUGUI statValueText;

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
}
