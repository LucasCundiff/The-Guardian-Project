using System.Collections;
using UnityEngine;

public class NecroticEffect : OnHitAttackEffect
{
	[Range(10, 60)]
	public int EffectDuration;
	[Range(.05f, 0.2f)]
	public float HealthPercentDamage;
	[Range(-0.5f, -2f)]
	public float RegenerationDebuff;

	private CharacterStats _characterCache;
	private StatModifier _modifierCache;

	public override void InitializeEffect(IDamageable target, float damage, CharacterStats source)
	{
		_characterCache = (CharacterStats)target;

		if (_characterCache)
		{
			_characterCache.TakeDamage(_characterCache.CurrentHealth * HealthPercentDamage);
			_modifierCache = new StatModifier(RegenerationDebuff, StatModType.Flat, source);
			_characterCache.Stats[9].AddModifier(_modifierCache);
			_characterCache?.StartCoroutine(RemoveDebuff(_characterCache.Stats[9], _modifierCache));
		}
	}

	public IEnumerator RemoveDebuff(Stat stat, StatModifier modifier)
	{
		yield return new WaitForSeconds(EffectDuration);

		stat.RemoveModifier(modifier);
	}
}
