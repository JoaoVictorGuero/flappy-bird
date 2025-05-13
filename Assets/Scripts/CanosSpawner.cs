using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanosSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2.5f;
    public float heightOffset = 1f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (GameManager._isMoving == true)
        {
            if (timer >= spawnRate)
            {
                SpawnPipe();
                timer = 0f;
            }
        }

    }

    void SpawnPipe()
    {
        Vector3 screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
        float yPos = Random.Range(-heightOffset, heightOffset);
        
        Vector3 spawnPos = new Vector3(screenRight.x + 1f, yPos, 0);
        Instantiate(pipePrefab, spawnPos, Quaternion.identity);
    }
}