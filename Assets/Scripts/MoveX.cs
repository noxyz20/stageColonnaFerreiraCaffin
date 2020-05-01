/*
 * Stage univ-corse
 * Authors: Franck Colonna - Clément Caffin - Filipe Ferreira
 * MoveX
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class MoveX : MonoBehaviour
{

    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;

    public Vector3 _nextPost;

    public bool _playerOn;
    void Start()
    {
        _nextPost = startPos.position;
        _playerOn = false;
    }

    void Update()
    {
        if (transform.position == pos1.position)
            _nextPost = pos2.position;
        if (transform.position == pos2.position)
            _nextPost = pos1.position;
        
        transform.position = Vector3.MoveTowards(transform.position, _nextPost, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            other.transform.GetComponent<PlayerController>().platform = transform;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            other.transform.GetComponent<PlayerController>().platform = null;
        }
    }
}
