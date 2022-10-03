using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCloudImage : MonoBehaviour
{

    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private float colorLerpTime;

    private Color newCloudColor;

    private void Start()
    {
        newCloudColor = sprite.color;
    }

    private void FixedUpdate()
    {
        LerpToNewColor(newCloudColor);
    }

    public void MakeApparent()
    {
        SetNewColorTransparency(1f);
    }

    public void MakeTransparent(float alpha)
    {
        SetNewColorTransparency(alpha);
    }

    private void SetNewColorTransparency(float alpha)
    {
        newCloudColor = new Color(1, 1, 1, alpha);
    }

    private void LerpToNewColor(Color newColor)
    {
        sprite.color = Color.Lerp(sprite.color, newColor, colorLerpTime * Time.fixedDeltaTime);
    }
}
