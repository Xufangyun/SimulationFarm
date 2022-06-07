using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public Item itemPrefab;

    private Transform itemParent;

    private Dictionary<string, List<SceneItem>> sceneItemDict = new Dictionary<string, List<SceneItem>>();

    private void OnEnable()
    {
        EventHandler.InstantiateItemInScene += OnInstantiateItemInScene;
        EventHandler.BeforeSceneUnLoadEvent += OnBeforeSceneUnLoadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.RecreatItemInScene += OnRecreatItemInScene;
    }

    private void OnDisable()
    {
        EventHandler.InstantiateItemInScene -= OnInstantiateItemInScene;
        EventHandler.BeforeSceneUnLoadEvent += OnBeforeSceneUnLoadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.RecreatItemInScene -= OnRecreatItemInScene;
    }

    private void OnRecreatItemInScene()
    {
        RecreatAllItem();
    }

    private void OnBeforeSceneUnLoadEvent()
    {
        GetAllSceneItem();
    }

    private void OnAfterSceneLoadedEvent()
    {
        itemParent = GameObject.FindWithTag("ItemParent").transform;
        
    }

    /// <summary>
    /// 在指定位置生成物品
    /// </summary>
    /// <param name="ID">物品ID</param>
    /// <param name="pos">世界坐标</param>
    private void OnInstantiateItemInScene(int ID, Vector3 pos)
    {
        var item = Instantiate(itemPrefab, pos, Quaternion.identity, itemParent);
        item.itemID = ID;
    }

    private void GetAllSceneItem()
    {
        List<SceneItem> currentSceneItems = new List<SceneItem>();

        foreach (var item in FindObjectsOfType<Item>())
        {
            SceneItem sceneItem = new SceneItem
            {
                itemID = item.itemID,
                position = new SerializableVector3(item.transform.position)
            };

            currentSceneItems.Add(sceneItem);
        }
        if(sceneItemDict.ContainsKey(SceneManager.GetActiveScene().name))
        {
            //找到数据就更新Item数据列表
            sceneItemDict[SceneManager.GetActiveScene().name] = currentSceneItems;
        }
        else   //如果是新场景
        {
            sceneItemDict.Add(SceneManager.GetActiveScene().name, currentSceneItems);
        }
    }

    private void RecreatAllItem()
    {
        List<SceneItem> currentsceneItems = new List<SceneItem>();

        if (sceneItemDict.TryGetValue(SceneManager.GetActiveScene().name, out currentsceneItems))
        {
            if (currentsceneItems != null)
            {
                //清场
                foreach (var item in FindObjectsOfType<Item>())
                {
                    Destroy(item.gameObject);
                }

                foreach (var item in currentsceneItems)
                {
                    Item newItem = Instantiate(itemPrefab, item.position.ToVector3(), Quaternion.identity, itemParent);
                    newItem.Init(item.itemID);
                }


            }
        }
    }
}
