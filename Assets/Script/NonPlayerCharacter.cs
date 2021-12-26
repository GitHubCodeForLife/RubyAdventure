using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public GameObject dialogBox;
    float displayTime = 3f;
    float displayTimer;
    private void Start()
    {
        dialogBox.SetActive(false);
        displayTimer = -1.0f;
    }
    private void Update()
    {
        if(displayTimer > 0)
        {
            displayTimer -= Time.deltaTime;
            if (displayTimer <= 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }
    internal void DisplayDialog()
    {
        displayTimer = displayTime;
        dialogBox.SetActive(true);

    }
}
