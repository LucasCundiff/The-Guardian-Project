using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPanelToggle : MonoBehaviour
{
	[SerializeField] PlayerInputManager playerInputManager;
	[SerializeField] GameObject characterPanel, generalUI;
	[SerializeField] bool StartOpen;

	public void Start()
	{
		playerInputManager.PlayerInput.UI.CharacterPanelToggle.performed += ToggleEventHelper;

		ToggleCharacterPanel(StartOpen);
	}

	public void ToggleCharacterPanel(bool state)
	{
		if (characterPanel)
		{
			characterPanel.SetActive(state);
			generalUI.SetActive(!state);

			//Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
			//Cursor.visible = state;

			Time.timeScale = state ? 0f : 1f;
		}
	}

	public void ToggleEventHelper(InputAction.CallbackContext context)
	{
		ToggleCharacterPanel(!characterPanel.activeSelf);
	}
}
