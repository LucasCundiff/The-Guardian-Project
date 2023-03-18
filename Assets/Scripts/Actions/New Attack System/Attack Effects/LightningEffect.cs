using System;
using UnityEngine;

public class LightningEffect : OnHitAttackEffect
{
	[Range(0.1f, 2f)]
	public float PowerMultiplier;
	[Range(5f, 20f)]
	public float EffectRange = 10f;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		var cTarget = (CharacterStats)target;

		if (cTarget)
		{
			foreach (CharacterStats character in CharacterTracker.Instance.AllCharacters)
			{
				if (DetermineDistance(character) && character.Faction != source.Faction)
				{
					character.TakeDamage(power * PowerMultiplier);
				}
			}
		}
	}

	private bool DetermineDistance(CharacterStats character)
	{
		var distance = character.transform.position - transform.position;

		return distance.magnitude < EffectRange;
	}
}
