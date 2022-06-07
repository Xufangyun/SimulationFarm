using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFarm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("��Ʒ����")]
        public ItemDataList_SO itemDataList_SO;

        [Header("��������")]
        public InventoryBag_SO playerBag;

        private void Start()
        {
            EventHandler.CallUpdataInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        /// <summary>
        /// ͨ��ID������Ʒ��Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataList_SO.ItemDetailsList.Find(i => i.itemID == ID);
        }

        /// <summary>
        /// �����Ʒ��Player����
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toDestroy">�Ƿ�������Ʒ</param>
        public void AddItem(Item item, bool toDestroy)
        {
            var index = GetItemIndexInBag(item.itemID);

            AddItemAtIndex(item.itemID, index, 1);

            if (toDestroy)
            {
                Destroy(item.gameObject);
            }

            //����UI
            EventHandler.CallUpdataInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        /// <summary>
        /// ��鱳���Ƿ��п�λ
        /// </summary>
        /// <returns></returns>
        private bool CheckBagCapacity()
        {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == 0) return true;//TODO(����)
            }
            return false;
        }

        /// <summary>
        /// ͨ������ID��ȡ������Ʒ�ڱ����е�λ��
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private int GetItemIndexInBag(int ID)
        {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == ID) return i;
            }
            return -1;
        }

        /// <summary>
        /// ��ָ���������λ�������Ʒ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="index"></param>
        /// <param name="amount"></param>
        private void AddItemAtIndex(int ID, int index, int amount)
        {
            if (index == -1 && CheckBagCapacity())//����û������Ʒ��ͬʱ�����п�λ
            {
                var item = new InventoryItem { itemID = ID, itemAmount = amount };
                for (int i = 0; i < playerBag.itemList.Count; i++)
                {
                    if (playerBag.itemList[i].itemID == 0)
                    {
                        playerBag.itemList[i] = item;
                        break;
                    }
                }
            }
            else     //�����Ѿ����ڸ���Ʒ
            {
                int currentAmount = playerBag.itemList[index].itemAmount+amount;
                var item = new InventoryItem { itemID = ID, itemAmount = currentAmount };

                playerBag.itemList[index] = item;
            }
        }

        /// <summary>
        /// ������Χ�ڽ�����Ʒ
        /// </summary>
        /// <param name="fromIndex">��ʼ���</param>
        /// <param name="targetIndex">Ŀ���������</param>
        public void SwapItem(int fromIndex,int targetIndex)
        {
            InventoryItem currentItem = playerBag.itemList[fromIndex];
            InventoryItem targetItem = playerBag.itemList[targetIndex];

            if (targetItem.itemID != 0)
            {
                playerBag.itemList[fromIndex] = targetItem;
                playerBag.itemList[targetIndex] = currentItem;
            }
            else
            {
                playerBag.itemList[targetIndex] = currentItem;
                playerBag.itemList[fromIndex] = new InventoryItem();
            }

            EventHandler.CallUpdataInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }
    }
}
