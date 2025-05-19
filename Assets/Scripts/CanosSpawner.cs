using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanosSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2.5f;
    public float heightOffset = 1f;
    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (Player.isMoving != true) return;
        if (!(_timer >= spawnRate)) return;
        SpawnPipe();
        _timer = 0f;

    }

    private void SpawnPipe()
    {
        var screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
        var yPos = Random.Range(-heightOffset, heightOffset);
        
        var spawnPos = new Vector3(screenRight.x + 1f, yPos, 0);
        Instantiate(pipePrefab, spawnPos, Quaternion.identity);
    }
}