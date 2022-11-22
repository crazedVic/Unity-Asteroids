using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.4/manual/Actions.html
    // https://gamedev.soarhap.com/unity-new-input-system/#handling-keyboard-input
    // https://unitycodemonkey.com/video.php?v=caNvN4rRrh0
    
    float turnDirection = 0f;
    float moveDirection = 0f;

    Rigidbody2D rb;

    float cornerOffset = 1.0f;
    float teleportOffset = 0.2f;

    public BoxCollider2D boundsCollider; //will be set by GameManager when prefab instantiate

    public static System.Action gameOver;

    [Header("Bullet Configuration")]
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject bulletContainer;

    [SerializeField]
    GameObject bulletSpawner;

    [SerializeField]
    [Range(0.2f, 2.0f)]
    float bulletCooldown = 0.9f;

    [SerializeField]
    [Range(150f, 500f)]
    public float bulletSpeed = 200.0f;
    float currentBulletCooldown = 0f;

    [Header("Player Configuration")]
    [SerializeField]
    [Range(80f, 500f)]
    public float turnSpeed = 100.0f;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(currentBulletCooldown > 0)
        {
            currentBulletCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("screen"))
        {
            // wrap to other side
            transform.position = CalculateWrappedPosition(transform.position);
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // hit by something, most likely an asteroid
        if (collision.CompareTag("asteroid"))
        {
            Debug.Log("Death");
            gameOver.Invoke();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.transform.Rotate(new Vector3( 0f,0f,turnDirection * turnSpeed * Time.deltaTime));
        rb.AddForce(rb.transform.up * moveDirection * turnSpeed*Time.deltaTime);
    }

    public void OnShoot()
    {
        Debug.Log("shoot");
        // enforce buttenCooldown
        if (currentBulletCooldown <= 0)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.transform.position = bulletSpawner.transform.position;
            bulletInstance.transform.parent = bulletContainer.transform;
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(rb.transform.up * bulletSpeed);
            currentBulletCooldown = bulletCooldown;
        }
        
    }

    public void OnTurn(InputValue value)
    {
        turnDirection = value.Get<float>();
    }

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<float>();
    }

    public void OnStopTurning(InputValue value)
    {
        turnDirection = 0f;
    }

    public void OnStopMoving(InputValue value)
    {
        moveDirection = 0f;
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
