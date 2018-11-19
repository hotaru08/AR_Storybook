using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimator : MonoBehaviour
{
    /// <summary>
    /// Function to set Animator Controller
    /// </summary>
    /// <param name="_controller">Animator to set</param>
    public void SetRuntimeAnimator(Object _controller)
    {
        GetComponent<Animator>().runtimeAnimatorController = _controller as RuntimeAnimatorController;
    }
}
