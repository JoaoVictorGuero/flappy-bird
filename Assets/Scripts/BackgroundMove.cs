using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private Material _material;
    public float speed;

    private void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (Player.isMoving == true)
        {
            _material.mainTextureOffset = new Vector2(Player.posicao * speed, 0);
        }
    }
}
