using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Effects/Resource Restore")]
public class ResourceRestoreEffect : OnHitAttackEffect
{
	public ResourceType ResourceType;

	public bool IgnorePower = false;
	[Range(0, 1000)]
	[Tooltip("Only use if ignoring power")]
	public float RestoreAmount;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		if (source)
		{
			var restoreAmount = IgnorePower ? RestoreAmount : power;
			switch (ResourceType)
			{
				case ResourceType.Health:
					source.CurrentHealth += restoreAmount;
					break;
				case ResourceType.Stamina:
					source.CurrentStamina += restoreAmount;
					break;
				case ResourceType.Mana:
					source.CurrentMana += restoreAmount;
					break;
				case ResourceType.HealthShield:
					source.HealthShield += restoreAmount;
					break;
				default:
					break;
			}
		}
	}
}
