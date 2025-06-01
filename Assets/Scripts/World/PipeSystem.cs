using System.Collections;
using UnityEngine;

public class PipeSystem : MonoBehaviour, IPooledObject
{
    private Coroutine moveRoutine = null;
    private Coroutine damageRoutine = null;

    private bool isMoving;
    float randomDirection;
    private float life = 100;

    [Header("Basic")]
    [SerializeField] private GameObject upPart;
    [SerializeField] private GameObject downPart;
    [Header("Visuals")]
    [SerializeField] private Color damageColor;
    [Header("Advanced")]
    [SerializeField] private float moveRateTime;
    [SerializeField] private float damageTime;


    public void OnObjectSpawn()
    {
        life = 100;
        upPart.transform.localPosition = new Vector2(upPart.transform.localPosition.x, Random.Range(1.9f, 2.5f));
        downPart.transform.localPosition = new Vector2(downPart.transform.localPosition.x, Random.Range(-2.5f, -1.9f));
        upPart.GetComponent<SpriteRenderer>().color = Color.white;
        downPart.GetComponent<SpriteRenderer>().color = Color.white;
        isMoving = false;
        randomDirection = 0;
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        if (damageRoutine != null) StopCoroutine(damageRoutine);
        if (GameManager.canMove)
        {
            moveRoutine = StartCoroutine(moveFrequency());
        }
    }
    private void Update()
    {
        if (isMoving) move();
        if (life == 0) kill();
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

    public void damageHit(float damage)
    {
        life -= damage;
        life = Mathf.Clamp(life, 0, 100);
        if (damageRoutine != null) StopCoroutine(damageRoutine);
        damageRoutine = StartCoroutine(damageSys());
    }
    public void kill()
    {
        GameManager.instance.effectParticle.transform.position = transform.position;
        GameManager.instance.effectParticle.SetActive(true);
        gameObject.SetActive(false);
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
    private IEnumerator damageSys()
    {
        upPart.GetComponent<SpriteRenderer>().color = damageColor;
        downPart.GetComponent<SpriteRenderer>().color = damageColor;
        yield return new WaitForSeconds(damageTime);
        upPart.GetComponent<SpriteRenderer>().color = Color.white;
        downPart.GetComponent<SpriteRenderer>().color = Color.white;
        StopCoroutine(damageRoutine);
    }
}