using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevl : MonoBehaviour
{
    private bool costil = true;
    private bool _win;

    void Start()
    {
        costil = true;
        UIManager.win += UIManager_Win;
    }

    private void UIManager_Win()
    {
        _win = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_win)
        {
            if (costil)
            {
                SceneSwith.SwithToScene(3);
                costil = false;
            }
           
            
        }
       
    }

    void OnDisable()
    {
        UIManager.win -= UIManager_Win;
    }
}
