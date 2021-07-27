using System;
using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Trap
{

    public class ShootTrapScript : MonoBehaviour
    {
        
        [SerializeField] private bool isRight = false;
        [SerializeField] private GameObject bullet;
       
        [Range(0.2f,3)]
        [SerializeField] private float bulletRange = 1;
        

        private Vector3 bulletStartPos;
      

        void OnEnable()
        {
            
            bulletStartPos = transform.GetChild(1).position;
            StartCoroutine(shoot());
        }

        IEnumerator shoot()
        {
            while (true)
            {
                var _bullet = Instantiate(bullet);
                _bullet.transform.position = bulletStartPos;
                yield return new WaitForSeconds(bulletRange);
            }

        }


    }
}
   
