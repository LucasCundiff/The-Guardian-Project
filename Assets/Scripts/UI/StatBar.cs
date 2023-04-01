using System;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
	[SerializeField] ResourceType statType;
	[SerializeField] Image statBarFill;
	[SerializeField] CharacterStats user;
	[SerializeField] PlayerLevel playerLevel;

	private float currentAmount;

	public void Start()
	{
		switch (statType)
		{
			case ResourceType.Health:
				user.HealthChangedEvent += UpdateValue;
				UpdateValue(user.CurrentHealth, user.MaxHealth);
				break;
			case ResourceType.Mana:
				user.ManaChangedEvent += UpdateValue;
				UpdateValue(user.CurrentMana, user.MaxMana);
				break;
			case ResourceType.Stamina:
				user.StaminaChangedEvent += UpdateValue;
				UpdateValue(user.CurrentStamina, user.MaxStamina);
				break;
			case ResourceType.HealthShield:
				user.HealthShieldChangedEvent += UpdateValue;
				UpdateValue(user.HealthShield, user.MaxHealth);
				break;
			case ResourceType.EXP:
				playerLevel.OnGainExperienceEvent += EventHelper;
				EventHelper(playerLevel.CurrentExperience, playerLevel.ExperienceNeeded);
				break;
			default:
				break;
		}
	}

	public void EventHelper(int current, int max)
	{
		UpdateValue(current, max);
	}

	public void UpdateValue(float currentValue, float maxValue)
	{
		if (!statBarFill) return;

		if (currentValue != 0 && maxValue != 0)
			statBarFill.fillAmount = currentValue / maxValue;
		else
			statBarFill.fillAmount = 0;
	}
}
