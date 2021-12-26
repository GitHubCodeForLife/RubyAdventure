using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class Timer : MonoBehaviour
{
    public float maxTime = 1f;
    float currentTime;
    public bool isEnd;

    public Timer()
    {
        this.Reset();
    }
    private void Awake()
    {
        this.Reset();
    }
    private void Update()
    {
        if (isEnd == false)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                isEnd = true;
        }
    }
    public void Reset()
    {
        isEnd = false;
        currentTime = maxTime;
    }
}


