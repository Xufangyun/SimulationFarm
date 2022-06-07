using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


[ExecuteInEditMode]
public class GridMap : MonoBehaviour
{
    public MapData_SO mapDate;

    public GridType gridType;

    private Tilemap currentTilemap;

    private void OnEnable()
    {
        if (!Application.IsPlaying(this))
        {
            currentTilemap = GetComponent<Tilemap>();

            if (mapDate != null)
            {
                mapDate.tileProperties.Clear();
            }

        }
    }

    private void OnDisable()
    {
        if (!Application.IsPlaying(this))
        {
            currentTilemap = GetComponent<Tilemap>();

            UpdateTileProperties();

#if UNITY_EDITOR
            if (mapDate != null)
                EditorUtility.SetDirty(mapDate);
#endif
        }
    }

    private void UpdateTileProperties()
    {
        currentTilemap.CompressBounds();

        if (!Application.IsPlaying(this))
        {
            if (mapDate != null)
            {
                Vector3Int startPos = currentTilemap.cellBounds.min;
                Vector3Int endPos = currentTilemap.cellBounds.max;

                for(int x = startPos.x; x< endPos.x; x++)
                {
                    for (int y=startPos.y; y < endPos.y; y++)
                    {
                        TileBase tile = currentTilemap.GetTile(new Vector3Int(x, y, 0));

                        if (tile != null)
                        {
                            TileProperty newTile = new TileProperty
                            {
                                tileCoordinate = new Vector2Int(x, y),

                                gridType = this.gridType,

                                boolTypeValue = true,
                            };

                            mapDate.tileProperties.Add(newTile);
                        }
                    }
                }
            }
        }
    }
}
