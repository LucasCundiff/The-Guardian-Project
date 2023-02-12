using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
	public abstract void InitializeEffect(IDamageable target, float damage, CharacterStats source);
}
