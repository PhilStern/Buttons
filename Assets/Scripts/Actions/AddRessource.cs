using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddRessource : MonoBehaviour
{
    public RessourceType Ressource;

    public void AddRessourceAction(int value)
    {
        Manager.Instance.AddToRessource(Ressource, value);
    }


}
