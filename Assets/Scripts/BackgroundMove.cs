using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    Material _material;
    public float speed;
    // Start is called before the first frame update
    
    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _material.mainTextureOffset = new Vector2(Moviment.posicao*speed, 0); 
    }
}
