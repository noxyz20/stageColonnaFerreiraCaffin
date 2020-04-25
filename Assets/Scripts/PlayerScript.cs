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
    private BoxCollider2D _boxC;

    public float moveSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 9.81f;

    private Vector2 _movement = Vector2.zero;

    public ContactFilter2D filter;
    public bool isGrounded;

    void Start()
    {
        _boxC = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        isGrounded = _boxC.IsTouching(filter);

        if (isGrounded)
        {
            _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _movement *= moveSpeed;

            if (Input.GetButton("Jump"))
            {
                _movement.y = jumpSpeed;
            }
        }

        _movement.y -= gravity * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = _movement;
    }
}
