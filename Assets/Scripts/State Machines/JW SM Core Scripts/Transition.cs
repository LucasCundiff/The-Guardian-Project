using System;
using UnityEngine;

public class Transition 
{
	public IState TargetState;
	public Func<bool> TransitionCondtion;
	
	public Transition(IState targetState, Func<bool> condtion)
	{
		TargetState = targetState;
		TransitionCondtion = condtion;
	}
}
