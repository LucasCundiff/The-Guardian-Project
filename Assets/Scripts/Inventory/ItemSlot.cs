using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
	[SerializeField] protected Image slotImage;
	[SerializeField] Sprite inactiveSprite;

	[SerializeField] Color clearColor = Color.clear;
	[SerializeField] Color visibleColor = Color.white;

	public event Action<ItemSlot> OnPointerEnterEvent;
	public event Action<ItemSlot> OnPointerExitEvent;
	public event Action<ItemSlot> OnRightClickEvent;
	public event Action<ItemSlot> OnLeftClickEvent;
	public event Action<ItemSlot> OnBeginDragEvent;
	public event Action<ItemSlot> OnDragEvent;
	public event Action<ItemSlot> OnEndDragEvent;
	public event Action<ItemSlot> OnDropEvent;

	protected bool isPointerOver;

	protected Item item;
	public Item Item
	{
		get { return item; }
		set
		{
			item = value;

			ToggleSlotImage();
			RefreshEvents();
		}
	}

	public void Awake()
	{
		Item = null;
	}

	public virtual bool CanRecieveItem(Item item)
	{
		return true;
	}

	private void ToggleSlotImage()
	{
		slotImage.sprite = Item ? Item.ItemSprite : inactiveSprite;
		slotImage.color = Item ? visibleColor : clearColor;
	}

	private void RefreshEvents()
	{
		if (isPointerOver)
		{
			OnPointerExit(null);
			OnPointerEnter(null);
		}
	}

	protected virtual void OnDisable()
	{
		if (isPointerOver)
			OnPointerExit(null);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData?.button == PointerEventData.InputButton.Right)
		{
			OnRightClickEvent?.Invoke(this);
		}
		else if (eventData?.button == PointerEventData.InputButton.Left)
		{
			OnLeftClickEvent?.Invoke(this);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isPointerOver = true;
		OnPointerEnterEvent?.Invoke(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerOver = false;
		OnPointerExitEvent?.Invoke(this);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		OnBeginDragEvent?.Invoke(this);
	}

	public void OnDrag(PointerEventData eventData)
	{
		OnDragEvent?.Invoke(this);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		OnEndDragEvent?.Invoke(this);
	}

	public void OnDrop(PointerEventData eventData)
	{
		OnDropEvent?.Invoke(this);
	}
}
