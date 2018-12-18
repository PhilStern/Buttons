using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionObserver : MonoBehaviour
{
    [SerializeField]
    private List<Condition> Conditions = new List<Condition>();


    public bool ConditionsFullfilled()
    {
        
        bool fullfilled = true;
        foreach (Condition c in Conditions)
        {
            if (c.IsFullfilled() == false)
            {
                fullfilled = false;
            }
        }
        return fullfilled;
    }
}
