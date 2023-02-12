using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
	public int CurrentLevel { get; private set; }
	public int CurrentExperience { get; private set; }
	public int ExperienceNeeded { get; private set; }

	public Action OnLevelUpEvent;
	public Action<int, int> OnGainExperienceEvent;

	private int previousExeprienceNeededAmount = 100;
	private int currentExperienceNeededAmount = 100;

	private void Awake()
	{
		CurrentLevel = 1;
		CurrentExperience = 1;
		ExperienceNeeded = previousExeprienceNeededAmount + currentExperienceNeededAmount;
	}

	public void AddExperience(int amount)
	{
		CurrentExperience += amount;
		
		while (CurrentExperience > ExperienceNeeded)
			LevelUp();

		OnGainExperienceEvent?.Invoke(CurrentExperience, ExperienceNeeded);
	}

	private void LevelUp()
	{
		CurrentLevel++;
		OnLevelUpEvent?.Invoke();
		CurrentExperience -= ExperienceNeeded;

		previousExeprienceNeededAmount = currentExperienceNeededAmount;
		currentExperienceNeededAmount = ExperienceNeeded;
		ExperienceNeeded = previousExeprienceNeededAmount + currentExperienceNeededAmount;
	}
}
