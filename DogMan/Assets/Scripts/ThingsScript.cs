using System;
using UnityEngine;


public class ThingsScript : MonoBehaviour
{
     private float speed = 1;
     [SerializeField] private string name;
 

     

     public static Action<string> collect = delegate { };
     void Update()
    {
        transform.Rotate(0,speed,0);
        
    }

     void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.tag=="player")
         {
             
             
            Destroy(gameObject);
            collect(name);
        }
         
     }

   

    
}
