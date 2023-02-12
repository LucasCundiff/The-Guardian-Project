using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemTooltip : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI nameText, modifierText;

	private Item _itemToDisplay;

	public void Start()
	{
		gameObject.SetActive(false);
	}

	public void Update()
	{
		if (gameObject.activeSelf)
			gameObject.transform.position = Mouse.current.position.ReadValue();
	}

	public void EnableTooltip(ItemSlot itemSlot)
	{
		_itemToDisplay = itemSlot.Item;

		if (_itemToDisplay)
		{
			nameText.text = _itemToDisplay.ItemName;
			modifierText.text = _itemToDisplay.ItemDescription;

			gameObject.SetActive(true);
		}		
	}

	public void DisableTooltip(ItemSlot itemSlot)
	{
		gameObject.SetActive(false);
	}
}
