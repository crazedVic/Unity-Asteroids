using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    Rigidbody2D rb; //references the rigidbody attached to the asteroid
    Collider2D myCollider; //references the circle collider attached to the asteroid
    SpriteRenderer sr; // references the image being used by the asteroid

    [SerializeField]
    [Range(50.0f, 200.0f)]
    float baseSpeed = 70.0f;

    float cornerOffset = 1.0f;
    float teleportOffset = 0.2f;

    public BoxCollider2D boundsCollider; //will be set by GameManager when prefab instantiated

    [SerializeField]
    float size = 1.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        GetMoving(baseSpeed);
    }
    public void GetMoving(float speed)
    {
        // set size
        transform.localScale = new Vector2(size, size);
        // initially apply a force in a random direction to the asteroid
        float x = Random.Range(boundsCollider.bounds.min.x, boundsCollider.bounds.max.x);
        float y = Random.Range(boundsCollider.bounds.min.y, boundsCollider.bounds.max.y);
        Vector2 spawnLocation = ScreenBounds.spawnLocations[Random.Range(0, ScreenBounds.spawnLocations.Count)];

        transform.position = spawnLocation;// new Vector2(x, y);

        rb.AddRelativeForce(Random.onUnitSphere * speed);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("screen"))
            transform.position = CalculateWrappedPosition(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            Destroy(gameObject);

        }
        // when hit by a bullet, the asteroid will despawn
        // and 3 new asteroids will spawn unless already at smallest size (0.5f)
        // we need to track how many asteroids on the screen, and move to next level if last asteroid dies.
    }

    public Vector2 CalculateWrappedPosition(Vector2 worldPosition)
    {
        bool xBoundResult =
            Mathf.Abs(worldPosition.x) > (Mathf.Abs(boundsCollider.bounds.min.x) - cornerOffset);
        bool yBoundResult =
            Mathf.Abs(worldPosition.y) > (Mathf.Abs(boundsCollider.bounds.min.y) - cornerOffset);

        Vector2 signWorldPosition =
            new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));

        if (xBoundResult && yBoundResult)
        {
            return Vector2.Scale(worldPosition, Vector2.one * -1)
                + Vector2.Scale(new Vector2(teleportOffset, teleportOffset),
                signWorldPosition);
        }
        else if (xBoundResult)
        {
            return new Vector2(worldPosition.x * -1, worldPosition.y)
                + new Vector2(teleportOffset * signWorldPosition.x, teleportOffset);
        }
        else if (yBoundResult)
        {
            return new Vector2(worldPosition.x, worldPosition.y * -1)
                + new Vector2(teleportOffset, teleportOffset * signWorldPosition.y);
        }
        else
        {
            return worldPosition;
        }
    }
}
