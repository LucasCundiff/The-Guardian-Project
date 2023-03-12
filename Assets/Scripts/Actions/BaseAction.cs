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

	[Range(0, 255)]
	public int Power;
	[Range(1, 100)]
	public int CriticalChance;
	[Range(1f, 10f)]
	public float CriticalMultiplier;

	public bool IsAttacking { get; private set; }

	public void InitializeAction(PlayerInputManager input, CharacterStats characterStats)
	{
		User = characterStats;

		Primary?.InitializeAttack(input.PlayerInput.Player.Primary, this, User);
		Secondary?.InitializeAttack(input.PlayerInput.Player.Secondary, this, User);
		Tertiary?.InitializeAttack(input.PlayerInput.Player.Tertiary, this, User);
		Quaternary?.InitializeAttack(input.PlayerInput.Player.Quaternary, this, User);
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
