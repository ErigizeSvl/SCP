using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InteractableObj : MonoBehaviour
{
    // Public var
    public UnityEvent onInteract;

    // Fn interaction
    public void Interaction()
    {
        onInteract.Invoke();
    }
}
