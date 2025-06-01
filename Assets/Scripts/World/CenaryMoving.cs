using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenaryMoving : MonoBehaviour
{
    void Update()
    {
        if (Player.isMoving == true)
            transform.position += Vector3.left * GameManager.currentSpeed * Time.deltaTime;
    }
}
