using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MFarm.Map;

public class CursorManager : MonoBehaviour
{
    public Sprite normal, tool, seed,item;

    private Sprite currentSprite;//存储当前鼠标图片

    private Image cursorImage;

    private RectTransform cursorCanvas;

    //鼠标检测
    private Camera mainCamera;
    
    private Grid currentGrid;

    private Vector3Int mouseGridPos;

    private Vector3 mouseWorldPos;

    private bool cursorEnable;

    private bool cursorPositionValid;

    private ItemDetails currentItem;

    private void Start()
    {
        cursorCanvas = GameObject.FindGameObjectWithTag("CursorCanvas").GetComponent<RectTransform>();
        cursorImage = cursorCanvas.GetChild(0).GetComponent<Image>();

        currentSprite = normal;
        SetCursorImage(currentSprite);

        mainCamera = Camera.main;
    }


    private void Update()
    {
        if (cursorCanvas == null) return;

        cursorImage.transform.position = Input.mousePosition;

        if (!InteractWithUI()&&cursorEnable)
        {
            SetCursorImage(currentSprite);
            CheckCursorValid();
        }
        else
        {
            SetCursorImage(normal);
        }
        
    }

    


    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.BeforeSceneUnLoadEvent += OnBeforeSceneUnLoadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.BeforeSceneUnLoadEvent += OnBeforeSceneUnLoadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }

    private void OnBeforeSceneUnLoadEvent()
    {
        cursorEnable = false;
    }

    private void OnAfterSceneLoadedEvent()
    {
        currentGrid = FindObjectOfType<Grid>();
    }

    #region 设置鼠标样式
    /// <summary>
    /// 设置鼠标图片
    /// </summary>
    /// <param name="itemDetails"></param>
    /// <param name="isSelected"></param>
    private void SetCursorImage(Sprite sprite)
    {
        cursorImage.sprite = sprite;
        cursorImage.color = new Color(1, 1, 1, 1);
    }
    /// <summary>
    /// 设置鼠标可用
    /// </summary>
    private void SetCursorValid()
    {
        cursorPositionValid = true;
        cursorImage.color = new Color(1, 1, 1, 1);
    }

    /// <summary>
    /// 设置鼠标不可用
    /// </summary>
    private void SetCursorUnValid()
    {
        cursorPositionValid = false;
        cursorImage.color = new Color(1, 0, 0, 0.5f);
    }
    #endregion

    /// <summary>
    /// 物品选择事件函数
    /// </summary>
    /// <param name="itemDetails"></param>
    /// <param name="isSelected"></param>
    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        currentItem = itemDetails;

        if (!isSelected)
        {
            currentItem = null;
            cursorEnable = false;
            currentSprite = normal;
        }
        else
        {
            currentItem = itemDetails;
            switch (itemDetails.itemType)
            {
                case ItemType.Seed:
                    currentSprite = seed;
                    break;
                case ItemType.Commodity:
                    currentSprite = item;
                    break;
                case ItemType.ChopTool:
                    currentSprite = tool;
                    break;
                default:
                    currentSprite = normal;
                    break;
            }
            cursorEnable = true;
        }
    }

    private void CheckCursorValid()
    {
        mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseGridPos = currentGrid.WorldToCell(mouseWorldPos);

        TileDetails currentTile = GridManager.Instance.GetTileDetailsOnMousePosition(mouseGridPos);

        if (currentTile!=null)
        {
            Debug.Log("1");
            switch (currentItem.itemType)
            {
                case ItemType.Commodity:
                    Debug.Log("2");
                    if (currentTile.canDropItem && currentItem.canDropped)
                    {
                        Debug.Log("3");
                        SetCursorValid();
                    }
                    else 
                        SetCursorUnValid();
                    break;
            }
        }
        else
        {
            SetCursorUnValid();
        }
    }


    /// <summary>
    /// 判断是否与UI互动
    /// </summary>
    /// <returns></returns>
    private bool InteractWithUI()
    {
        if (EventSystem.current != null & EventSystem.current.IsPointerOverGameObject())
            return true;
        else
            return false;
    }

}
