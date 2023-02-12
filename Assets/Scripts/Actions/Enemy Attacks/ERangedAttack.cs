using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERangedAttack : EnemyAttack
{
	[SerializeField] protected GameObject projectilePrefab;

	public override void UseAttack()
	{
		gameObject.SetActive(true);
		base.UseAttack();

		var projectile = Instantiate(projectilePrefab, transform).GetComponent<ERangedAmmo>();
		if (projectile)
		{
			projectile.transform.SetParent(null);
			projectile.Initialize(user);
		}

		Invoke("DisableAttack", attackTime);
	}

	public void DisableAttack() => gameObject.SetActive(false);
}
