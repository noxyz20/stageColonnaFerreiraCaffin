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

    void Start()
    {
        _nextPost = startPos.position;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Collider2D>().transform.SetParent(transform);
            other.GetComponent<PlayerController>().maxSpeed *= 2.5f;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Collider2D>().transform.SetParent(null);
            other.GetComponent<PlayerController>().maxSpeed /= 2.5f;
        }
    }
}
