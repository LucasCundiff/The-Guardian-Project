using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemTooltip : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI nameText, modifierText;

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
		var itemToDisplay = itemSlot.Item;

		if (itemToDisplay)
		{
			nameText.text = itemToDisplay.ItemName;
			modifierText.text = itemToDisplay.ItemDescription;

			gameObject.SetActive(true);
		}		
	}

	public void DisableTooltip(ItemSlot itemSlot)
	{
		gameObject.SetActive(false);
	}
}
