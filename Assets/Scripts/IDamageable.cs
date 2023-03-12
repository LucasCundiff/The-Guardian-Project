using System;

public interface IDamageable
{
	bool TakeDamage(float damage);
	bool Heal(float healAmount);
	void Die();
}
