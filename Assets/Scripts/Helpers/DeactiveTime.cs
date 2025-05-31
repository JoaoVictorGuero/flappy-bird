using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveTime : MonoBehaviour
{
    [SerializeField] private float time;

    private float timer = 0;
    private void Update()
    {
        if(timer >= time)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

}
