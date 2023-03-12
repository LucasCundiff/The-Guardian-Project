using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableItemEffect : ScriptableObject
{
	public abstract void UseEffect(CharacterStats user);
}
