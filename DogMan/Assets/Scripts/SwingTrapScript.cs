using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingTrapScript : MonoBehaviour
{
    public static Action<Vector3> damage = delegate { };
    private bool isright = true;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "player")
        {
            if (isright)
            {
                damage(Vector3.right);
            }
            else
            {
                damage(Vector3.left);
            }
            
        }

    }

    public void Change()
    {
        if (isright)
        {
            isright = false;
        }
        else
        {
            isright = true;
        }
    }

}
