using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIToggle : MonoBehaviour
{
	[SerializeField] GameObject characterPanel;

	public void Awake()
	{
		PlayerInputManager.Instance.PlayerInput.UI.WindowToggle.performed += EnableCharacterWindow;

		characterPanel.SetActive(true);
	}

	public void Start()
	{
		characterPanel.SetActive(false);
	}

	private void EnableCharacterWindow(InputAction.CallbackContext obj)
	{
		if (GameStateManager.Instance.CurrentState is GameState_Default)
			characterPanel.SetActive(true);
	}
}
