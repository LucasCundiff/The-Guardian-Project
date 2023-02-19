using TMPro;
using UnityEngine;

public class PlayerLevelUI : MonoBehaviour
{
	[SerializeField] PlayerLevel targetPlayer;
	[SerializeField] TextMeshProUGUI levelText;

	private void Awake()
	{
		if (targetPlayer)
		{
			targetPlayer.OnLevelUpEvent += () => levelText.text = targetPlayer.CurrentLevel.ToString();

			levelText.text = targetPlayer.CurrentLevel.ToString();
		}
	}
}
