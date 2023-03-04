using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Attack : MonoBehaviour
{
	[Range(0.33f, 3f)]
	public float AttackTime;

	protected CharacterStats currentUser;
	protected BaseAction currentAction;
	protected InputAction attackInput;

	public abstract void InitializeAttack(InputAction input, BaseAction action, CharacterStats user);
	public abstract void DeinitializeAttack();
	public abstract bool PayAttackCost();
}
