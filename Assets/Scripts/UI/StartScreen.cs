using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
	[SerializeField] string playerScene;
	[SerializeField] string startingScene;

	public void StartGame()
	{
		SceneManager.LoadScene(playerScene, LoadSceneMode.Single);
		SceneManager.LoadScene(startingScene, LoadSceneMode.Additive);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
