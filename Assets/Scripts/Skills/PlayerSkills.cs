using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkills : MonoBehaviour
{
	[SerializeField] SkillTooltip skillTooltip;

	public Action<int> OnSoulPointsChangedEvent;

	public List<SkillSlot> ManaSkillSlots = new List<SkillSlot>();
	public List<SkillSlot> StaminaSkillSlots = new List<SkillSlot>();

	public List<Skill> AllSkills = new List<Skill>();

	protected int _soulPoints;
	public int SoulPoints
	{
		get { return _soulPoints; }
		set
		{
			_soulPoints = value;
			OnSoulPointsChangedEvent?.Invoke(_soulPoints);
		}
	}

	public int StartingSoulPoints = 1;
	public int soulPointsPerLevel = 1;
	public SkillSlot DragSlot;

	private List<SkillSlot> _usedSlots = new List<SkillSlot>();

	[SerializeField] PlayerLevel playerLevel;

	public void Awake()
	{
		SoulPoints = StartingSoulPoints;
		playerLevel.OnLevelUpEvent += () => SoulPoints += soulPointsPerLevel;
		DragSlot.gameObject.SetActive(false);

		IntializeSkills();

		AddEventsToSkillSlots(ManaSkillSlots);
		AddEventsToSkillSlots(StaminaSkillSlots);
	}

	private void IntializeSkills()
	{
		foreach (Skill skill in AllSkills)
		{
			if (skill.SkillType == SkillType.ManaSkill)
				_usedSlots = ManaSkillSlots;
			else if (skill.SkillType == SkillType.StaminaSkill)
				_usedSlots = StaminaSkillSlots;

			foreach (SkillSlot skillSlot in _usedSlots)
			{
				if (skillSlot.Skill == null)
				{
					skillSlot.Skill = skill.GetCopy();
				}
			}
		}
	}

	private void AddEventsToSkillSlots(List<SkillSlot> skillSlots)
	{
		foreach (SkillSlot skillSlot in skillSlots)
		{
			skillSlot.OnPointerEnterEvent += ShowTooltip;
			skillSlot.OnPointerExitEvent += HideTooltip;
			skillSlot.OnLeftClickedEvent += TryLevelUpSkill;
			skillSlot.OnBeginDragEvent += BeginDragSkillSlot;
			skillSlot.OnDragEvent += DuringDragSkillSlot;
			skillSlot.OnEndDragEvent += EndDragSkillSlot;
		}
	}

	private void ShowTooltip(SkillSlot skillSlot)
	{
		skillTooltip.EnableTooltip(skillSlot);
	}

	private void HideTooltip(SkillSlot skillSlot)
	{
		skillTooltip.DisableTooltip(skillSlot);
	}

	public void TryLevelUpSkill(SkillSlot skillSlot)
	{
		if (skillSlot.Skill.CurrentLevel >= skillSlot.Skill.MaxLevel || SoulPoints < 1) return;

		SoulPoints--;
		skillSlot.Skill.LevelUpSkill();
	}

	public void BeginDragSkillSlot(SkillSlot skillSlot)
	{
		DragSlot.Skill = skillSlot.Skill;
		DragSlot.gameObject.SetActive(true);
	}

	public void DuringDragSkillSlot(SkillSlot skillSlot)
	{
		DragSlot.gameObject.transform.position = Mouse.current.position.ReadValue();
	}

	public void EndDragSkillSlot(SkillSlot skillSlot)
	{
		DragSlot.gameObject.SetActive(false);
	}
}
