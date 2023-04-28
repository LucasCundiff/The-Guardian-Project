using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseAction : MonoBehaviour
{
	public CharacterStats User { get; private set; }

	public BaseAttack Primary;
	public BaseAttack Secondary;
	public BaseAttack Tertiary;
	public BaseAttack Quaternary;
	[Range(.05f, 12.5f)]
	public float PowerMultiplier = 1f;
	[Range(1, 100)]
	public int CriticalChance;
	[Range(1f, 10f)]
	public float CriticalMultiplier;

	public bool IsAttacking { get; private set; }

	public void InitializeAction(CharacterStats characterStats)
	{
		User = characterStats;

		Primary?.InitializeAttack(PlayerInputManager.Instance.PlayerInput.Player.Primary, this, User);
		Secondary?.InitializeAttack(PlayerInputManager.Instance.PlayerInput.Player.Secondary, this, User);
		Tertiary?.InitializeAttack(PlayerInputManager.Instance.PlayerInput.Player.Tertiary, this, User);
		Quaternary?.InitializeAttack(PlayerInputManager.Instance.PlayerInput.Player.Quaternary, this, User);
	}

	public void DeinitializeAction()
	{
		Primary?.DeinitializeAttack();
		Secondary?.DeinitializeAttack();
		Tertiary?.DeinitializeAttack();
		Quaternary?.DeinitializeAttack();
	}


	public IEnumerator StartAttackCooldown(float cooldownTime)
	{
		IsAttacking = true; 
		
		yield return new WaitForSeconds(cooldownTime);

		IsAttacking = false;
	}

	public void ToggleAttackCooldown(bool state)
	{
		IsAttacking = state;
	}
}
