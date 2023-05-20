using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private bool isFalling = false;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isFalling)
        {
            transform.position -= Vector3.up * 10f * Time.deltaTime; // Make the platform fall down
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isFalling = true;
        }
    }

    public void ResetPlatform()
    {
        isFalling = false;
        transform.position = initialPosition;
    }
}
