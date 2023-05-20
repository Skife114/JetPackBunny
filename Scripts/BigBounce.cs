using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBounce : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object colliding with the platform is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Do nothing
        }
    }
}
