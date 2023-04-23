using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ChestInteractable : Interactable_ItemContainer, IInteractable
{
	[SerializeField] Animator chestAnimator;

	protected string chestAnimatorBoolName = "ChestIsOpen";

	public override void Interact()
	{
		chestAnimator.SetBool(chestAnimatorBoolName, true);
		base.Interact();
	}

	public override void ItemContainerClose()
	{
		chestAnimator.SetBool(chestAnimatorBoolName, false);
	}
}
