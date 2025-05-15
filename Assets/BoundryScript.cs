using UnityEngine;

public class RoomBoundaryBuilder : MonoBehaviour
{
    public float wallThickness = 1f;
    public float inset = 0.5f; // ile do wewn¹trz od granic kamery

    void Start()
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        float roomWidth = camWidth - inset * 2f;
        float roomHeight = camHeight - inset * 2f;

        // Lewa œciana
        CreateWall("Left", new Vector2(-8.5f, 0), new Vector2(wallThickness, 10));
        // Prawa œciana
        CreateWall("Right", new Vector2(8.5f, 0), new Vector2(wallThickness, 10));
        // Górna œciana
        CreateWall("Top", new Vector2(0, 4.5f), new Vector2(roomWidth + wallThickness * 2f, wallThickness));
        // Dolna œciana
        CreateWall("Bottom", new Vector2(0, -4.5f), new Vector2(roomWidth + wallThickness * 2f, wallThickness));
    }

    void CreateWall(string name, Vector2 localPos, Vector2 size)
    {
        GameObject wall = new GameObject("Wall_" + name);
        wall.transform.parent = transform;
        wall.transform.localPosition = localPos;

        var collider = wall.AddComponent<BoxCollider2D>();
        collider.size = size;
    }
}
