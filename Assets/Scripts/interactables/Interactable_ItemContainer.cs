using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactable_ItemContainer : MonoBehaviour, IInteractable
{
	[SerializeField] List<Item> items = new List<Item>();
	[SerializeField] string itemContainerName = "";

	public virtual void Awake()
	{
		CreateItemCopies();
	}

	protected virtual void CreateItemCopies()
	{
		var copiedList = items.Select(item => item.GetCopy()).ToList();
		items = new List<Item>(copiedList);
	}

	public virtual void Interact()
	{
		LoadItemsToItemContainerWindow();
	}

	private void LoadItemsToItemContainerWindow()
	{
		InteractableUI_ItemContainerWindow.Instance.OpenWindow(items, this);
	}

	public virtual void ItemContainerClose()
	{
	}

	public virtual string InteractDescription()
	{
		return itemContainerName;
	}

	public virtual void RemoveItem(Item item)
	{
		items.Remove(item);
	}
}
