using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

public class RandomTilemapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] availableTiles; // Przypisz tile1, tile2, tile3 w Inspectorze
    public int width = 8;
    public int height = 16;

    // Twarde wspó³rzêdne pocz¹tkowe
    private int startX = -8;
    private int startY = -4;

    void Start()
    {
        GenerateRandomMap();
    }

    void GenerateRandomMap()
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileBase randomTile = availableTiles[Random.Range(0, availableTiles.Length)];

                // Generowanie od startX, startY
                Vector3Int tilePosition = new Vector3Int(x + startX, y + startY, 0);
                tilemap.SetTile(tilePosition, randomTile);
            }
        }
    }
}
