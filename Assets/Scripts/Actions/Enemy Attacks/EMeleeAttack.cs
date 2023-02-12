using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMeleeAttack : EnemyAttack
{
	[SerializeField] protected float damage;

	public override void UseAttack()
	{
		base.UseAttack();
		gameObject.SetActive(true);
		Invoke("DisableAttack", 0.2f);
	}

	protected void DisableAttack()
	{
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		var target = other.GetComponent<CharacterStats>();

		if (target && target.Faction != user.Faction)
		{
			target.TakeDamage(damage);
		}
	}
}
