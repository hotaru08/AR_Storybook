using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For Event Scripts, inherit from this interface so as to know what functions are needed
/// </summary>
public interface IEventInterface
{
    void EventReceived();
}
