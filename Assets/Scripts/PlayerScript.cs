/*
 * Stage univ-corse
 * Authors: Franck Colonna - Clément Caffin - Filipe Ferreira
 * PlayerScript
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int life;

    public int degatDesLimites;

    private SpriteRenderer mySprite;
    private PolygonCollider2D _boxC;
    private BoxCollider2D _foot;

    public float moveSpeed = 4.0f;
    public float jumpSpeed = 25.0f;
    public float gravity = 10f;

    private Vector2 _movement = Vector2.zero;

    public ContactFilter2D filter;
    public bool isGrounded;
    private bool _canJump;
    private int _orientation;

    void Start()
    {
        _orientation = 1;
        mySprite = GetComponent<SpriteRenderer>();
        life = 10;
        _boxC = GetComponent<PolygonCollider2D>();
        _foot = GetComponent<BoxCollider2D>();
        _canJump = true;
    }

    private void Update()
    {
        gravity = 10;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
        
        _movement = new Vector2(Input.GetAxis("Horizontal"), 0);
        
        if (_movement.x/_orientation < 0)
        {
            mySprite.flipX = !mySprite.flipX;
            _orientation *= -1;
        }
        _movement *= moveSpeed;
        
        isGrounded = _foot.IsTouching(filter) && _canJump;
        if (isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                _movement.y = jumpSpeed;
            }
        }

        if (!_foot.IsTouching(filter) && _canJump)
        {
            gravity = 100;
        }
        

        _movement.y -= gravity * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = _movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("deathLimit"))
        {
            Destroy(gameObject);
        }
    }
}
