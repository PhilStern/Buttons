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

    public Canvas Canvas;
    public GameObject TextPrefab;

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
    }


    public void AddToRessource(RessourceType ressource, int value)
    {
        for (int i = 0; i < Ressources.Count; i++)
        {
            if(Ressources[i].RessourceType == ressource)
            {
                Ressources[i].AddToRessource(value);
            }
        }
    }



}
