using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    private Animator[] animators;

    public SpriteRenderer holdItem;

    public List<AnimatorType> animatorType;

    private Dictionary<string, Animator> animatorNameDict = new Dictionary<string, Animator>();
    private void Start()
    {
        animators = GetComponentsInChildren<Animator>();

        foreach (var anim in animators)
        {
            animatorNameDict.Add(anim.name, anim);
        }
    }

    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.BeforeSceneUnLoadEvent += OnBeforeSceneUnLoadEvent;
    }

    

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.BeforeSceneUnLoadEvent -= OnBeforeSceneUnLoadEvent;
    }

    private void OnBeforeSceneUnLoadEvent()
    {
        holdItem.enabled = false;
        SwitchAnimator(PartType.None);
    }

    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        PartType currentType;
        switch (itemDetails.itemType)
        {
            case ItemType.Seed:
                currentType = PartType.Carry;
                break;
            case ItemType.Commodity:
                currentType = PartType.Carry;
                break;
            default:
                currentType = PartType.None;
                break;
        }

        if (isSelected == false)
        {
            currentType = PartType.None;
            holdItem.enabled = false;
        }
        else
        {
            if (currentType == PartType.Carry)
            {
                holdItem.sprite = itemDetails.itemOnWorldSprite;
                holdItem.enabled = true;
            }
        }

        SwitchAnimator(currentType);
    }

    private void SwitchAnimator(PartType partType)
    {
        foreach (var item in animatorType)
        {
            if (item.partType == partType)
            {
                animatorNameDict[item.partName.ToString()].runtimeAnimatorController = item.overrideController; 
            }
        }
    }
}
