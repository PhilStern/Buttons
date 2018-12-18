using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddRessource : MonoBehaviour
{
    public ResourceType Ressource;

    public void AddRessourceAction(int value)
    {
        Manager.Instance.AddToRessource(Ressource, value);
    }


}
