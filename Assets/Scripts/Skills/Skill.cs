using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Skill : ScriptableObject
{
	[Range(1, 5)]
	public int MaxLevel;
	public int CurrentLevel = 0;
	public Sprite SkillSprite;
	public string SkillName;
	public SkillType SkillType;
	public BaseAction SkillGameObject;

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

	public virtual string GetSkillDestription()
	{
		if (!SkillGameObject) return "";

		var sb = new StringBuilder();

		if (SkillGameObject.Primary)
		{
			sb.Append(SkillGameObject.Primary.gameObject.name);
			sb.AppendLine();
		}
		if (SkillGameObject.Secondary)
		{
			sb.Append(SkillGameObject.Secondary.gameObject.name);
			sb.AppendLine();
		}
		if (SkillGameObject.Tertiary)
		{
			sb.Append(SkillGameObject.Tertiary.gameObject.name);
			sb.AppendLine();
		}
		if (SkillGameObject.Quaternary)
			sb.Append(SkillGameObject.Quaternary.gameObject.name);

		return sb.ToString();
	}
}
