using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ERangedAmmo : MonoBehaviour
{
	[SerializeField] protected float damage;
	[SerializeField] protected Vector3 projectileDirection;
	[SerializeField] protected float projectileSpeed;
	[SerializeField] protected ForceMode projectileForce;
	[SerializeField] Rigidbody rb;

	protected CharacterStats user;

	public void Initialize(CharacterStats user)
	{
		this.user = user;
		rb.AddRelativeForce(projectileDirection * projectileSpeed, projectileForce);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other && other.isTrigger) return;

		var target = other.GetComponent<CharacterStats>();

		if (target && target.Faction != user.Faction)
			target.TakeDamage(damage);
		else if (target?.Faction == user.Faction)
			return;

		Destroy(gameObject);
	}
}
