using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatTooltip : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI statNameText;
	[SerializeField] TextMeshProUGUI statDescriptionText;

	public void Start()
	{
		gameObject.SetActive(false);
	}

	public void Update()
	{
		if (gameObject.activeSelf)
			gameObject.transform.position = Mouse.current.position.ReadValue();
	}

	public void ShowTooltip(StatUI statUI)
	{
		if (statUI && statNameText && statDescriptionText)
		{
			statNameText.text = statUI.CurrentStat.StatName;
			statDescriptionText.text = statUI.CurrentStat.StatDescription;

			gameObject.transform.position = Mouse.current.position.ReadValue();
			gameObject.SetActive(true);
		}
	}

	public void HideTooltip(StatUI statUI)
	{
		gameObject.SetActive(false);
	}
}
