using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType
{
    ObjectCount,
    UnitCount,
    Ressource
}

public enum ConditionOperator
{
    IsEqual,
    IsNotEqual,
    IsBiggerThan,
    IsSmallerThan
}

[System.Serializable]
public class Condition 
{

    [SerializeField]
    private ConditionType Type;

    [SerializeField]
    private ConditionOperator Operator;

    [SerializeField]
    private ResourceType Resource;
    [SerializeField]
    private ObjectType Object;
    [SerializeField]
    private int Value;

    public bool IsFullfilled()
    {
        bool fullfilled = false;

        if (Type == ConditionType.ObjectCount)
        {
            if (CompareValues(Manager.Instance.GetObjectCount(Object), Operator, Value))
            {
                fullfilled = true;
            }
        }
        else if (Type == ConditionType.Ressource)
        {
            if (CompareValues(Manager.Instance.GetRessourceCount(Resource), Operator, Value)) 
            {
                fullfilled = true;
            }
        }
        else if (Type == ConditionType.UnitCount)
        {
            if (CompareValues(Manager.Instance.GetRessourceCount(Resource), Operator, Value)) 
            {
                fullfilled = true;
            }
        }

        return fullfilled;
    }


    public bool CompareValues(int ValueA, ConditionOperator coperator, int ValueB)
    {
        bool returner = false;
        switch (coperator)
        {
            case ConditionOperator.IsEqual:
                if (ValueA == ValueB)
                {
                    returner = true;
                }
                break;
            case ConditionOperator.IsNotEqual:
                if (ValueA != ValueB)
                {
                    returner = true;
                }
                break;
            case ConditionOperator.IsBiggerThan:
                if (ValueA > ValueB)
                {
                    returner = true;
                }
                break;
            case ConditionOperator.IsSmallerThan:
                if (ValueA < ValueB)
                {
                    returner = true;
                }
                break;
        }
        return returner;
    }

    
}
