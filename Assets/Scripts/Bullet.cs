using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("screen") || collision.CompareTag("asteroid"))
            Destroy(gameObject);
    }

    /* TODO
     * Player has 4 lives, extra life every 10000 points
     * Starting asteroids need to be much smaller
     * Bullets must live the width of the screen, with wrapping
     * Smaller asteroids travel faster - so adjust size and mass of each
     * UFO spawns random y and travels steady x
     * Each level 2 more asteroids spawn, starting with 4.
     * Bullet speed is much faster than what i have currently
     * You can see propulsion on ship when you hit thrusters
     * the force of the bullet will cause the asteroid pieces to move in same 
     * current direction but with perpendicular force applied
     * 
     * 
     */

}
