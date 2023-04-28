using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float movementSpeed;
	[SerializeField] Rigidbody playerRigidbody;
	[SerializeField] Transform playerOrientation;

	private Vector2 playerMoveInputValue;
	private Vector3 moveDirection;

	private void Update()
	{
		playerMoveInputValue = PlayerInputManager.Instance.PlayerInput.Player.Move.ReadValue<Vector2>();
		moveDirection = playerOrientation.forward * playerMoveInputValue.y + playerOrientation.right * playerMoveInputValue.x;
	}
	public void FixedUpdate()
	{
		MovePlayerCharacter();
	}

	private void MovePlayerCharacter()
	{
		playerRigidbody.MovePosition(playerRigidbody.position + moveDirection * movementSpeed * Time.fixedDeltaTime);
	}
}
