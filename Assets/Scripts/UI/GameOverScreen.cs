using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
	[SerializeField] GameObject gameOverScreen;
	[SerializeField] CharacterStats player;
	[SerializeField] string playerScene;

	private void Start()
	{
		gameOverScreen.SetActive(false);

		player.OnDeathEvent += ActivateScreen;
	}

	private void ActivateScreen()
	{
		gameOverScreen.SetActive(true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(playerScene, LoadSceneMode.Single);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
