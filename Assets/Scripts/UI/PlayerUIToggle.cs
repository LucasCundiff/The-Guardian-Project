using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIToggle : MonoBehaviour
{
	[SerializeField] PlayerInputManager playerInputManager;
	[SerializeField] GameObject characterPanel, generalUI;
	[SerializeField] bool StartOpen;

	public void Awake()
	{
		ToggleCharacterPanel(true);
	}

	public void Start()
	{
		playerInputManager.PlayerInput.UI.CharacterPanelToggle.performed += ToggleEventHelper;

		ToggleCharacterPanel(StartOpen);

		if (!StartOpen)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	public void ToggleCharacterPanel(bool state)
	{
		if (characterPanel)
		{
			characterPanel.SetActive(state);
			generalUI.SetActive(!state);

			Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
			Cursor.visible = state;

			Time.timeScale = state ? 0f : 1f;
		}
	}

	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void ToggleEventHelper(InputAction.CallbackContext context)
	{
		ToggleCharacterPanel(!characterPanel.activeSelf);
	}
}
