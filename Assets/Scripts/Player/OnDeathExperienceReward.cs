using UnityEngine;

public class OnDeathExperienceReward : MonoBehaviour
{
	public int ExperienceAmount;

	private CharacterStats characterStats;

	public void Start()
	{
		characterStats = GetComponentInParent<CharacterStats>();
		
		if(characterStats)
			characterStats.OnDeathEvent += ExperienceReward;
	}

	public void ExperienceReward()
	{
		foreach (CharacterStats character in CharacterTracker.Instance.AllCharacters)
		{
			var player = character.GetComponent<PlayerLevel>();
			if (player)
				player.AddExperience(ExperienceAmount);
		}	
	}
}
