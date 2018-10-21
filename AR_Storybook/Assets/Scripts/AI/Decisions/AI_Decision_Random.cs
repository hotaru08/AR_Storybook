using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.AI;

[CreateAssetMenu(menuName = "AI/Decision/Random Chance")]
public class AI_Decision_Random : AI_Decision
{
	[Range(0f, 1f)][SerializeField] float chance;

	public override bool Decide(AI_Controller controller)
	{
		return RandomChance(controller);
	}

	public override void Reset()
	{
		
	}

	private bool RandomChance(AI_Controller controller)
	{
		return Random.Range(0f, 1f) >= chance;
	}
}
