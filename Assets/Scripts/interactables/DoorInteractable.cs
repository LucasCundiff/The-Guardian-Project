using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
	[SerializeField] Animator doorAnimator;

	protected string doorAnimatorBoolName = "DoorIsOpen";

	public void Interact()
	{
		var state = doorAnimator.GetBool(doorAnimatorBoolName);
		doorAnimator.SetBool(doorAnimatorBoolName, !state);
	}

	public string InteractDescription()
	{
		var stateText = doorAnimator.GetBool(doorAnimatorBoolName) ? "Close" : "Open";
		return $"{stateText} Door";
	}
}
