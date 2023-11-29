using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Singleton
    public static Inventory Instance { get; private set; }


    // Private var
    private int keycardLvl, scp207;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        keycardLvl = 0;
        scp207 = 0;
    }

    void Update()
    {
        // If press R
        if (Input.GetKeyDown(KeyCode.R))
        {
            UseSCP207();
        }
    }

    // Change security lvl
    public void SetKeycardLvl(int _Lvl)
    {
        // If new lvl is > than the last
        if(_Lvl > keycardLvl)
        {
            keycardLvl = _Lvl;
        }
    }

    // Fn return level access value
    public int GetKeycardLvl()
    {
        return keycardLvl;
    }

    // Fn to add SCP207
    public void AddSCP205()
    {
        scp207++;
    }

    // Fn use SCP207
    public void UseSCP207()
    {
        if (scp207 > 0)
        {
            scp207--;
            GetComponent<Eyes>().ResetTime2Blink();
        }
    }
}
