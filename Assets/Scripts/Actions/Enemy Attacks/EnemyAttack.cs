using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	[SerializeField] protected string attackName;
	[SerializeField] protected float attackTime;
	[SerializeField] protected CharacterStats user;

	public float AttackTime { get { return attackTime; } }

	public void InitializeAttack(CharacterStats user) => this.user = user;

	public virtual void UseAttack()
	{
		Debug.Log($"{user} is using {attackName}");
	}
}