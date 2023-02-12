using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
	void TickState();
	void OnEnterState();
	void OnExitState();
}
