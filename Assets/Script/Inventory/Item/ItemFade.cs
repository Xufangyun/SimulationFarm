using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemFade : MonoBehaviour
{

    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// �𽥻ָ���ɫ
    /// </summary>
    public void FadeIn()
    {
        Color targetColor = new Color(1, 1, 1, 1);
        sprite.DOColor(targetColor,Settings.itemFadeDuration);
    }

    /// <summary>
    /// �𽥰�͸��
    /// </summary>
    public void FadeOut()
    {
        Color targetAlpha = new Color(1, 1, 1, Settings.targetAlpha);
        sprite.DOColor(targetAlpha, Settings.itemFadeDuration);
    }
}
