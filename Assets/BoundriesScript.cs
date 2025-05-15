using UnityEngine;

public class BoundriesScript : MonoBehaviour
{

    public float thickness = 1f; //Thickness of collider
    void Start()
    {
        Camera cam = Camera.main;
        //float height = 2f * cam.orthographicSize;
        //float width = height * cam.aspect;
        float width = 16f;
        float height = 9f;


        //Vector3 camPos = cam.transform.position;

        //// Boundries
        //CreateBoundary(new Vector2(camPos.x, camPos.y + height / 2 + thickness / 2),
        //    new Vector2(width, thickness)); // Top

        //CreateBoundary(new Vector2(camPos.x, camPos.y - height / 2 - thickness / 2),
        //    new Vector2(width, thickness)); // Bottom
        //CreateBoundary(new Vector2(camPos.x - width / 2 - thickness / 2, camPos.y),
        //    new Vector2(thickness, height)); // Left
        //CreateBoundary(new Vector2(camPos.x + width / 2 + thickness / 2, camPos.y),
        //    new Vector2(thickness, height)); // Right


        CreateBoundary(new Vector2(0, height / 2 + thickness / 2), new Vector2(width, thickness));  // Top
        CreateBoundary(new Vector2(0, -height / 2 - thickness / 2), new Vector2(width, thickness)); // Bottom
        CreateBoundary(new Vector2(-width / 2 - thickness / 2, 0), new Vector2(thickness, height)); // Left
        CreateBoundary(new Vector2(width / 2 + thickness / 2, 0), new Vector2(thickness, height));  // Right

    }
    void CreateBoundary(Vector2 position, Vector2 size)
    {
        GameObject boundary = new GameObject("Boundary");
        boundary.transform.parent = transform;
        boundary.transform.localPosition = position;

        BoxCollider2D collider = boundary.AddComponent<BoxCollider2D>();
        collider.size = size;
        collider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
