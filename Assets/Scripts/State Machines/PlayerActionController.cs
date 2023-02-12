using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
	[SerializeField] protected PlayerInputManager playerInputManager;
	[SerializeField] protected CharacterStats user;
	[SerializeField] protected ActionBar ActionBar;
	[SerializeField] protected ActionState currentActionState = ActionState.Unarmed, defaultState = ActionState.Weapon;
	[SerializeField] protected GameObject unarmedAction;
	[SerializeField] protected Transform actionSpawnTransform;

	protected GameObject actionGameObject;
	protected ActionSlot currentActionSlot;

	private void Start()
	{
		playerInputManager.PlayerInput.Player.ActionStateToggle.performed += ToggleActionState;
		ActionBar.OnActionSlotChanged += SetActionSlot;
	}

	private void ToggleActionState(InputAction.CallbackContext obj)
	{
		if (obj.control.IsPressed())
			ActionHoldSwitch();
		else
			ActionPressedSwitch();
	}

	private void ActionPressedSwitch()
	{
		switch (currentActionState)
		{
			case ActionState.Unarmed:
				SetActionState(defaultState);
				break;
			case ActionState.Weapon:
				SetActionState(ActionState.Skill);
				break;
			case ActionState.Skill:
				SetActionState(ActionState.Weapon);
				break;
			default:
				break;
		}
	}

	private void ActionHoldSwitch()
	{
		if (currentActionState == ActionState.Unarmed)
			SetActionState(defaultState);
		else
		{
			defaultState = currentActionState;
			SetActionState(ActionState.Unarmed);
		}
	}

	public void SetActionSlot(ActionSlot newSlot)
	{
		currentActionSlot = newSlot;
		LoadAction();
	}

	private void SetActionState(ActionState actionState)
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
			case ActionState.Unarmed:
				if (unarmedAction)
					objectToInstansiate = unarmedAction;
				break;
			case ActionState.Weapon:
				if (currentActionSlot.CurrentWeapon)
					objectToInstansiate = currentActionSlot.CurrentWeapon.weaponGameObject;
				break;
			case ActionState.Skill:
				if (currentActionSlot.CurrentSkill)
					objectToInstansiate = currentActionSlot.CurrentSkill.SkillGameObject;
				break;
			default:
				break;
		}

		if (objectToInstansiate)
		{
			actionGameObject = Instantiate(objectToInstansiate, actionSpawnTransform);
			actionGameObject.GetComponent<BaseAction>()?.InitializeAction(playerInputManager, user);
		}
	}
} 
