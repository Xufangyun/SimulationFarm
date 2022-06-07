using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFarm.Inventory;

public class Item : MonoBehaviour
{
    public int itemID;

    private SpriteRenderer spriteRenderer;

    public ItemDetails itemDetails;

    private BoxCollider2D coll;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if (itemID != 0)
        {
            Init(itemID);
        }
    }

    public void Init(int ID)
    {
        itemID = ID;
        itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
        if (itemDetails != null)
        {
            spriteRenderer.sprite = itemDetails.itemOnWorldSprite != null ? itemDetails.itemOnWorldSprite : itemDetails.itemIcon;

            Vector2 newsize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
            coll.size = newsize;
            coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);
        }
    }
}
