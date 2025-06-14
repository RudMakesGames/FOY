using Inventory.UI;
using System.Collections;
using Inventory.Model;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.Windows;


namespace Inventory
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioSource))]
    public class Move : MonoBehaviour
    {

        private static PlayerInput playerInput;
        [Header("Player's Animator")]
        public Animator anim;
        public static bool GameIsPaused = false;
        [Header("Player's RigidBody2D")]
        public Rigidbody2D rb;
        [Header("GroundCheck Transform for jumping")]
        public Transform groundcheck;
        public LayerMask groundLayer;
        public LayerMask stairLayer;
        public float horizontal;
        [Range(-5f, 20f)]
        private Vector2 moveInput;
        [SerializeField]
        public float speed = 3f,
         JumpPower = 7f,
         distance = 2f,
         MaxPitch = 1.1f,
         MaxStairPitch = 1.1f,
         MinStairPitch = 0.8f,
         MinPitch = 0.8f,
         nextStairFootstepTime,
         nextFootstepTime,
         StairPitchChangeRate = 0.1f,
         StairFootstepDelay,
         pitchChnageRate = 0.1f,
         footstepDelay = 0.5f;

        public int ChaseSpeedInc = 2;
        public GameObject PauseMenuUI,
         ResumeButton,
         puzzle;
        public bool CanChangeDir = true,
         isJumping,
         IsOn = true,
         isFacingRight = true,
         canInteract,
         IsAllowedToGoDown;
        bool inDialogue;
        [Header("Walking sfx")]
        public AudioSource Walking, StairWalk;

        public ChaseSequence chaseSequence;
        /*
        [Header("Tutorial Only")]
        public GameObject MovePrompt, JumpPrompt, InteractPrompt;

        private bool isAorDpressed, isJumpPressed, isEpressed;*/

        [Header("Idle Animation Switching")]
        [SerializeField]
        private float IdleTimer = 5;
        private void Start()
        {

            playerInput = gameObject.GetComponent<PlayerInput>();
            nextFootstepTime = 0f;
        }
        IEnumerator StairCase()
        {
            Debug.Log("Can GO down the stairs");
            Physics2D.IgnoreLayerCollision(3, 6, true);
            yield return new WaitForSeconds(2);
            Physics2D.IgnoreLayerCollision(3, 6, false);
            Debug.Log("Collision Enabled Again");
        }
        private void Update()
        {
            if(isGrounded() && anim.GetBool("IsRunning") == false)
            {
                IdleTimer -= Time.deltaTime;
                if (IdleTimer <= 0)
                {
                    anim.SetInteger("IdleCount", Random.Range(1, 3));
                    IdleTimer = 5;
                }   
            }
            anim.SetBool("IsRunning", horizontal != 0);
            #region 

            if (isJumping == true)
            {
                if(anim!= null)
                anim.SetBool("IsJumping", true);
            }
            else
            {
                if (anim != null)
                    anim.SetBool("IsJumping", false);
            }


            if (PuzzleManager.instance.isPuzzleActive)
            {
                if (anim != null)
                {
                    inDialogue = true;
                    horizontal = 0;
                    rb.velocity = Vector3.zero;
                    anim.SetBool("IsJumping", false);
                    return;
                }
                

               
            }
            if (PuzzleManager.instance.isPuzzleActive == false)
            {
                inDialogue = false;
                rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);
            }
            if (CutsceneManager.Instance.isActive)
            {
                if(anim!= null)
                {
                    inDialogue = true;
                    horizontal = 0f;
                    rb.velocity = Vector3.zero;
                    anim.SetBool("IsJumping", false);
                    return;
                }
               
            }
            if (CutsceneManager.Instance.isActive == false)
            {
                inDialogue = false;
                rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);
            }

            if (DialogueManager.Instance.isDialogueActive)
            {
                if(anim != null)
                {
                    inDialogue = true;
                    horizontal = 0;
                    rb.velocity = Vector3.zero;
                    anim.SetBool("IsJumping", false);
                    return;
                }
              

            }
            if (DialogueManager.Instance.isDialogueActive == false)
            {
                inDialogue = false;
                rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);
            }

            if(rb!= null)
            {
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            }
            if (isOnStairs())
            {
                StartCoroutine(StairCase());

            }
            else if (isGrounded())
            {

                Physics2D.IgnoreLayerCollision(3, 6, false);
            }


            if (horizontal != 0f)
            {
                if (isGrounded())
                {
                    if (anim != null)
                    {
                        if (!Walking.isPlaying && Time.time >= nextFootstepTime)
                        {
                            Walking.Play();
                            nextFootstepTime = Time.time + footstepDelay;
                        }
                        Walking.pitch = pitchChnageRate * Time.deltaTime;
                        Walking.pitch = Random.Range(MinPitch, MaxPitch);
                    }

                }

                if (isOnStairs())
                {
                    if (anim != null)
                    {
                        if (!StairWalk.isPlaying && Time.time >= nextStairFootstepTime)
                        {
                            StairWalk.Play();
                            nextStairFootstepTime = Time.time + StairFootstepDelay;
                        }
                        StairWalk.pitch = StairPitchChangeRate * Time.deltaTime;
                        StairWalk.pitch = Random.Range(MinStairPitch, MaxStairPitch);
                    }

                }

            }
            else
            {
                if (anim != null)
                {
                    anim.SetBool("IsRunning", false);
                    Walking.Stop();

                }

            }

            Flip();
            #endregion
        }
        #region Inputs
        public void RunFast(InputAction.CallbackContext context)
        {
            
                if(context.performed && chaseSequence!= null)
                {
                    chaseSequence.Chasespeed = chaseSequence.Chasespeed + ChaseSpeedInc;
                    Debug.Log("Speed Increased!");
                }
                else if(context.canceled)
                {
                  chaseSequence.Chasespeed = 4;
                Debug.Log("Speed Back to 4!");
            }
           
             
            
        }
        public void Jump(InputAction.CallbackContext context)
        {
            if (!inDialogue)
            {
                if (context.performed && isGrounded())
                {
                    anim.SetTrigger("Takeoff");
                    rb.velocity = new Vector2(rb.velocity.x, JumpPower);
                    isJumping = true;
                }
                if (context.canceled)
                {
                    anim.SetBool("IsJumping", false);
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.75f);
                }
            }

        }
        private bool isGrounded()
        {
            return Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundLayer);
           
        }
        private bool isOnStairs()
        {
            return Physics2D.OverlapCircle(groundcheck.position, 0.2f, stairLayer);
        }

        public void Movement(InputAction.CallbackContext context)
        {
            if (!inDialogue)
            {
                if(context.performed)
                {
                    horizontal = context.ReadValue<Vector2>().x;
                }
                else if(context.canceled)
                {
                    horizontal = 0f;
                }
                

            }


        }

  

        public void Flip()
        {
            if (isFacingRight && horizontal < 0f && CanChangeDir || !isFacingRight && horizontal > 0f && CanChangeDir)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
        public void ItemInteract(InputAction.CallbackContext context)
        {
            if (context.performed && DialogueManager.Instance.isDialogueActive)
            {
                DialogueManager.Instance.DisplayNextDialogueLine();
            }
        }
        public void PauseGame(InputAction.CallbackContext context)
        {
            if (context.performed && PuzzleManager.instance.isPuzzleActive)
            {
                if (puzzle != null)
                {
                    puzzle.SetActive(false);
                    PuzzleManager.instance.isPuzzleActive = false;
                }

            }
            if (context.performed && GameIsPaused)
            {
                Resume();

            }
            else if (context.performed && !GameIsPaused)
            {
                Pause();

            }
            void Resume()
            {
                EventSystem.current.SetSelectedGameObject(null);
                PauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                GameIsPaused = false;
            }
            void Pause()
            {

                EventSystem.current.SetSelectedGameObject(ResumeButton);
                PauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                GameIsPaused = true;
            }


        }

        public static void EnableActions()
        {
            playerInput.currentActionMap.Enable();
        }
        public static void DisableActions()
        {
            playerInput.currentActionMap.Disable();
        }

        #endregion
        #region collision
        public void OnCollisionEnter2D(Collision2D collision)
        {
            
            if(collision.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
                Physics2D.IgnoreLayerCollision(3, 6, false);
            }

            if (collision.gameObject.CompareTag("Run") || collision.gameObject.CompareTag("Rail"))
            {
                isJumping = false;
                
            }
        }

       
        public void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Rail"))
            {
                isJumping = true;
            }
        }

    }
    
        #endregion
    
    
}

