using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.UI;

public class lifeUI : MonoBehaviour
{
    public Sprite[] HearthSprites;
    public Image HearthUI;
    private Health _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }
    void Update()
    {
        HearthUI.sprite = HearthSprites[_player.currentHP];
    }
}
