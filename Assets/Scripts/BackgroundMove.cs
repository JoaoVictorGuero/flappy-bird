using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    Material _material;
    public float speed;
    
    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }
    
    void Update()
    {
        if (GameManager._isMoving == true)
        {
            _material.mainTextureOffset = new Vector2(GameManager.posicao * speed, 0);
        }
    }
}
