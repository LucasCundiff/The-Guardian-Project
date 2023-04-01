using UnityEngine;

[CreateAssetMenu(menuName = "Attack Effects/Mana Drain")]
public class ManaDrainEffect : OnHitAttackEffect
{
	public bool IgnorePower = false;
	[Range(0, 1000)]
	[Tooltip("Only use if ignoring power")]
	public float ManaDrainAmount;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		var cTarget = (CharacterStats)target;

		if (cTarget)
		{
			var drainAmount = IgnorePower ? ManaDrainAmount : power;
			cTarget.CurrentMana -= drainAmount;
		}
	}
}
