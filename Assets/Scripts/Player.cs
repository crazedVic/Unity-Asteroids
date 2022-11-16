using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.4/manual/Actions.html
    // https://gamedev.soarhap.com/unity-new-input-system/#handling-keyboard-input
    // https://unitycodemonkey.com/video.php?v=caNvN4rRrh0
    InputAction move, shoot;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions["Move"];
        shoot = playerInput.actions["Shoot"];
    }

    // Update is called once per frame
    void Update()
    {
       // var m = move.ReadValue();
        var f = shoot.ReadValue<float>();
       // Debug.Log(m);
        Debug.Log(f);
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
