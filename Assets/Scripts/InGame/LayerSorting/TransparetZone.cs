using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparetZone : MonoBehaviour
{
    [SerializeField] private SpriteRenderer parentSprite;

    private Color notTransparentColor;
    private Color transparentColor;
    private Color newColor;

    private int defaultSortingOrder;
    private float colorLerpTime;

    private void Start()
    {
        parentSprite = GetComponentInParent<SpriteRenderer>();

        notTransparentColor = new Color(parentSprite.color.r, parentSprite.color.g, parentSprite.color.b, 1f);
        transparentColor = new Color(parentSprite.color.r, parentSprite.color.g, parentSprite.color.b, 0.75f);
        newColor = parentSprite.color;

        defaultSortingOrder = parentSprite.sortingOrder;
        colorLerpTime = 5f;
    }

    private void FixedUpdate()
    {
        LerpColorToNewColor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Hitbox")
        {
            parentSprite.sortingOrder = collision.GetComponentInParent<SpriteRenderer>().sortingOrder + 1;
            newColor = transparentColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Hitbox")
        {
            parentSprite.sortingOrder = defaultSortingOrder;
            newColor = notTransparentColor;
        }
    }

    private void LerpColorToNewColor()
    {
        parentSprite.color = Color.Lerp(parentSprite.color, newColor, colorLerpTime * Time.fixedDeltaTime);
    }
}
