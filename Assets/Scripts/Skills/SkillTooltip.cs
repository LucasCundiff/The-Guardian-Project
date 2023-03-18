using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillTooltip : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI nameText, attackListText;

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
			attackListText.text = skillToDisplay.GetSkillDestription();

			gameObject.SetActive(true);
		}
	}

	public void DisableTooltip(SkillSlot skillSlot)
	{
		gameObject.SetActive(false);
	}
}
