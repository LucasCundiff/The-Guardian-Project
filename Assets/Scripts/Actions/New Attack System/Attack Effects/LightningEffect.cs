using UnityEngine;

public class LightningEffect : OnHitAttackEffect
{
	[Range(0.1f, 2f)]
	public float DamageMultiplier;
	public GameObject LightningEffectPrefab;

	private CharacterStats user;
	private float effectDamage;
	private LightningEffect currentInstance;

	private CharacterStats _characterCache;
	private IDamageable _damageableCache;

	public override void InitializeEffect(IDamageable target, float damage, CharacterStats source)
	{
		_characterCache = (CharacterStats)target;

		if (_characterCache)
		{
			currentInstance = Instantiate(LightningEffectPrefab, _characterCache.transform).GetComponent<LightningEffect>();
			currentInstance.user = source;
			currentInstance.effectDamage = _characterCache.Stats[0].CurrentValue * DamageMultiplier;

			Destroy(currentInstance.gameObject, 0.3f);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		ShockAOE(other);
	}

	private void ShockAOE(Collider possibleTarget)
	{
		_damageableCache = possibleTarget.GetComponent<IDamageable>();

		if (_damageableCache != null && (CharacterStats)_damageableCache != user)
		{
			_damageableCache.TakeDamage(effectDamage);
		}
	}
}
