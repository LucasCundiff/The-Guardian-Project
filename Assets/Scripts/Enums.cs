public enum ItemType
{
	Basic,
	Equipment,
	Usable,
	Skill,
}

public enum ArmorType
{
	Head,
	Neck,
	Chest,
	Hands,
	Shoulders,
	Waist,
	Legs,
	Feet,
	Artifact,
	None,
}

public enum ResourceType
{
	Health,
	Stamina,
	Mana,
	HealthShield,
	EXP,
}

public enum StatModType
{
	Flat = 100,
	PercentAdd = 200,
	PercentMult = 300,
}

public enum SkillType
{
	ManaSkill,
	StaminaSkill,
}

public enum DamageType
{
	Physical, //Increase damage by x% 
	Arcane, //Reduces target's resistance and drains target's mana
	Fire, //Burn DoT for x% damage dealt
	Shock, //Damages enemies in x radius for x% of damage
	Frost, //Slow target by x% for x seconds and drains target's stamina
	Chaos, //Increase damage by ((1 + x)^num of times status proc)%
	Holy, //Reduces target's armor and grants damage shield
	Necrotic, //Decreases max health by x% for x seconds
}

public enum ActionState
{
	Unarmed,
	Weapon,
	Skill,
}

public enum AIState
{
	Idle,
	Searching,
	Wander,
	Follow,
	Catchup,
	Chase,
	Attack,
	Dead,
	None,
}