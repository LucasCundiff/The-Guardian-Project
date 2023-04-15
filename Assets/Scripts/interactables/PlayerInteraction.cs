using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
	[SerializeField] float interactDistance;
	[SerializeField] LayerMask interactLayer;
	[SerializeField] PlayerInputManager playerInput;
	[SerializeField] CharacterStats player;
	[SerializeField] TextMeshProUGUI interactionText;
	[SerializeField] GameObject interactionTextParent;

	protected IInteractable currentInteraction;

	protected void Start()
	{
		playerInput.PlayerInput.Player.Interact.performed += Interact;
	}

	private void Interact(InputAction.CallbackContext context)
	{
		if (currentInteraction != null)
		{
			Debug.Log($"Interacting with {currentInteraction}!");
			currentInteraction.Interact();
			currentInteraction = null;
		}
	}

	protected void Update()
	{
		var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

		Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);
		var hitSomething = Physics.Raycast(ray, out var hit, interactDistance, interactLayer, QueryTriggerInteraction.Ignore);
		if (hitSomething)
		{
			currentInteraction = hit.collider.gameObject.GetComponent<IInteractable>();
			ToggleInteractionText(true);
		}
		else
		{
			ToggleInteractionText(false);
			currentInteraction = null;
			interactionText.text = null;
		}
	}

	private void ToggleInteractionText(bool state)
	{
		if (state && currentInteraction != null)
		{
			interactionText.text = currentInteraction.InteractDescription();
			interactionTextParent.SetActive(true);
		}
		else
			interactionTextParent.SetActive(false);
	}
}

