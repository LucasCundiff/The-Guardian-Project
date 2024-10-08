using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Effects/Splash Damage")]
public class SplashDamageEffect : OnHitAttackEffect
{
	[Range(5f, 20f)]
	public float EffectRange = 10f;
	public bool FriendlyFire;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		var cTarget = (CharacterStats)target;
		var truePower = power * source.Stats[13].CurrentValue;

		if (cTarget)
		{
			foreach (CharacterStats character in CharacterTracker.Instance.AllCharacters)
			{
				if (DetermineDistance(character, cTarget))
				{
					if (!FriendlyFire && character.Faction == source.Faction) continue;

					var damage = Mathf.Clamp(truePower - truePower * character.Stats[9].CurrentValue * 0.002f, 1f, Mathf.Infinity);
					character.TakeDamage(damage);
				}
			}
		}
		else
			target.TakeDamage(truePower);
	}

	private bool DetermineDistance(CharacterStats character, CharacterStats cTarget)
	{
		var distance = character.transform.position - cTarget.transform.position;
		return distance.magnitude < EffectRange;
	}
}
