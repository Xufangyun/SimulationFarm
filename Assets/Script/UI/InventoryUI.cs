using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MFarm.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public ItemToolTip itemTooltip;

        [Header("拖拽图片")]
        public Image dragItem;

        [Header("玩家背包UI")]

        [SerializeField]private GameObject bagUI;

        [SerializeField] private SlotUI[] playerSlot;

        private bool bagOpen;

        private void Start()
        {
            //给每一个Item赋予对应的序号
            for(int i = 0; i < playerSlot.Length; i++)
            {
                playerSlot[i].slotIndex = i;
            }

            bagOpen = bagUI.activeInHierarchy;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                OpenBagUI();
            }
        }

        private void OnEnable()
        {
            EventHandler.UpdataInventoryUI += OnUpdataInventoryUI;
            EventHandler.BeforeSceneUnLoadEvent += OnBeforeSceneUnLoadEvent;
        }

        private void OnDisable()
        {
            EventHandler.UpdataInventoryUI -= OnUpdataInventoryUI;
            EventHandler.BeforeSceneUnLoadEvent -= OnBeforeSceneUnLoadEvent;
        }

        

        private void OnUpdataInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.Player:
                    for (int i=0;i<playerSlot.Length;i++)
                    {
                        if (list[i].itemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            playerSlot[i].UpdataSlot(item, list[i].itemAmount);
                        }
                        else
                        {
                            playerSlot[i].UpdataEmptySlot();
                        }
                    }
                    break;
            }
        }

        private void OnBeforeSceneUnLoadEvent()
        {
            UpdateSlotHightlight(-1);
        }

        public void OpenBagUI()
        {
            bagOpen = !bagOpen;
            bagUI.SetActive(bagOpen);
        }

        /// <summary>
        /// 更新Slot高亮显示
        /// </summary>
        /// <param name="index"></param>
        public void UpdateSlotHightlight(int index)
        {
            foreach (var slot in playerSlot)
            {
                if (slot.isSelected && slot.slotIndex == index)
                {
                    slot.slotHightlight.gameObject.SetActive(true);
                }
                else
                {
                    slot.isSelected = false;
                    slot.slotHightlight.gameObject.SetActive(false);
                }
            }
        }
    }
}