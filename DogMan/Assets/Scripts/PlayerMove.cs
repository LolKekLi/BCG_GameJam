using System;
using System.Collections;
using System.Diagnostics.SymbolStore;
using Trap;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class PlayerMove : MonoBehaviour
    {
        private Animator _animator;

        public float speedMove;
        public float shiftMove;
        public float jumpPower;
        private float gravity;

        public bool isGround;
        public bool isWall;
        public bool isRoof;
        public bool right = true;

        private Rigidbody rb;

        private Vector3 moveVector;
        
        private CharacterController ch_controller;

        private void Start()
        {
            ch_controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            
            SwingTrapScript.damage += _bulletDeath;
            BulletScript.damege += _bulletDeath;
            TrapScript.damage += _bulletDeath;
        }

        private void _bulletDeath(Vector3 vector3)
        {
            StartCoroutine(Force(vector3));
        }

        IEnumerator Force(Vector3 vector3)
        {

            rb.AddForce(vector3 * 350f,ForceMode.Force);
            yield return new WaitForSecondsRealtime(0.2f);
            rb.AddForce(vector3 * -350f, ForceMode.Force);

        }

        private void Update()
        {
            CharasterMove();
            GameGravity();
            Jump();
        }

        private void CharasterMove()
        {
            moveVector = Vector3.zero;
            moveVector.x = Input.GetAxis("Horizontal") * speedMove;
          
            if (moveVector.x != 0)
            {
                if (moveVector.x > 0)
                {
                    right = true;
                }
                else
                {
                    right = false;
                }
                _animator.SetBool("Move", true);
            }
            else
            {
                _animator.SetBool("Move", false);
            }
            
            
            if (Input.GetKey(KeyCode.LeftControl))
            { 
                GetComponent<CharacterController>().radius = 0.5f;
                GetComponent<CharacterController>().height = 3.4f;
                GetComponent<CharacterController>().center = new Vector3(0.0f, 0.4f, -0.05f);
                
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                {
                    _animator.SetBool("StepSquat", true);
                }
                else
                {
                    _animator.SetBool("StepSquat", false);
                }
                _animator.SetBool("Squat", true);
            }
            else
            {
                GetComponent<CharacterController>().radius = 0.5f;
                GetComponent<CharacterController>().height = 3.8f;
                GetComponent<CharacterController>().center = new Vector3(0.0f, 0.5f, -0.05f);
                
                _animator.SetBool("StepSquat", false);
                _animator.SetBool("Squat", false);
            }

            if (Input.GetKey(KeyCode.LeftShift) && moveVector.x!=0)
            {
                moveVector.x = Input.GetAxis("Horizontal") * shiftMove;
                _animator.SetBool("Shift", true);
            }
            else
            {
                _animator.SetBool("Shift", false);
            }
            
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
            moveVector.y = gravity;
            ch_controller.Move(moveVector * Time.deltaTime);
        }

        private void GameGravity()
        {
            if (!ch_controller.isGrounded)
            {
                gravity -= 30f * Time.deltaTime;
            }
            else
            {
                gravity = -1f;
            }
        }
        
        private void Jump()
        {
            Ray ray2= new Ray(Vector3.zero,Vector3.zero);
            Ray ray = new Ray(Vector3.zero,Vector3.zero);
            
            if (right)
            {
                ray2 = new Ray(gameObject.transform.position, Vector3.right);
                Debug.DrawRay(transform.position, ray2.direction * 1f);
                ray = new Ray(Vector3.zero, Vector3.zero);
            }
            else
            {
                ray = new Ray(gameObject.transform.position, Vector3.left);
                Debug.DrawRay(transform.position, ray.direction * 1f);
            }
            
            Ray ray3 = new Ray(gameObject.transform.position, Vector3.down);
            Ray ray4 = new Ray(gameObject.transform.position, Vector3.up);
            
            Debug.DrawRay(transform.position, ray4.direction * 2f);
            Debug.DrawRay(transform.position, ray3.direction * 1f); 
            
            RaycastHit rh;
            
            if (Physics.Raycast(ray4, out rh, 1.3f))
            {
                isRoof = true;
            }
            else
            {
                isRoof = false;
            }

            if (isRoof == true)
            {
                gravity = -2f;
            }

            if (Physics.Raycast(ray3, out rh, 1.0f))
            {
                
                isGround = true;
            }
            else
            {
                
                isGround = false;
            }

            if (Physics.Raycast(ray, out rh, 1f) || Physics.Raycast(ray2, out rh, 1f))
            {
                isWall = true;
            }
            else
            {
                isWall = false;
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && isGround == true ||
                Input.GetKeyDown(KeyCode.Space) && isWall == true)
            {
                _animator.SetBool("Jump", true);
                gravity = jumpPower;
            }
            else
            {
                _animator.SetBool("Jump", false);
            }

        }

        private void OnDisable()
        {
            SwingTrapScript.damage -= _bulletDeath;
            BulletScript.damege -= _bulletDeath;
            TrapScript.damage -= _bulletDeath;
        }
    }
}
