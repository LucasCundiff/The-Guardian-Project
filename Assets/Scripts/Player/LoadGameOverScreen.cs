using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadGameOverScreen : MonoBehaviour
{
	[SerializeField] CharacterStats player;
	[SerializeField] string GameOverScreenName;

	private void Start()
	{
		player.OnDeathEvent += LoadGOScreen;
	}

	private void LoadGOScreen()
	{
		SceneManager.LoadScene(GameOverScreenName);
	}
}
