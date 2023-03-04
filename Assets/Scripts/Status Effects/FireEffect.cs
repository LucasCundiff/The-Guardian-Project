using System.Collections;
using UnityEngine;

public class FireEffect : OnHitAttackEffect
{
	[Range(.05f, .2f)]
	public float DamageHealthPercent;
	[Range(2, 12)]
	public int EffectDuration;

	private float _damagePerTick;
	private CharacterStats _characterCache;

	public override void InitializeEffect(IDamageable target, float damage, CharacterStats source)
	{
		_characterCache = (CharacterStats)target;

		if (_characterCache)
			_damagePerTick = _characterCache.Stats[0].CurrentValue * DamageHealthPercent / EffectDuration;
		else
			_damagePerTick = damage * (1 + DamageHealthPercent) / EffectDuration;

		source?.StartCoroutine(BurnDOT(target, _damagePerTick));
	}

	protected IEnumerator BurnDOT(IDamageable target, float damagePerTick)
	{
		int currentDuration = 0;

		while (currentDuration < EffectDuration)
		{
			target?.TakeDamage(damagePerTick);
			currentDuration++;

			yield return new WaitForSeconds(1f);
		}
	}
}
