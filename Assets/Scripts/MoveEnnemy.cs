using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Random = System.Random;
using RandomUnity = UnityEngine.Random;

namespace Platformer.Mechanics
{
    public class MoveEnnemy : KinematicObject
    {
        public Transform platform;

        public float maxSpeed;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>

        public Health health;
        
        public Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        
        void Awake()
        {
            health = GetComponent<Health>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            move.x = (new int[] {-1, 1})[RandomUnity.Range(0, 2)];
            maxSpeed = (new float[] {1f, 1.25f, 1.5f, 1.75f, 2f, 2.25f, 2.5f, 3f})[RandomUnity.Range(0, 8)];
        }


        protected override void ComputeVelocity()
        {
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
            if (other.CompareTag("limitDroite") || other.CompareTag("limitGauche"))
            {
                move.x = -move.x;
            }
        }

    }
}