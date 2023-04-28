using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	[SerializeField] Vector2 mouseSensitivty;
	[SerializeField] Vector2 verticalRotationClamp;
	[SerializeField] Transform currentCamera;
	[SerializeField] Transform currentCharacter;

	private float currentVerticalRotation;
	private float currentHorizontalRotation;

	public void Start()
	{
		PlayerInputManager.Instance.PlayerInput.Player.Look.performed += UpdateCameraRotation;
		PlayerInputManager.Instance.PlayerInput.Player.Look.performed += UpdateCharacterRotation;
	}

	public void UpdateCameraRotation(InputAction.CallbackContext context)
	{
		if (currentCamera)
			currentCamera.rotation = Quaternion.Euler(GetVerticalRotation(context.ReadValue<Vector2>().y), GetHorizontalRotation(context.ReadValue<Vector2>().x), 0);
	}

	private float GetHorizontalRotation(float mousePostionX)
	{
		currentHorizontalRotation += mousePostionX * mouseSensitivty.y * Time.deltaTime;

		return currentHorizontalRotation;
	}

	private float GetVerticalRotation(float mousePositionY)
	{
		currentVerticalRotation -= mousePositionY * mouseSensitivty.x * Time.deltaTime;
		currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, verticalRotationClamp.x, verticalRotationClamp.y);

		return currentVerticalRotation;
	}

	private void UpdateCharacterRotation(InputAction.CallbackContext context)
	{
		if (currentCharacter)
			currentCharacter.rotation = Quaternion.Euler(0, GetHorizontalRotation(context.ReadValue<Vector2>().y), 0);
	}
}
