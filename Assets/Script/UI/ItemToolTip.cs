using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI nameText;

    [SerializeField] private TextMeshProUGUI typeText;

    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private Text valueText;

    [SerializeField] private GameObject bottomPart;

    public void SetupTooltip(ItemDetails itemDetails,SlotType slotType)
    {
        nameText.text = itemDetails.itemName;

        typeText.text = GetItemType(itemDetails.itemType);

        descriptionText.text = itemDetails.itemDescription;

        if(itemDetails.itemType==ItemType.Seed|| itemDetails.itemType == ItemType.Commodity || itemDetails.itemType == ItemType.Furniture)
        {
            bottomPart.SetActive(true);

            var price = itemDetails.itemPrice;
            if (slotType == SlotType.Bag)
            {
                price = (int)(itemDetails.sellPercentage*price);
            }
            valueText.text = price.ToString();
        }
        else
        {
            bottomPart.SetActive(false);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    private string GetItemType(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Seed:
                    return "����";
            case ItemType.Commodity:
                    return "��Ʒ";
            case ItemType.Furniture:
                    return "�Ҿ�";
            case ItemType.BreakTool:
            case ItemType.ChopTool:
            case ItemType.CollectTool:
            case ItemType.HoeTool:
            case ItemType.ReapTool:
            case ItemType.WaterTool:
                    return "����";
            default:
                return"��";
        }
    }
}
