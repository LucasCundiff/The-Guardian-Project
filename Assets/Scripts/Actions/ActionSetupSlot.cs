using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionSetupSlot : ActionSlot, IDropHandler
{
	public Action<ActionSetupSlot> OnSkillDroppedEvent;

	public void OnDrop(PointerEventData eventData) => OnSkillDroppedEvent?.Invoke(this);
}
