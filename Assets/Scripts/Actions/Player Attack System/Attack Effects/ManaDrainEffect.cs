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
			drainAmount = Mathf.Clamp(drainAmount - drainAmount * cTarget.Stats[10].CurrentValue * 0.002f, 0, Mathf.Infinity);
			cTarget.CurrentMana -= drainAmount;
		}
	}
}
