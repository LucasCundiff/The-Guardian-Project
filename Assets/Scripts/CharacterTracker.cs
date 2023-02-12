using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
	static public CharacterTracker Instance;
	public List<CharacterStats> AllCharacters { get; private set; }

	private void Awake()
	{
		if (Instance != null)
			Destroy(Instance);

		Instance = this;
		AllCharacters = new List<CharacterStats>(FindObjectsOfType<CharacterStats>());
		
	}

	public void RegisterCharacterToTracker(CharacterStats newCharacter)
	{
		if (AllCharacters.Contains(newCharacter)) return;

		AllCharacters.Add(newCharacter);
	}
	public void UnregisterCharacterToTracker(CharacterStats newCharacter) => AllCharacters.Remove(newCharacter);
}
