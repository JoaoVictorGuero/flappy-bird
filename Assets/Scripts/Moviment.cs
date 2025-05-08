using System;
using UnityEngine;

public class Moviment : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public static float posicao = 0f;

    private void Start()
    {
        posicao = 0;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        Move();
        posicao += Time.deltaTime;
    }

    private void Move()
    {
        // Move para cima
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody2D.velocity = new Vector2(0f, 1f);
        }
    }
}
