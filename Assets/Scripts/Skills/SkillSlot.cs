using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	protected Skill _skill;
	public Skill Skill
	{
		get { return _skill; }
		set
		{
			if (_skill == value) return;

			if (_skill)
				_skill.OnSkillLevelUpEvent -= UpdateSlotUI;

			_skill = value;

			if (_skill)
			{
				_skill.OnSkillLevelUpEvent += UpdateSlotUI;
				SlotImage.sprite = _skill.SkillSprite;
				UpdateSlotUI(_skill.CurrentLevel);
			}
		}
	}

	[SerializeField] Image SlotImage;

	public Action<SkillSlot> OnRightClickedEvent;
	public Action<SkillSlot> OnLeftClickedEvent;
	public Action<SkillSlot> OnBeginDragEvent;
	public Action<SkillSlot> OnDragEvent;
	public Action<SkillSlot> OnEndDragEvent;

	private Color lockedColor = new Color(0.3f, 0.3f, 0.3f, 1f);

	private void UpdateSlotUI(int currentLevel)
	{
		SlotImage.color = currentLevel > 0 ? Color.white : lockedColor;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
			OnRightClickedEvent?.Invoke(this);

		if (eventData.button == PointerEventData.InputButton.Left)
			OnLeftClickedEvent?.Invoke(this);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (Skill.CurrentLevel <= 0) return;

		OnBeginDragEvent?.Invoke(this);
	}

	public void OnDrag(PointerEventData eventData)
	{
		OnDragEvent?.Invoke(this);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		OnEndDragEvent?.Invoke(this);
	}
}
