using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Effects/Stamina Drain")]
public class StaminaDrainEffect : OnHitAttackEffect
{
	public bool IgnorePower = false;
	[Range(0, 1000)]
	[Tooltip("Only use if ignoring power")]
	public float StaminaDrainAmount;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		var cTarget = (CharacterStats)target;

		if (cTarget)
		{
			var drainAmount = IgnorePower ? StaminaDrainAmount : power;
			cTarget.CurrentStamina -= drainAmount;
		}
	}
}
