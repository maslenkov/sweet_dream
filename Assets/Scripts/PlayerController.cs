using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // QUESTION: should I use a List or a Queue? (Or a List of Queues?)
    // public List<Vector2> PositionsHistory = new List<Vector2>();

    public Queue<Vector2> PositionsHistory = new Queue<Vector2>();

    private Rigidbody2D rb;

    private float XVelocityK = 5f;

    private float jumpForce = 10f;

    private bool firstMove = true;

    [SerializeField] private GameObject enemyPrefab;

    private Vector3 previousPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        previousPosition = transform.position;
    }

    private void Update()
    {
        // Pauses the game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (Pause()) return;

        // horizontal movement
        float move = Input.GetAxis("Horizontal");
        if (move != 0) 
        {
            rb.velocity = new Vector2(move * XVelocityK, rb.velocity.y);
        }

        // jump/vertical movement
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // check if the player has ever moved
        if (firstMove && transform.position != previousPosition)
        {
            firstMove = false;
            EnqueueEnemySpawn();
        }

        // save the player position to the attribute PositionsHistory array
        // PositionsHistory.Add(transform.position);

        // save the player position to the attribute PositionsHistory queue
        PositionsHistory.Enqueue(transform.position);

        // update the previous position
        previousPosition = transform.position;
    }

    private bool Pause() 
    {
        return false;
    }

    private void EnqueueEnemySpawn()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(1f); // wait for 2 seconds before spawning

        // string enemyName = "Enemy"; // name of the enemy prefab
        // GameObject enemyPrefab = Resources.Load<GameObject>(enemyName);

        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Enemy prefab not found in Resources folder.");
        }
    }
}
