using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // QUESTION: should I use a List or a Queue? (Or a List of Queues?)
    // public List<Vector2> PositionsHistory = new List<Vector2>();
    public Queue<Vector2> PositionsHistory = new Queue<Vector2>();

    [SerializeField] private float XVelocityK = 5f;

    private float YVelocityForce = 10f;

    private bool firstMove = true;

    [SerializeField] private GameObject enemyPrefab;

    private void FixedUpdate()
    {
        if (Pause()) return;

        // save position for first movement check
        Vector3 lastPosition = transform.position;

        // jump/vertical movement
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, YVelocityForce), ForceMode2D.Impulse);
        }


        // horizontal movement
        float move = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(move * XVelocityK, 0, 0);
        transform.position += movement * Time.fixedDeltaTime;

        if (firstMove && transform.position != lastPosition)
        {
            firstMove = false;
            EnqueueEnemySpawn();
        }

        // save the player position to the attribute PositionsHistory
        // PositionsHistory.Add(transform.position);

        // save the player position to the attribute PositionsHistory
        PositionsHistory.Enqueue(transform.position);
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
        yield return new WaitForSeconds(2f); // wait for 2 seconds before spawning

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
