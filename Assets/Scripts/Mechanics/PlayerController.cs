using System;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    
    public class PlayerController : KinematicObject
    {
        public float lastTouch;

        public Transform platform;
        
        public float maxSpeed = 2.5f;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 3;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        public Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        private bool _isEchelle;
        private bool goMove;

        public Bounds Bounds => collider2d.bounds;
        

        void Awake()
        {
            goMove = false;
            _isEchelle = false;
            health = GetComponent<Health>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            platform = null;
            lastTouch = Time.time;
        }

        private void OnGUI()
        {
            if (goMove)
            {
                float restant = Time.time - lastTouch;
                string text = string.Format( "Don't Stop Running : {0}", 4-Convert.ToInt32(restant)); 
                GUIStyle guiStyle = new GUIStyle();
                guiStyle.fontSize = 50;
                guiStyle.fontStyle = FontStyle.Bold;
                guiStyle.normal.textColor = Color.red;
                guiStyle.alignment = TextAnchor.MiddleCenter;
                GUI.Label( new Rect(Screen.width/2f-50, Screen.height/2f-25, 100, 50), text, guiStyle); 
            }
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (move.x != 0)
                {
                    lastTouch = Time.time;
                    goMove = false;
                    print((Time.time - lastTouch));
                }
                else
                {
                    float immobile = Time.time - lastTouch;
                    if (immobile >= 1)
                    {
                        if (immobile >= 4)
                        {
                            health.Die();
                            print("GAMEOVER");
                        }
                        print(("BOUGE FDP !"));
                        goMove = true;
                    }
                }
                
                if (move.x != 0 && platform != null)
                {
                    transform.SetParent(null);
                }
                else if (move.x == 0 && platform != null)
                {
                    transform.SetParent(platform);
                }
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                {
                    jumpState = JumpState.PrepareToJump;
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                        velocity.y = 0;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    velocity.y = 0;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
                transform.SetParent(null);
                platform = null;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }
            else if (_isEchelle)
            {
                velocity.y = Input.GetAxis("Vertical")*3;
            }
            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            
            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
            
            targetVelocity = move * maxSpeed;
            
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("deathLimit"))
            {
                health.Die();
            }
            if (other.CompareTag("echelle"))
            {
                _isEchelle = true;
            }
        }
        
        

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("echelle"))
            {
                _isEchelle = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("ennemy"))
            {
                health.Decrement();
            }
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}