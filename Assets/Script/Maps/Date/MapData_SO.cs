using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDate_SO", menuName = "Map/MapDate")]

public class MapData_SO : ScriptableObject
{
    [SceneName] public string sceneName;

    public List<TileProperty> tileProperties;
}
