using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour, IInteractable
{
	public Item Item;
	public ItemTooltip ItemTooltip;

	void Start()
	{
		Item = Item.GetCopy();
	}

	public void Interact()
	{
		var inventory = CharacterTracker.Instance.Player.GetComponent<CharacterInventory>();

		if (inventory && inventory.AddItem(Item))
			Destroy(gameObject);
	}

	public string InteractDescription()
	{
		return Item.ItemName;
	}
}
