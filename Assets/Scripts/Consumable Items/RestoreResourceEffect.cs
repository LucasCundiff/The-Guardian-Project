using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effect/Restore Resource")]
public class RestoreResourceEffect : ConsumableItemEffect
{
	[SerializeField] float healthRestore, staminaRestore, manaRestore;

	public override void UseEffect(CharacterStats user)
	{
		if (healthRestore != 0)
			user.CurrentHealth += healthRestore;

		if (staminaRestore != 0)
			user.CurrentStamina += staminaRestore;

		if (manaRestore != 0)
			user.CurrentMana += manaRestore;
	}
}
