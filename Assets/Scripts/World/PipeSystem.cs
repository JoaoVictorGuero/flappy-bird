using System.Collections;
using UnityEngine;

public class PipeSystem : MonoBehaviour, IPooledObject
{
    private Coroutine moveRoutine;
    private bool isMoving;
    float randomDirection;

    [Header("Basic")]
    [SerializeField] private GameObject upPart;
    [SerializeField] private GameObject downPart;
    [Header("Advanced")]
    [SerializeField] private float moveRateTime;

    public void OnObjectSpawn()
    {
        upPart.transform.localPosition = new Vector2(upPart.transform.localPosition.x, Random.Range(1.9f, 2.5f));
        downPart.transform.localPosition = new Vector2(downPart.transform.localPosition.x, Random.Range(-2.5f, -1.9f));
        isMoving = false;
        randomDirection = 0;
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        if (GameManager.canMove)
        {
            moveRoutine = StartCoroutine(moveFrequency());
        }
    }
    private void Update()
    {
        if (isMoving) move();
    }

    private void move()
    {
        transform.Translate(new Vector2(0, randomDirection * Time.deltaTime), Space.World);
        if (transform.position.y >= 1.5f || transform.position.y <= -0.55f)
        {
            isMoving = false;
            StopCoroutine(moveRoutine);
        }
    }

    private IEnumerator moveFrequency()
    {
        while (GameManager.canMove)
        {
            randomDirection = Random.Range(-2f, 2f);
            isMoving = true;
            yield return new WaitForSeconds(moveRateTime);
            isMoving = false;
            yield return new WaitForSeconds(moveRateTime);
        }
    }
}