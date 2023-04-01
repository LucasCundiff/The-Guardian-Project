using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnHitAttackEffect : ScriptableObject
{
	public abstract void InitializeEffect(IDamageable target, float power, CharacterStats source);
}
