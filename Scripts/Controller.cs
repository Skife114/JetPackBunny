using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float moveInput;
    private float speed = 10f;

    private bool isStarted = false;
    private bool isJumping = false;
    private float originalGravityScale;
    public float jumpForce = 5f;
    private float jetpackForce = 0f;
    public float initialJetpackForce = 2f;
    public float maxJetpackForce = 6f;
    public float jetpackAcceleration = 2f;

    private float topScore = 0.0f;

    public Text scoreText;
    public Text startText;

    public GameObject jetpack; // Reference to the jetpack game object

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalGravityScale = rb2d.gravityScale;

        rb2d.gravityScale = 0;
        rb2d.velocity = Vector2.zero;

        jetpack.transform.Find("leftFlame").gameObject.SetActive(false);
        jetpack.transform.Find("rightFlame").gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isStarted)
        {
            isStarted = true;
            startText.gameObject.SetActive(false);
            rb2d.gravityScale = 5f;
        }

        if (isStarted)
        {
            if (rb2d.velocity.y > 0 && transform.position.y > topScore)
            {
                topScore = transform.position.y;
            }

            scoreText.text = "Score: " + Mathf.Round(topScore).ToString();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            rb2d.gravityScale = 0f;
            jetpack.transform.Find("leftFlame").gameObject.SetActive(true);
            jetpack.transform.Find("rightFlame").gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            rb2d.gravityScale = originalGravityScale;
            jetpack.transform.Find("leftFlame").gameObject.SetActive(false);
            jetpack.transform.Find("rightFlame").gameObject.SetActive(false);
        }

        // Check if the player falls too far below the screen
        if (transform.position.y < -30f)
        {
            TriggerDeath();
        }
    }

    void FixedUpdate()
    {
        if (isStarted)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);

            if (isJumping)
            {
                if (jetpackForce < maxJetpackForce)
                {
                    jetpackForce += jetpackAcceleration * Time.fixedDeltaTime;
                }

                if (rb2d.velocity.y > maxJetpackForce)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, maxJetpackForce);
                }

                rb2d.AddForce(new Vector2(0f, jetpackForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            }
        }
        else
        {
            jetpackForce = initialJetpackForce;
        }
    }

    public void TriggerDeath()
    {
        // Reset the player's position to the starting position
        transform.position = Vector3.zero;

        // Reset the player's velocity to zero
        rb2d.velocity = Vector2.zero;

        // Reset the top score
        topScore = 0f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
