using System;
using System.Collections;
using UnityEngine;

public class FrostEffect : StatusEffect
{
	[Range(10f, 60f)]
	public int EffectDuration;
	[Range(-0.05f, -3f)]
	public float MovementSpeedDebuff;
	[Range(0.05f, 0.2f)]
	public float DrainStaminaPercent;

	private CharacterStats _characterCache;
	private StatModifier _modifierCache;
	private float _floatCache;

	public override void InitializeEffect(IDamageable target, float damage, CharacterStats source)
	{
		_characterCache = (CharacterStats)target;

		if (_characterCache)
		{
			_floatCache = _characterCache.Stats[2].CurrentValue * DrainStaminaPercent;
			_characterCache.CurrentStamina -= _floatCache;

			if (source)
				source.CurrentStamina += _floatCache;

			_modifierCache = new StatModifier(MovementSpeedDebuff, StatModType.Flat, source);
			_characterCache.Stats[10].AddModifier(_modifierCache);
			_characterCache.StartCoroutine(RemoveDebuff(_characterCache.Stats[10], _modifierCache));
		}
	}

	private IEnumerator RemoveDebuff(Stat stat, StatModifier modifier)
	{
		yield return new WaitForSeconds(EffectDuration);

		stat.RemoveModifier(modifier);
	}
}
