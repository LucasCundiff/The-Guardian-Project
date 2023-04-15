using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeSummon : BaseSummon
{
	[SerializeField] List<OnHitAttackEffect> attackEffects = new List<OnHitAttackEffect>();

	public override void Summon(CharacterStats currentUser, BaseAttack attack)
	{
		parentAttack = attack;

		base.Summon(currentUser, attack);
	}

	public override void Unsummon()
	{
		base.Unsummon();
	}

	private void OnTriggerStay(Collider other)
	{
		var target = other.GetComponent<IDamageable>();
		if (target != null)
		{
			var power = parentAttack.DeterminePower() * Time.deltaTime;
			foreach (OnHitAttackEffect hitAttackEffect in attackEffects)
			{ 
				hitAttackEffect.InitializeEffect(target, power, user);
			}
		}
	}
}
