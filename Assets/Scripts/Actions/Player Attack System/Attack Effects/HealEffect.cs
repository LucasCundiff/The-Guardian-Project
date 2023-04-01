using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Effects/Heal")]
public class HealEffect : OnHitAttackEffect
{
	public bool IgnorePower = false;
	[Range(0, 1000)]
	[Tooltip("Only use if ignoring power")]
	public float HealAmount;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		var healAmount = IgnorePower ? HealAmount : power;
		target.Heal(healAmount);
	}
}
