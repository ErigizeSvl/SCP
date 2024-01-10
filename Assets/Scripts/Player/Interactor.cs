using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    // Public var
    public float interactionDistance;

    // Private var
    private bool canInteract = true;

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            Interaction();
        }
    }
    public void CanInteract(bool _state)
    {
        canInteract = _state;
    }
    // Fn Interaction
    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit _hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _hit, interactionDistance))
            {
                if(_hit.collider.gameObject.GetComponent<InteractableObj>() != null)
                {
                    _hit.collider.gameObject.GetComponent<InteractableObj>().Interaction();
                    Debug.Log(gameObject.name);
                }
            }
        }
    }
}
