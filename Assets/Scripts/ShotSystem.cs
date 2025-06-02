using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSystem : MonoBehaviour, IPooledObject
{
    [SerializeField] private float damage;
    [SerializeField] private float activeTime;

    private float timer = 0;
    public void OnObjectSpawn()
    {
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= activeTime) gameObject.SetActive(false);
    }
    //sistema de Dano ao cano 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            collision.transform.parent.GetComponent<PipeSystem>().damageHit(damage);
            gameObject.SetActive(false);
        }
    }
    
}
