using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Effects/Stun")]
public class StunEffect : OnHitAttackEffect
{
	public bool IgnorePower = false;
	[Range(0, 120)]
	[Tooltip("Only use if ignoring power")]
	public float StunDuration;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{      
		//Add a variation for player later and fix later to actually stun

		var cTarget = (CharacterStats)target;
		var aiTarget = cTarget?.GetComponent<AIStateMachine>();

		if (aiTarget)
		{
			var trueDuration = IgnorePower ? StunDuration : power;
			trueDuration = Mathf.Clamp(trueDuration - trueDuration * cTarget.Stats[10].CurrentValue * 0.002f, 0f, Mathf.Infinity);
			source.StartCoroutine(RunStunEffect(aiTarget, trueDuration));
		}

	}

	protected IEnumerator RunStunEffect(AIStateMachine aiTarget, float duration)
	{
		aiTarget.enabled = false;

		yield return new WaitForSeconds(duration);

		aiTarget.enabled = true;
	}
}
