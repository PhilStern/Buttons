using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ButtonState
{
    Inactive,
    Base,
    Hover,
    Click
}

public class SpriteButton : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D RigidBody;
    private BoxCollider2D BoxCollider;
    [Tooltip("The base sprite is required.")]
    [SerializeField]
    private Sprite Base;
    [SerializeField]
    private Color BaseColor;
    [SerializeField]
    private Sprite Hover;
    [SerializeField]
    private Color HoverColor;
    [SerializeField]
    private Sprite Click;
    [SerializeField]
    private Color ClickColor;
    [SerializeField]
    private Sprite Inactive;
    [SerializeField]
    private Color InactiveColor;
    [SerializeField]
    private ButtonState State;
    [SerializeField]
    private Timer timer;

    private ConditionObserver conditionObserver;

    private bool Active;

    public UnityEvent OnClicked;

    [SerializeField]
    private bool hover = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        conditionObserver = GetComponent<ConditionObserver>();
        timer = GetComponent<Timer>();
        SetSprite(Base);
        BoxCollider = gameObject.AddComponent<BoxCollider2D>();
        RigidBody = gameObject.AddComponent<Rigidbody2D>();
        RigidBody.isKinematic = true;
        SetActive(true);
    }

    private void OnMouseOver()
    {
        if (Active)
        {
            if (Input.GetMouseButton(0))
            {
                SetState(ButtonState.Click);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (OnClicked != null)
                    OnClicked.Invoke();
            }
            else
            {
                SetState(ButtonState.Hover);
            }
        }
    }

    private void OnMouseExit()
    {
        if (Active)
        {
            SetState(ButtonState.Base);
        }
    }

    private void SetState(ButtonState state)
    {
        State = state;
        switch (State)
        {
            case ButtonState.Base:
                SetSprite(Base);
                SetColor(BaseColor);
            break;
            case ButtonState.Hover:
                SetSprite(Hover);
                SetColor(HoverColor);
            break;
            case ButtonState.Click:
                SetSprite(Click);
                SetColor(ClickColor);
            break;
            case ButtonState.Inactive:
                SetSprite(Inactive);
                SetColor(InactiveColor);
            break;
        }
    }

    private void SetSprite(Sprite sprite)
    {
        if (spriteRenderer != null && sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
        else if(spriteRenderer != null && Base != null)
        {
            spriteRenderer.sprite = Base;
        }
        else if(spriteRenderer != null)
        {
            Debug.Log(gameObject.name + "'s base sprite is missing.");
        }
    }

    private void SetColor(Color color)
    {
        if (spriteRenderer != null && !color.Equals(Color.clear))
        {
            spriteRenderer.color = color;
        }
        else if (spriteRenderer != null)
        {
            Debug.Log(gameObject.name + " has a missing color.");
        }
    }

    public void UpdateConditions()
    {
        if (conditionObserver != null)
        {
            if (conditionObserver.ConditionsFullfilled() && !timer.TimerIsRunning())
            {
                SetActive(true);
            }
            else
            {
                SetActive(false);
            }
        }
        else if (timer != null && !timer.TimerIsRunning())
        {
            SetActive(true);
        }
        else
        {
            SetActive(false);
        }
    }

    public void SetActive(bool active)
    {
        if (conditionObserver != null)
        {
            if (active == true && conditionObserver.ConditionsFullfilled())
            {
                Active = active;
            }
            else
            {
                if (Manager.Instance.Debug)
                {
                    Debug.Log("Conditions not fullfilled setting active command to false");
                }
                Active = false;
            }
        }
        else
        {
            Active = active;
        }

        if (!Active)
        {
            SetState(ButtonState.Inactive);
        }
        else
        {
            SetState(ButtonState.Base);
        }
    }



}
