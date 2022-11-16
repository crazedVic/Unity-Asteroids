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

    public static event Action<Collider2D> ExitTriggerFired;

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
    }

    public void UpdateBoundsSize()
    {
        Debug.Log("resizing bounds");
        float ySize = mainCamera.orthographicSize * 2; //orthographic size is half the viewing area
        Vector2 boxColliderSize = new Vector2(ySize * mainCamera.aspect, ySize);
        boxCollider.size = boxColliderSize;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("asteroid"))
            ExitTriggerFired?.Invoke(collision);
    }

    
}
