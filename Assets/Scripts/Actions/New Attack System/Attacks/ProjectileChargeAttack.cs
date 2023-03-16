using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileChargeAttack : BaseChargeAttack
{
	[SerializeField] GameObject projectileAmmo;
	[SerializeField] Transform spawnPoint;

	protected override IEnumerator ExecuteCharge()
	{
		var ammo = Instantiate(projectileAmmo, spawnPoint, false).GetComponent<ProjectileAmmo>();
		ammo.transform.SetParent(null);
		ammo.InitializeAmmo(CurrentUser, this);

		return base.ExecuteCharge();
	}
}
