using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstantAttack : BaseInstantAttack
{
	[SerializeField] GameObject projectileAmmo;
	[SerializeField] Transform spawnPoint;

	protected override void StartAttack()
	{
		base.StartAttack();
		var ammo = Instantiate(projectileAmmo, spawnPoint, false).GetComponent<ProjectileAmmo>();
		ammo.transform.SetParent(null);
		ammo.InitializeAmmo(User, this);
	}

	protected override IEnumerator EndAttack()
	{
		return base.EndAttack();
	}
}
