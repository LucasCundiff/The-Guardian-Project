using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
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
				RefreshEvents();
			}
		}
	}

	[SerializeField] Image SlotImage;

	public Action<SkillSlot> OnPointerEnterEvent;
	public Action<SkillSlot> OnPointerExitEvent;
	public Action<SkillSlot> OnRightClickedEvent;
	public Action<SkillSlot> OnLeftClickedEvent;
	public Action<SkillSlot> OnBeginDragEvent;
	public Action<SkillSlot> OnDragEvent;
	public Action<SkillSlot> OnEndDragEvent;

	protected Color lockedColor = new Color(0.3f, 0.3f, 0.3f, 1f);
	protected bool isPointerOver;

	private void RefreshEvents()
	{
		if (isPointerOver)
		{
			OnPointerExit(null);
			OnPointerEnter(null);
		}
	}

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

	public void OnPointerEnter(PointerEventData eventData)
	{
		isPointerOver = true;
		OnPointerEnterEvent?.Invoke(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerOver = false;
		OnPointerExitEvent?.Invoke(this);
	}
}
