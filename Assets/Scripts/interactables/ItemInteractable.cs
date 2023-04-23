using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour, IInteractable
{
	[SerializeField] Item startingItem;

	protected GameObject currentItemGameobject;

	protected Item item;
	public Item Item
	{
		get { return item; }
		set
		{
			item = value;
			if (currentItemGameobject)
				Destroy(currentItemGameobject);

			currentItemGameobject = Instantiate(Item.GetGFX(), transform);
			//var collider = gameObject.AddComponent<BoxCollider>(); Readd later when you have models to get measurments from
		}
	}

	void Start()
	{
		if (startingItem)
			Item = startingItem.GetCopy();
	}

	public void Interact()
	{
		var inventory = CharacterTracker.Instance.Player.GetComponent<CharacterInventory>();

		if (inventory && inventory.AddItem(Item))
		{
			Destroy(gameObject);
		}
	}

	public string InteractDescription()
	{
		return Item.ItemName;
	}
}
