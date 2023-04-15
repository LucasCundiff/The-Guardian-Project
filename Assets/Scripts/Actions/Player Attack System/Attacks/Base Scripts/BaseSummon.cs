using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSummon : MonoBehaviour
{
	[Range(20, 300)]
	public float SummonDuration;
	[Range(1, 15)]
	public int SummonLimit = 1;
	public string SummonName;

	protected CharacterStats user;
	protected BaseAttack parentAttack;

	public virtual void Summon(CharacterStats currentUser, BaseAttack attack)
	{
		user = currentUser;
		parentAttack = attack;

		PlayerSummonTracker.Instance.AddSummon(this);
		StartCoroutine(UnsummonCoroutine());
	}

	private IEnumerator UnsummonCoroutine()
	{
		yield return new WaitForSeconds(SummonDuration);
		Unsummon();
	}

	public virtual void Unsummon()
	{
		PlayerSummonTracker.Instance.RemoveSummon(this);
		Destroy(gameObject);
	}
}
