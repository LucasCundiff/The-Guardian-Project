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
			Debug.Log($"{cTarget} target was found");
			foreach (CharacterStats character in CharacterTracker.Instance.AllCharacters)
			{
				if (DetermineDistance(character, cTarget) && character.Faction != source.Faction)
				{
					character.TakeDamage(power * PowerMultiplier);
					Debug.Log($"{character} taking {power * PowerMultiplier} lightning damage");

				}
			}

			return;
		}

		Debug.Log($"No targets found for lightning damage");
	}

	private bool DetermineDistance(CharacterStats character, CharacterStats cTarget)
	{
		var distance = cTarget.transform.position - character.transform.position;
		Debug.Log($"{character} is {distance} away from {cTarget}");
		return distance.magnitude < EffectRange;
	}
}
