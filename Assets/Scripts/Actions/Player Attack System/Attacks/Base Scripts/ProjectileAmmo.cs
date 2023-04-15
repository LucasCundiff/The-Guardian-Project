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
	[SerializeField] float projectileDuration = 10f;
	[SerializeField] List<int> stopProjectileLayers = new List<int> { 6, 7 };

	protected CharacterStats user;
	protected BaseAttack currentAttack;

	public virtual void InitializeAmmo(CharacterStats user, BaseAttack baseAttack)
	{
		if (!rb) rb = GetComponent<Rigidbody>();

		this.user = user;
		currentAttack = baseAttack;

		rb.AddRelativeForce(projectileDirection * projectileSpeed, forceType);
		StartCoroutine(SelfDestruct());
	}

	protected virtual IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(projectileDuration);
		EndProjectile();
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		currentAttack.PossibleTargetHit(other);
		CheckStopLayer(other);
	}

	protected virtual void CheckStopLayer(Collider other)
	{
		foreach (int layerValue in stopProjectileLayers)
		{
			if (layerValue == other.gameObject.layer)
				EndProjectile();
		}
	}

	protected virtual void EndProjectile()
	{
		Destroy(gameObject);
	}
}
