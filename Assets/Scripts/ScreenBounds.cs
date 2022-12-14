using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{
    public Camera mainCamera;
    BoxCollider2D boxCollider;

    public static List<Vector2> spawnLocations = new List<Vector2>();

    private float edgeOffset = 0.5f;
    private float spacing = 0.5f;

    private void Awake()
    {
        this.mainCamera.transform.localScale = Vector3.one;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        UpdateBoundsSize();
        BuildAsteroidLocationSpawnArray();
    }

    void BuildAsteroidLocationSpawnArray()
    {

        // let's do left edge first, x is contant
        for (float y = boxCollider.bounds.min.y; y < boxCollider.bounds.max.y; y = y + spacing) 
        {
            spawnLocations.Add(new Vector2(boxCollider.bounds.min.x + edgeOffset, y));
        }
        // right edge - offset, x is constant
        for (float y = boxCollider.bounds.min.y; y < boxCollider.bounds.max.y; y = y + spacing)
        {
            spawnLocations.Add(new Vector2(boxCollider.bounds.max.x - edgeOffset, y));
        }

        // bottom edge x changes, y is constant
        for (float x = boxCollider.bounds.min.x; x < boxCollider.bounds.max.x; x = x + spacing)
        {
            spawnLocations.Add(new Vector2(x, boxCollider.bounds.min.y + edgeOffset));
        }

        // bottom edge x changes, y is constant
        for (float x = boxCollider.bounds.min.x; x < boxCollider.bounds.max.x; x = x + spacing)
        {
            spawnLocations.Add(new Vector2(x, boxCollider.bounds.max.y - edgeOffset));
        }

    }

    public void UpdateBoundsSize()
    {
        // for desktop games, how to detect if window size changes?
        float ySize = mainCamera.orthographicSize * 2; //orthographic size is half the viewing area
        Vector2 boxColliderSize = new Vector2(ySize * mainCamera.aspect, ySize);
        boxCollider.size = boxColliderSize;
    }

    
}
