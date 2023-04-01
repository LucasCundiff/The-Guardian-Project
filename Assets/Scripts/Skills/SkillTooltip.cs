using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillTooltip : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI nameText, levelText, attackListText;

	private Item skillToDisplay;

	public void Start()
	{
		gameObject.SetActive(false);
	}

	public void Update()
	{
		if (gameObject.activeSelf)
			gameObject.transform.position = Mouse.current.position.ReadValue();
	}

	public void EnableTooltip(SkillSlot skillSlot)
	{
		var skillToDisplay = skillSlot.Skill;

		if (skillToDisplay)
		{
			nameText.text = skillToDisplay.SkillName;
			levelText.text = skillToDisplay.CurrentLevel > 0 ? $"({skillToDisplay.CurrentLevel}/{skillToDisplay.MaxLevel})" : "Locked";
			attackListText.text = skillToDisplay.GetSkillDestription();

			gameObject.SetActive(true);
		}
	}

	public void DisableTooltip(SkillSlot skillSlot)
	{
		gameObject.SetActive(false);
	}

	private void OnDisable() => DisableTooltip(null);
}
