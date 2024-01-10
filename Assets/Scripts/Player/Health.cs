using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //Public Var
    public UnityEvent onDead;

    public void Attacked()
    {
        onDead.Invoke();
        GetComponent<PlayerMovement>().canMove = false;
        GetComponent<PlayerMovement>().canRotate = false;
        GetComponent<Interactor>().CanInteract(false);

    }
}
