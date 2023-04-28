using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackKeysReminderText : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI reminderText;

	public void Start()
	{
		reminderText.text = $"Primary:'{PlayerInputManager.Instance.PlayerInput.Player.Primary.bindings[0].ToDisplayString()}'  Secondary:'{PlayerInputManager.Instance.PlayerInput.Player.Secondary.bindings[0].ToDisplayString()}'  Tertiary:'{PlayerInputManager.Instance.PlayerInput.Player.Tertiary.bindings[0].ToDisplayString()}'  Quaternary:'{PlayerInputManager.Instance.PlayerInput.Player.Quaternary.bindings[0].ToDisplayString()}'";
	}
}