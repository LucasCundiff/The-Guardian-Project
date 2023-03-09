using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileAmmo : MonoBehaviour
{
	[SerializeField] Vector3 projectileDirection = Vector3.forward;
	[SerializeField] Rigidbody rb;
	[SerializeField] ForceMode forceType;
	[SerializeField] float projectileSpeed;

	protected CharacterStats currentUser;
	protected BaseAttack currentBaseAttack;

	public virtual void InitializeAmmo(CharacterStats user, BaseAttack baseAttack)
	{
		if (!rb) rb = GetComponent<Rigidbody>();

		currentUser = user;
		currentBaseAttack = baseAttack;

		rb.AddRelativeForce(projectileDirection * projectileSpeed, forceType);
	}

	private void OnTriggerEnter(Collider other)
	{
		var target = other.GetComponent<IDamageable>();

		if (target != null && (CharacterStats)target != currentUser)
		{
			var power = currentBaseAttack.DeterminePower();

			foreach (OnHitAttackEffect hitAttackEffect in currentBaseAttack.OnTargetHitEffects)
			{
				hitAttackEffect.InitializeEffect(target, power, currentUser);
			}
		}

		Destroy(gameObject);
	}
}
