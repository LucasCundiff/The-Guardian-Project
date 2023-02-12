using TMPro;
using UnityEngine;

public class SoulPointsUI : MonoBehaviour
{
	public PlayerSkills PlayerSkills;
	public TextMeshProUGUI textBox;

	public void Awake()
	{
		PlayerSkills.OnSoulPointsChangedEvent += UpdateSoulPointText;
		UpdateSoulPointText(PlayerSkills.SoulPoints);
	}

	public void UpdateSoulPointText(int amount)
	{
		textBox.text = amount.ToString();
	}
}
