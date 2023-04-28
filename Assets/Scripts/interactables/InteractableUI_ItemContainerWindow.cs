using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableUI_ItemContainerWindow : MonoBehaviour
{
	[SerializeField] GameObject itemContainerWindowObject;
	[SerializeField] List<ItemSlot> itemSlots = new List<ItemSlot>();

	public ItemTooltip ItemTooltip;

	public static InteractableUI_ItemContainerWindow Instance;

	protected Interactable_ItemContainer itemsSource;

	public void Awake()
	{
		if (Instance)
			Destroy(Instance);

		Instance = this;

		itemContainerWindowObject.SetActive(true);
		AddEventsToSlots();
		itemContainerWindowObject.SetActive(false);
	}

	private void AddEventsToSlots()
	{
		foreach (ItemSlot itemSlot in itemSlots)
		{
			itemSlot.OnLeftClickEvent += LootItem;
			itemSlot.OnPointerEnterEvent += ItemTooltip.EnableTooltip;
			itemSlot.OnPointerExitEvent += ItemTooltip.DisableTooltip;
		}
	}

	public void OpenWindow(List<Item> items, Interactable_ItemContainer source)
	{
		itemContainerWindowObject.SetActive(true);
		itemsSource = source;

		if (items != null && source)
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (itemSlots.Count <= i) return;
				itemSlots[i].Item = items[i];
			}
		}
	}

	public void CloseWindow()
	{
		itemContainerWindowObject.SetActive(false);
		ClearItems();
		itemsSource?.ItemContainerClose();

		itemsSource = null;
	}

	private void ClearItems()
	{
		foreach (ItemSlot itemSlot in itemSlots)
		{
			itemSlot.Item = null;
		}
	}

	public void LootItem(ItemSlot itemSlot)
	{
		var playerInventory = CharacterTracker.Instance.Player.GetComponent<CharacterInventory>();
		var item = itemSlot.Item;

		if (playerInventory && playerInventory.AddItem(item))
		{
			foreach (ItemSlot lootItemSlot in itemSlots)
			{
				if (lootItemSlot.Item == item)
				{ 
					lootItemSlot.Item = null;
					itemsSource.RemoveItem(item);
					return;
				}
			}
		}
	}
}
