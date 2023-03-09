using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstantAttack : BaseInstantAttack
{
	[SerializeField] GameObject projectileAmmo;
	[SerializeField] Transform spawnPoint;

	protected override void StartAttack()
	{
		var ammo = Instantiate(projectileAmmo, spawnPoint, false).GetComponent<ProjectileAmmo>();
		ammo.transform.SetParent(null);
		ammo.InitializeAmmo(CurrentUser, this);
		base.StartAttack();
	}

	protected override IEnumerator EndAttack()
	{
		return base.EndAttack();
	}
}
