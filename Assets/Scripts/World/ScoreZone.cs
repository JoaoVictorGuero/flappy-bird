using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreZone : MonoBehaviour
{
    [SerializeField] private AudioClip scoreSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddScore(1);
            GetComponent<AudioSource>().PlayOneShot(scoreSound);
        }
    }
}