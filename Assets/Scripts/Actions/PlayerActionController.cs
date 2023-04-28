using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerActionController : MonoBehaviour
{
	[SerializeField] protected CharacterStats user;
	[SerializeField] protected ActionBar ActionBar;
	[SerializeField] protected ActionBarState currentActionState = ActionBarState.Unarmed, defaultState = ActionBarState.Weapon;
	[SerializeField] protected GameObject unarmedAction;
	[SerializeField] protected Transform actionSpawnTransform;

	protected GameObject actionGameObject;
	protected ActionSlot currentActionSlot;

	private void Start()
	{
		PlayerInputManager.Instance.PlayerInput.Player.ActionStateToggle.performed += ToggleActionState;
		ActionBar.OnActionSlotChanged += SetActionSlot;
		LoadAction();
	}

	private void ToggleActionState(InputAction.CallbackContext obj)
	{
		if (obj.interaction is HoldInteraction)
			ActionHoldSwitch();
		else
			ActionPressedSwitch();
	}

	private void ActionPressedSwitch()
	{
		switch (currentActionState)
		{
			case ActionBarState.Unarmed:
				SetActionState(defaultState);
				break;
			case ActionBarState.Weapon:
				SetActionState(ActionBarState.Skill);
				break;
			case ActionBarState.Skill:
				SetActionState(ActionBarState.Weapon);
				break;
			default:
				break;
		}
	}

	private void ActionHoldSwitch()
	{
		if (currentActionState == ActionBarState.Unarmed)
			SetActionState(defaultState);
		else
		{
			defaultState = currentActionState;
			SetActionState(ActionBarState.Unarmed);
		}
	}

	public void SetActionSlot(ActionSlot newSlot)
	{
		currentActionSlot = newSlot;
		LoadAction();
	}

	private void SetActionState(ActionBarState actionState)
	{
		if (currentActionState == actionState) return;

		currentActionState = actionState;
		LoadAction();
	}

	public void LoadAction()
	{
		if (currentActionSlot == null) return;

		if (actionGameObject)
		{
			actionGameObject.GetComponent<BaseAction>()?.DeinitializeAction();
			var objectToDestroy = actionGameObject;
			actionGameObject = null;
			Destroy(objectToDestroy, 0.1f);
		}

		GameObject objectToInstansiate = null;

		switch (currentActionState)
		{
			case ActionBarState.Unarmed:
				if (unarmedAction)
					objectToInstansiate = unarmedAction;
				break;
			case ActionBarState.Weapon:
				if (currentActionSlot.CurrentWeapon)
					objectToInstansiate = currentActionSlot.CurrentWeapon.WeaponObject.gameObject;
				break;
			case ActionBarState.Skill:
				if (currentActionSlot.CurrentSkill)
					objectToInstansiate = currentActionSlot.CurrentSkill.SkillGameObject.gameObject;
				break;
			default:
				break;
		}

		if (objectToInstansiate)
		{
			actionGameObject = Instantiate(objectToInstansiate, actionSpawnTransform);
			actionGameObject.GetComponent<BaseAction>()?.InitializeAction(user);
		}
	}
} 
