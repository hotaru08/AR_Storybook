using UnityEngine;

public abstract class State : MonoBehaviour
{
    private string m_stateName;
    private GameObject m_gameObject;

    public State(string _name, GameObject _go)
    {
        m_stateName = _name;
        m_gameObject = _go;
    }

    public virtual string GetStateName
    {
        get { return m_stateName; }
    }

    public virtual void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public virtual void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}