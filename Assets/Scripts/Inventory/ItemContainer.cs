using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
	[SerializeField] protected List<ItemSlot> itemSlots = new List<ItemSlot>();

	public event Action<ItemSlot> OnPointerEnterEvent;
	public event Action<ItemSlot> OnPointerExitEvent;
	public event Action<ItemSlot> OnRightClickEvent;
	public event Action<ItemSlot> OnLeftClickEvent;
	public event Action<ItemSlot> OnBeginDragEvent;
	public event Action<ItemSlot> OnDragEvent;
	public event Action<ItemSlot> OnEndDragEvent;
	public event Action<ItemSlot> OnDropEvent;

	public List<ItemSlot> ItemSlots { get { return itemSlots; } }

	public void Start()
	{
		for (int i = 0; i < itemSlots.Count; i++)
		{
			AddEventsToItemSlot(i);
		}
	}

	private void AddEventsToItemSlot(int index)
	{
		itemSlots[index].OnPointerEnterEvent += OnPointerEnterEvent;
		itemSlots[index].OnPointerExitEvent += OnPointerExitEvent;
		itemSlots[index].OnRightClickEvent += OnRightClickEvent;
		itemSlots[index].OnLeftClickEvent += OnLeftClickEvent;
		itemSlots[index].OnBeginDragEvent += OnBeginDragEvent;
		itemSlots[index].OnDragEvent += OnDragEvent;
		itemSlots[index].OnEndDragEvent += OnEndDragEvent;
		itemSlots[index].OnDropEvent += OnDropEvent;
	}

	public virtual bool AddItem(Item item)
	{
		return false;
	}

	public virtual bool RemoveItem(Item item)
	{
		return false;
	}

	public virtual bool ReplaceItem(Item newItem, out Item previousItem)
	{
		previousItem = null;
		return false;
	}
}
