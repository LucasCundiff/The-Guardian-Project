using UnityEngine;
using UnityEngine.UI;

public class SwitchTabsInCharacterPanel : MonoBehaviour
{
	[SerializeField] Image itemsButton, skillsButton;
	[SerializeField] GameObject itemsWindow, skillsWindow;

	#region DeleteThisLater

	private void Awake()
	{
		skillsWindow.SetActive(true);
	}

	private void Start()
	{
		skillsWindow.SetActive(false);
	}

	#endregion

	public void SetItemsTabActive()
	{
		itemsButton.fillCenter = true;
		skillsButton.fillCenter = false;

		itemsWindow.SetActive(true);
		skillsWindow.SetActive(false);
	}

	public void SetSkillsTabActive()
	{
		skillsButton.fillCenter = true;
		itemsButton.fillCenter = false;

		skillsWindow.SetActive(true);
		itemsWindow.SetActive(false);
	}

	private void OnDisable()
	{
		SetItemsTabActive();
	}
}
