using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenuButton : MonoBehaviour
{
	[SerializeField] string MainMenuSceneName;

	//Called from a button
	public void ReturnToMenu()
	{
		SceneManager.LoadScene(MainMenuSceneName);
	}
}
