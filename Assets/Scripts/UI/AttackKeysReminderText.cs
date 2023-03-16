using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackKeysReminderText : MonoBehaviour
{
	[SerializeField] PlayerInputManager playerInput;
	[SerializeField] TextMeshProUGUI reminderText;

	public void Start()
	{
		reminderText.text = $"Primary:'{playerInput.PlayerInput.Player.Primary.bindings[0].ToDisplayString()}'  Secondary:'{playerInput.PlayerInput.Player.Secondary.bindings[0].ToDisplayString()}'  Tertiary:'{playerInput.PlayerInput.Player.Tertiary.bindings[0].ToDisplayString()}'  Quaternary:'{playerInput.PlayerInput.Player.Quaternary.bindings[0].ToDisplayString()}'";
	}
}