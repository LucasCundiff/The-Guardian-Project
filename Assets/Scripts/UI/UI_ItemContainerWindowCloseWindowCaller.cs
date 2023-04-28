using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ItemContainerWindowCloseWindowCaller : MonoBehaviour
{
	public InteractableUI_ItemContainerWindow containerWindow;

	private void OnDisable()
	{
		containerWindow?.CloseWindow();
	}
}
