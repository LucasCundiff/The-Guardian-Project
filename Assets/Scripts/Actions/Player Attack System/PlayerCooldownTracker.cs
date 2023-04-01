using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackCooldown
{
	public AttackCooldown(string attackName, float cooldown)
	{
		AttackName = attackName;
		Cooldown = cooldown;
	}

	public string AttackName;
	public float Cooldown;
}

public class PlayerCooldownTracker : MonoBehaviour
{
	public static PlayerCooldownTracker Instance;

	public List<AttackCooldown> AttackCooldowns = new List<AttackCooldown>();

	public void Start()
	{
		if (Instance)
			Destroy(Instance);

		Instance = this;
	}

	public void Update()
	{
		for (int i = AttackCooldowns.Count - 1; i >= 0; i--)
		{
			AttackCooldowns[i].Cooldown -= Time.deltaTime;

			if (AttackCooldowns[i].Cooldown <= 0)
				AttackCooldowns.RemoveAt(i);
		}
	}

	public bool IsAttackOnCooldown(BaseAttack attack)
	{
		foreach (AttackCooldown cooldown in AttackCooldowns)
		{
			if (cooldown.AttackName == attack.name)
				return true;
		}

		return false;
	}

	public void StartAttackCooldown(BaseAttack attack, float cooldownTime)
	{
		foreach (AttackCooldown cooldown in AttackCooldowns)
		{
			if (cooldown.AttackName == attack.name)
				return;
		}

		AttackCooldowns.Add(new AttackCooldown(attack.name, cooldownTime));
		Debug.Log($"Starting cooldown for {attack.name}");
	}

	public float GetCooldownTime(BaseAttack attack)
	{
		foreach (AttackCooldown cooldown in AttackCooldowns)
		{
			if (cooldown.AttackName == attack.name)
				return cooldown.Cooldown;
		}

		return Mathf.Infinity;
	}
}
