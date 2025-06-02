using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObject : MonoBehaviour, IPooledObject
{
    [SerializeField] private PowerUpVariables powerUp;

    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

    }

    public void OnObjectSpawn()
    {
        _spriteRenderer.enabled = true;
        _boxCollider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PowerUp.instance.effect(powerUp);
            _audioSource.PlayOneShot(powerUp.sound, 0.5f);
            _spriteRenderer.enabled = false;
            _boxCollider2D.enabled = false;
        }
    }
}
