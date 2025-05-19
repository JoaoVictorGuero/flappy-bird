using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreZone : MonoBehaviour
{
    private bool scored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!scored && other.CompareTag("Player"))
        {
            Player.instance.AddScore(1);
            scored = true;
        }
    }
}