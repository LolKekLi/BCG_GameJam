using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public static Action<Vector3> damage = delegate { };

    private Vector3 vector3;

    void Start()
    {
        vector3 = new Vector3(0.2f, 1f, 0);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag=="player")
        {
            damage(vector3 * 1.5f);
        }
        
    }
}
