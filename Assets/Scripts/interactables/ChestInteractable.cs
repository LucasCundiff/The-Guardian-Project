using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
	[SerializeField] Animator chestAnimator;

	protected string chestAnimatorBoolName = "ChestIsOpen";

	public void Interact()
	{
		chestAnimator.SetBool(chestAnimatorBoolName, true);
		Destroy(this);
	}

	public string InteractDescription()
	{
		return "Loot Chest";
	}

}
