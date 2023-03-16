using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
	[Range(1, 5)]
	public int MaxLevel;
	public int CurrentLevel = 0;
	public Sprite SkillSprite;
	public string SkillName;
	[TextArea]
	public string SkillAttackList;
	public SkillType SkillType;
	public GameObject SkillGameObject;

	public Action<int> OnSkillLevelUpEvent;

	public void LevelUpSkill()
	{
		CurrentLevel++;
		OnSkillLevelUpEvent?.Invoke(CurrentLevel);
	}

	public virtual Skill GetCopy()
	{
		return this;
	}
}
