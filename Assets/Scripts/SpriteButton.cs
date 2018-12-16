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

    private bool Active;

    public UnityEvent OnClicked;

    [SerializeField]
    private bool hover = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
        else if(Base != null)
        {
            spriteRenderer.sprite = Base;
        }
        else
        {
            Debug.Log(gameObject.name + "'s base sprite is missing.");
        }
    }

    private void SetColor(Color color)
    {
        if (!color.Equals(Color.clear))
        {
            spriteRenderer.color = color;
        }
        else
        {
            Debug.Log(gameObject.name + " has a missing color.");
        }
    }

    public void SetActive(bool active)
    {
        Active = active;
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
