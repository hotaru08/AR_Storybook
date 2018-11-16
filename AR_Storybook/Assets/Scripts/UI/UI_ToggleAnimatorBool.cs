using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ToggleAnimatorBool : MonoBehaviour
{
	[SerializeField] bool startState;

	bool toggle;
	Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		toggle = startState;
	}

	public void ToggleAnimator(string id)
	{
		toggle = !toggle;
		animator.SetBool(id, toggle);
	}
}
