using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    private static Manager _instance;

    public static Manager Instance { get { return _instance; } }

    public List<SpriteButton> Buttons = new List<SpriteButton>();
    public List<Action> Actions = new List<Action>();
    public List<Ressource> Ressources = new List<Ressource>();
    public List<Object> Objects = new List<Object>();
    public List<Unit> Units = new List<Unit>();

    public Canvas Canvas;
    public GameObject TextPrefab;
    public bool Debug = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    private void Start()
    {
        for(int i = 0;i < Ressources.Count;i++)
        {
            RectTransform rt = Instantiate(TextPrefab, Canvas.transform).GetComponent<RectTransform>();
            Ressources[i].Text = rt.transform.GetComponent<Text>();
            rt.anchoredPosition = new Vector2(0, -20+i*20);
            Ressources[i].UpdateText();
        }
        UpdateButtons();
    }


    public void AddToRessource(ResourceType ressource, int value)
    {
        for (int i = 0; i < Ressources.Count; i++)
        {
            if(Ressources[i].RessourceType == ressource)
            {
                Ressources[i].AddToRessource(value);
            }
        }
        UpdateButtons();
    }

    public int GetRessourceCount(ResourceType ressource)
    {
        int value = 0;
        for (int i = 0; i < Ressources.Count; i++)
        {
            if (Ressources[i].RessourceType == ressource)
            {
                value = Ressources[i].GetValue();
            }
        }
        return value;
    }

    public int GetObjectCount(ObjectType objectType)
    {
        int counter = 0;
        foreach (Object o in Objects)
        {
            if (o.Type == objectType)
            {
                counter++;
            }
        }
        return counter;
    }

    public int GetObjectCount(UnitType unitType)
    {
        int counter = 0;
        foreach (Unit u in Units)
        {
            if (u.Type == unitType)
            {
                counter++;
            }
        }
        return counter;
    }

    public void UpdateButtons()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].UpdateConditions();
        }
    }



}
