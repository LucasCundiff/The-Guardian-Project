using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
	public PlayerInput PlayerInput;

	private void OnEnable()
	{
		if (PlayerInput == null)
			PlayerInput = new PlayerInput();

		PlayerInput.Enable();
	}

	private void OnDisable()
	{
		PlayerInput.Disable();
	}
}
