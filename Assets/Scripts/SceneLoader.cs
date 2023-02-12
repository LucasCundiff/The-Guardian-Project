using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public string StartingSceneName;

	void Start()
	{
		SceneManager.LoadScene(StartingSceneName, LoadSceneMode.Additive);
	}
}
