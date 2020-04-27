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
    
    private SpriteRenderer _mySprite;
    private PolygonCollider2D _boxC;
    private BoxCollider2D _foot;

    public Vector2 speed = new Vector2(50, 50);

    private Vector2 _movement = Vector2.zero;

    public ContactFilter2D filter;
    public bool isGrounded;
    private bool _canJump;
    private int _orientation;
    private bool _isEchelle;
    private bool _jump;


    void Start()
    {
        _orientation = 1;
        _mySprite = GetComponent<SpriteRenderer>();
        life = 10;
        _boxC = GetComponent<PolygonCollider2D>();
        _foot = GetComponent<BoxCollider2D>();
        _canJump = true;
        _isEchelle = false;
        _jump = false;
    }

    void Jump()
    {
        _jump = true;
    }
    private void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
        
        _movement = new Vector2(Input.GetAxis("Horizontal"), 0);
        
        if (_movement.x/_orientation < 0)
        {
            _mySprite.flipX = !_mySprite.flipX;
            _orientation *= -1;
        }
        _movement.x *= speed.x;
        
        isGrounded = _foot.IsTouching(filter) && _canJump;

        
        if (Input.GetButton("Jump") && isGrounded)
        {
            Jump();
        }
        

        if (!_foot.IsTouching(filter) && _canJump)
        {
            _movement.y = -2;
        }
        

        if (_isEchelle)
        {
            _movement.y = Input.GetAxis("Vertical") * speed.y/8;
        }
        
    }
    

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = _movement;
        if (_jump)
        {
            _jump = false;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up*speed.y, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("deathLimit"))
        {
            Destroy(gameObject);
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
}
