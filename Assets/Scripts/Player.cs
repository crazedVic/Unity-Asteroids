using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.4/manual/Actions.html
    // https://gamedev.soarhap.com/unity-new-input-system/#handling-keyboard-input
    // https://unitycodemonkey.com/video.php?v=caNvN4rRrh0
   

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnShoot()
    {
        Debug.Log("shoot");
    }

    public void OnTurn(InputValue value)
    {
        Debug.Log(value.Get<float>());
    }

    public void OnMove(InputValue value)
    {
        Debug.Log(value.Get<float>());
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // hit by something, most likely an asteroid
        if (collision.CompareTag("asteroid"))
        {
            Debug.Log("Death");
        }
    }
}
