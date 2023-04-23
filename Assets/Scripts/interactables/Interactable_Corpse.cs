using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Corpse : Interactable_ItemContainer
{
	protected CharacterStats character;
	protected bool itemCopiesCreated = false;

	public override void Awake()
	{
		character = GetComponentInParent<CharacterStats>();
		if (character)
			character.OnDeathEvent += EnableCorpseInteractable;
		base.Awake();

		gameObject.SetActive(false);
	}

	private void EnableCorpseInteractable()
	{
		gameObject.SetActive(true);
	}

	protected override void CreateItemCopies()
	{
		if (!itemCopiesCreated)
		{
			itemCopiesCreated = true;
			base.CreateItemCopies();
		}
	}
}
