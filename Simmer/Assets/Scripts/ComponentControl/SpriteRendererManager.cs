using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererManager : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer;

    public void Construct()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite toSet)
    {
        _spriteRenderer.sprite = toSet;
    }

    public void SetColor(Color toSet)
    {
        _spriteRenderer.color = toSet;
    }
}
