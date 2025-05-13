using UnityEngine;

public class CanosMoving : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        if (GameManager._isMoving == true)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        
        if (transform.position.x < Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x - 1f)
        {
            Destroy(gameObject);
        }
    }
}