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
    /// ��ָ��λ��������Ʒ
    /// </summary>
    /// <param name="ID">��ƷID</param>
    /// <param name="pos">��������</param>
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
            //�ҵ����ݾ͸���Item�����б�
            sceneItemDict[SceneManager.GetActiveScene().name] = currentSceneItems;
        }
        else   //������³���
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
                //�峡
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
