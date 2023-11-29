using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Public var
    public int AccessLvl;

    // Priv var 
    private bool isOpen;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        animator = GetComponent<Animator>();
    }

    // 
    public void Interaction()
    {
        if (Inventory.Instance.GetKeycardLvl() >= AccessLvl)
        {

            if (isOpen)
            {
                isOpen = false;
            }
            else
            {
                isOpen = true;
            }
            //Debug.Log(isOpen);
            animator.SetBool("isOpen", isOpen);
        }
    }
}
