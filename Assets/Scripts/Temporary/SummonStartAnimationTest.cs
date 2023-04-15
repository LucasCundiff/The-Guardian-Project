using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonStartAnimationTest : MonoBehaviour
{
	[SerializeField] BaseSummonAI summonAI;

	public void Awake()
	{
		summonAI.enabled = false;
	}

	public void EndOfAnimationEvent()
	{
		summonAI.enabled = true;
	}
}
