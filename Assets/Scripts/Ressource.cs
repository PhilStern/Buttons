﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Ressource 
{
    [SerializeField]
    private int Value;
    public ResourceType RessourceType;
    public Text Text;
   

    public Ressource(ResourceType ressourceType)
    {
        RessourceType = ressourceType;
    }

    public void AddToRessource(int value)
    {
        Value += value;
        UpdateText();
    }

    public void SetRessource(int value)
    {
        Value = value;
        UpdateText();
    }

    public int GetValue()
    {
        return Value;
    }

    public void UpdateText()
    {
        Text.text = Value.ToString();
    }
}
