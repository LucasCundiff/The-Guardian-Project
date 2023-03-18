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
		var playerLevel = CharacterTracker.Instance.Player.GetComponent<PlayerLevel>();
		if (playerLevel)
			playerLevel.AddExperience(ExperienceAmount);
	}
}
