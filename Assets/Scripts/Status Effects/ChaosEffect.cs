using System.Collections;
using UnityEngine;

public class ChaosEffect : StatusEffect
{
	[Range(5, 30)]
	public int EffectDuration;
	[Range(0.01f, 0.1f)]
	public float DamageMultiplier;
	public int CurrentStacks;

	public GameObject ChaosEffectPrefab;

	private int currentTime;
	private ChaosEffect currentInstance;

	private CharacterStats _characterCache;

	private void Start()
	{
		StartCoroutine(RemoveEffect());
	}

	public override void InitializeEffect(IDamageable target, float damage, CharacterStats source)
	{
		_characterCache = (CharacterStats)target;

		if (_characterCache)
		{
			currentInstance = _characterCache.GetComponentInChildren<ChaosEffect>();

			if (currentInstance)
			{
				UpdateCurrentInstance();
				_characterCache.TakeDamage(damage * Mathf.Pow(1 + DamageMultiplier, currentInstance.CurrentStacks - damage));

			}
			else
			{
				CreateNewInstance();
				_characterCache.TakeDamage(damage * Mathf.Pow(1 + DamageMultiplier, currentInstance.CurrentStacks) - damage);
			}
		}
	}

	private void CreateNewInstance()
	{
		currentInstance = Instantiate(ChaosEffectPrefab, _characterCache.transform).GetComponent<ChaosEffect>();
		currentInstance.CurrentStacks = 1;
	}

	private void UpdateCurrentInstance()
	{
		currentInstance.currentTime = 0;
		currentInstance.CurrentStacks++;
	}

	private IEnumerator RemoveEffect()
	{
		currentTime = 0;

		while(currentTime < EffectDuration)
		{
			currentTime++;

			yield return new WaitForSeconds(1f);
		}

		Destroy(gameObject);
	}
}
