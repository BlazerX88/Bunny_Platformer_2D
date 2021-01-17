using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float thrust = 10.0f;
    public LayerMask GroundLayer;
    public Animator animator;
    public float RunSpeed = 1.5f;
    private static PlayerController sharedInstance;
    private const string HIGHEST_SCORE_KEY = "HighestScore";
    private Vector3 initialposition;
    private Vector2 initialvelocity;
    private float initialGravity;
    // Start is called before the first frame update

    private void Awake()
    {
        sharedInstance = this;
        initialposition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        initialvelocity = rb.velocity;
        animator.SetBool("isAlive", true);
        initialGravity = rb.gravityScale;
    }

    public static PlayerController GetInstance()
    {
        return sharedInstance;
    }
    public void StartGame()
    {
        animator.SetBool("isAlive", true);
        transform.position = initialposition;
        rb.velocity = initialvelocity;
        rb.gravityScale = initialGravity;

    }

    private void FixedUpdate()
    {
        GameState currState = GameManager.GetInstance().currentgamestate;
        if (currState == GameState.InGame)
        {
            if (rb.velocity.x < RunSpeed)
            {
                rb.velocity = new Vector2(RunSpeed, rb.velocity.y);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        bool canJump = GameManager.GetInstance().currentgamestate == GameState.InGame;
        bool isOnTheGround = IsOnTheGround();
        animator.SetBool("isGrounded", isOnTheGround);
        if (canJump && (Input.GetMouseButtonDown(0)
            || Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.W)
            ) && isOnTheGround
            )
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
    }
    bool IsOnTheGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1.0f, GroundLayer.value);
    }

    public void KillPlayer()
    {
        animator.SetBool("isAlive", false);
        GameManager.GetInstance().GameOver();

        int HighestScore = PlayerPrefs.GetInt(HIGHEST_SCORE_KEY);
        int currentScore = GetDistance();
        if(currentScore > HighestScore)
        {
            PlayerPrefs.SetInt(HIGHEST_SCORE_KEY, currentScore);
        }
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        GameManager.GetInstance().GameOver();
     } 

    public int GetDistance()
    {
        var distance = (int)Vector2.Distance(initialposition, transform.position);
        return distance;
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt(HIGHEST_SCORE_KEY);
    }
}