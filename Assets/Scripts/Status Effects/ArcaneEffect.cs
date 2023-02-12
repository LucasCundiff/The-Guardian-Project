using System;
using System.Collections;
using UnityEngine;

public class ArcaneEffect : StatusEffect
{
	[Range(10f, 60f)]
	public int EffectDuration;
	[Range(-5, -20)]
	public int ResistanceDebuff;
	[Range(0.05f, 0.2f)]
	public float DrainManaPercent;

	private CharacterStats _characterCache;
	private StatModifier _modifierCache;
	private float _floatCache;

	public override void InitializeEffect(IDamageable target, float damage, CharacterStats source)
	{
		_characterCache = (CharacterStats)target;

		if (_characterCache)
		{
			_floatCache = _characterCache.Stats[1].CurrentValue * DrainManaPercent;
			_characterCache.CurrentMana -= _floatCache;

			if (source)
				source.CurrentMana += _floatCache;

			_modifierCache = new StatModifier(ResistanceDebuff, StatModType.Flat, source);
			_characterCache.Stats[8].AddModifier(_modifierCache);
			_characterCache.StartCoroutine(RemoveDebuff(_characterCache.Stats[8], _modifierCache));
		}
	}

	private IEnumerator RemoveDebuff(Stat stat, StatModifier modifier)
	{
		yield return new WaitForSeconds(EffectDuration);

		stat.RemoveModifier(modifier);
	}
}
