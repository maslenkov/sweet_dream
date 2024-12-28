using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Queue<Vector2> PositionsHistory = new Queue<Vector2>();

    private Rigidbody2D rb;

    private float enemyAwakeTime = 2f;

    private float xVelocityK = 5f;

    private float jumpForce = 10f;

    private bool firstMove = true;

    private Vector2 pauseVelocity;
    private bool pauseDataSaved = false;

    [SerializeField] private GameObject enemyPrefab;

    private Vector3 previousPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        previousPosition = transform.position;
        PlayerPrefs.SetInt("Pause", 0);
    }

    private void Update()
    {
        if (Pause()) {
            SaveVelocityData();
            return;
        } else {
            RestoreVelocityData();
        }

        // horizontal movement
        float move = Input.GetAxis("Horizontal");
        if (move != 0) 
        {
            rb.velocity = new Vector2(move * xVelocityK, rb.velocity.y);
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

        // save the player position to the attribute PositionsHistory queue
        PositionsHistory.Enqueue(transform.position);

        // update the previous position
        previousPosition = transform.position;
    }

    private bool Pause() 
    {
        return PlayerPrefs.GetInt("Pause", 0) == 1;
    }

    private void EnqueueEnemySpawn()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(enemyAwakeTime); // wait for 2 seconds before spawning

        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Enemy prefab not found in Resources folder.");
        }
    }

    private void SaveVelocityData()
    {
        if (!pauseDataSaved) {
            pauseVelocity = new Vector2(rb.velocity.x, rb.velocity.y);
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            pauseDataSaved = true;
        }
    }

    private void RestoreVelocityData()
    {
        if (pauseDataSaved) {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = pauseVelocity;
            pauseVelocity = Vector2.zero;
            pauseDataSaved = false;
        }
    }
}
