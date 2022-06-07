using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDetails
{
    public int itemID;

    public ItemType itemType;

    public string itemName;

    public Sprite itemIcon;

    public Sprite itemOnWorldSprite;

    public string itemDescription;

    public int itemUseRadius;

    public bool canPickedup;

    public bool canDropped;

    public bool canCarried;

    public int itemPrice;

    [Range(0,1)]
    public float sellPercentage;

}

[System.Serializable]
public struct InventoryItem
{
    public int itemID;

    public int itemAmount;
}

[System.Serializable]
public class AnimatorType
{
    public PartType partType;
    public PartName partName;

    public AnimatorOverrideController overrideController;
}

[System.Serializable]
public class SerializableVector3
{
    public float x, y, z;

    public SerializableVector3(Vector3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.y;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    public Vector2Int ToVector2Int()
    {
        return new Vector2Int((int)x, (int)y);
    }

}

public class SceneItem
{
    public int itemID;
    public SerializableVector3 position;
}


[System.Serializable]
public class TileProperty
{
    public Vector2Int tileCoordinate;

    public bool boolTypeValue;

    public GridType gridType;
}

public class TileDetails
{
    public int gridX, gridY;

    public bool canDig,canDropItem,canPlaceFurniture,isNPCObstacle;

    public int daySinceDig = -1;

    public int daySinceWatered=-1;

    public int seedItemID=1;

    public int growthDays=-1;

    public int daySinceLastHarvest=-1;


}
