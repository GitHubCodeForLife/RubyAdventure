using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInfo 
{
    public static bool IsContinue = false;

    private const string x = "X";
    public float X;
    private const string y = "Y";
    public float Y;
    private const string currentHealth = "CurrentHealth";
    public int CurrentHealth;

    public void Save(int SaveSlot)
    {
        PlayerPrefs.SetFloat("SaveSlot_" + SaveSlot.ToString() + "_" + x, X);
        PlayerPrefs.SetFloat("SaveSlot_" + SaveSlot.ToString() + "_" + y, Y);
        PlayerPrefs.SetInt("SaveSlot_" + SaveSlot.ToString() + "_" + currentHealth, CurrentHealth);
    }
    public void Load(int SaveSlot)
    {
        X = PlayerPrefs.GetFloat("SaveSlot_" + SaveSlot.ToString() + "_" + x, X);
        Y = PlayerPrefs.GetFloat("SaveSlot_" + SaveSlot.ToString() + "_" + y, Y);
        CurrentHealth = PlayerPrefs.GetInt("SaveSlot_" + SaveSlot.ToString() + "_" + currentHealth, CurrentHealth);
    }
}
