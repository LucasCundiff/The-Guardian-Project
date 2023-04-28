using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
	public PlayerInput PlayerInput;
	public CharacterStats Player;
	public static PlayerInputManager Instance;

	private void Awake()
	{
		if (Instance != null)
			Destroy(Instance);

		Instance = this;
	}

	private void Start()
	{
		Player.OnDeathEvent += () => PlayerInput.Player.Disable();
	}

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
