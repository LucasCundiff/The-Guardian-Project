using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddExperienceButton : MonoBehaviour
{
	public PlayerLevel PlayerLevel;
	public int ExperienceToAdd;

	public void AddExperience()
	{
		PlayerLevel.AddExperience(ExperienceToAdd);
	}
}
