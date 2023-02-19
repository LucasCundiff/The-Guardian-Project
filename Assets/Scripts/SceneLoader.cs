using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public string StartingScene;

	private void Start()
	{
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			if (SceneManager.GetSceneAt(i).name == StartingScene)
				return;
		}

		SceneManager.LoadScene(StartingScene, LoadSceneMode.Additive);
	}

}
