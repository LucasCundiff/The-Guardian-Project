using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemManager : MonoBehaviour
{
	public CharacterInventory Inventory;
	public CharacterArmor Armor;
	public CharacterWeapons Weapons;
	public ItemTooltip ItemTooltip;

	public CharacterStats PlayerStats;
	public StatPanelUI StatPanelUI;
	public ActionBarSetup ActionBarSetup;

	public List<Item> StartingItems = new List<Item>();
	public List<ArmorItem> StartingArmor = new List<ArmorItem>();
	public List<WeaponItem> StartingWeapons = new List<WeaponItem>();

	[HideInInspector]
	public ItemSlot DragSlot;
	public ItemSlot DragSlotUI;

	private Item _previousItem;
	private Item _item;
	private EquipmentItem _equipmentItem;
	private ArmorItem _armorItem;
	private WeaponItem _weaponItem;

	public void Awake()
	{
		Inventory.OnRightClickEvent += RemoveItemFromInventory;
		Inventory.OnPointerEnterEvent += ItemTooltip.EnableTooltip;
		Inventory.OnPointerExitEvent += ItemTooltip.DisableTooltip;
		Inventory.OnBeginDragEvent += DragSlotStart;
		Inventory.OnDragEvent += WhileDragSlot;
		Inventory.OnEndDragEvent += EndDragSlot;
		Inventory.OnDropEvent += DropSlot;

		Armor.OnRightClickEvent += RemoveItemFromArmor;
		Armor.OnPointerEnterEvent += ItemTooltip.EnableTooltip;
		Armor.OnPointerExitEvent += ItemTooltip.DisableTooltip;
		Armor.OnArmorEquipped += EquipItem;
		Armor.OnArmorUnequipped += UnequipItem;
		Armor.OnBeginDragEvent += DragSlotStart;
		Armor.OnDragEvent += WhileDragSlot;
		Armor.OnEndDragEvent += EndDragSlot;
		Armor.OnDropEvent += DropSlot;

		Weapons.OnRightClickEvent += RemoveItemFromWeapons;
		Weapons.OnPointerEnterEvent += ItemTooltip.EnableTooltip;
		Weapons.OnPointerExitEvent += ItemTooltip.DisableTooltip;
		Weapons.OnWeaponEquippedEvent += EquipItem;
		Weapons.OnWeaponUnequippedEvent += UnequipItem;
		Weapons.OnBeginDragEvent += DragSlotStart;
		Weapons.OnDragEvent += WhileDragSlot;
		Weapons.OnEndDragEvent += EndDragSlot;
		Weapons.OnDropEvent += DropSlot;
	}

	public void Start()
	{
		StatPanelUI.InitializeStatUIs(PlayerStats);
		InitializeStartingEquipment();
		ActionBarSetup.UpdateActionSlotsWeapons();
	}

	private void InitializeStartingEquipment()
	{
		for (int i = 0; i < StartingItems.Count; i++)
		{
			Inventory.AddItem(StartingItems[i].GetCopy());
		}

		for (int i = 0; i < StartingArmor.Count; i++)
		{
			_armorItem = (ArmorItem)StartingArmor[i].GetCopy();

			if (Armor.ReplaceItem(_armorItem, out _previousItem))
			{
				if (_previousItem) _previousItem.Destroy();
			}
			else
				_armorItem.Destroy();
		}

		for (int i = 0; i < StartingWeapons.Count; i++)
		{
			_weaponItem = (WeaponItem)StartingWeapons[i].GetCopy();

			if (!Weapons.AddItem(_weaponItem))
			{
				if (Weapons.ReplaceItem(_weaponItem, out _previousItem))
					if (_previousItem) _previousItem.Destroy();
				else
					_weaponItem.Destroy();
			}
		}
	}

	private void RemoveItemFromInventory(ItemSlot itemSlot)
	{
		_item = itemSlot.Item;

		if (_item == null)
			return;
		else if (_item is ArmorItem armorItem)
		{
			if (Armor.ReplaceItem(armorItem, out _previousItem))
			{
				Inventory.RemoveItem(armorItem);

				if (_previousItem)
				{
					Inventory.AddItem(_previousItem);
					_previousItem = null;
				}

			}
		}
		else if (_item is WeaponItem weaponItem)
		{
			if (Weapons.AddItem(weaponItem))
			{
				Inventory.RemoveItem(weaponItem);
			}
			else if (Weapons.ReplaceItem(weaponItem, out _previousItem) && _previousItem)
			{
				Inventory.RemoveItem(weaponItem);

				if (_previousItem)
				{
					Inventory.AddItem(_previousItem);
					_previousItem = null;
				}
			}
		}
		else if (_item is ConsumableItem consumableItem)
		{
			consumableItem.UseItem(PlayerStats);

			Inventory.RemoveItem(consumableItem);

			consumableItem.Destroy();
		}
		else
		{
			Inventory.RemoveItem(_item);
		}
	}

	private void RemoveItemFromArmor(ItemSlot itemSlot)
	{
		if (itemSlot.Item is ArmorItem armorItem && Inventory.AddItem(armorItem))
		{
			Armor.RemoveItem(armorItem);
		}
	}

	private void RemoveItemFromWeapons(ItemSlot itemSlot)
	{
		if (itemSlot.Item is WeaponItem weaponItem && Inventory.AddItem(weaponItem))
		{
			Weapons.RemoveItem(weaponItem);
		}
	}

	public void DragSlotStart(ItemSlot itemSlot)
	{
		DragSlot = itemSlot;

		DragSlotUI.Item = itemSlot.Item;
		DragSlotUI.gameObject.SetActive(true);
	}

	public void WhileDragSlot(ItemSlot itemSlot)
	{
		if (DragSlotUI == null) return;

		DragSlotUI.transform.position = Mouse.current.position.ReadValue();
	}

	public void EndDragSlot(ItemSlot itemSlot)
	{
		DragSlotUI.Item = null;
	}

	public void DropSlot(ItemSlot dropSlot)
	{
		_previousItem = dropSlot.Item;
		_item = DragSlot.Item;

		if (dropSlot.CanRecieveItem(_item) && DragSlot.CanRecieveItem(_previousItem))
		{
			SwapItems(dropSlot);
		}

		DragSlotUI.Item = null;
	}

	private void SwapItems(ItemSlot dropSlot)
	{
		dropSlot.Item = _item;
		DragSlot.Item = _previousItem;

		if (CheckIsEquipmentSlot(dropSlot))
		{
			EquipItem(_item);
			UnequipItem(_previousItem);
		}

		if (CheckIsEquipmentSlot(DragSlot))
		{
			UnequipItem(_item);
			EquipItem(_previousItem);
		}

		ActionBarSetup.UpdateActionSlotsWeapons();
	}

	private bool CheckIsEquipmentSlot(ItemSlot itemSlot)
	{
		if (itemSlot is ArmorSlot armorSlot)
			return true;
		if (itemSlot is WeaponSlot weaponSlot)
			return true;

		return false;
	}

	private void EquipItem(Item item)
	{
		_equipmentItem = (EquipmentItem)item;

		_equipmentItem?.Equip(PlayerStats);
	}

	private void UnequipItem(Item item)
	{
		_equipmentItem = (EquipmentItem)item;

		_equipmentItem?.Unequip(PlayerStats);
	}
}
