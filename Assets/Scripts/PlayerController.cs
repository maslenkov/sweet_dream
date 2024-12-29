using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// require SceneMangement to restart the game
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Queue<Vector2>[] PositionsHistory = Enumerable.Range(1,3).Select(i => new Queue<Vector2>()).ToArray();
    public Queue<Quaternion>[] RotationsHistory = Enumerable.Range(1,3).Select(i => new Queue<Quaternion>()).ToArray();
    public bool doubleJump = false;
    public bool inFirstJump = false;
    public bool finished = false;

    private Rigidbody2D rb;

    private float xVelocityK = 5f;

    private float jumpForce = 12f;

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
        if (Pause() || finished) {
            if (finished) {
                TheEnd();
            }
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
        if (Input.GetButtonDown("Jump") && (IsGrounded() || DoubleJumpAllowed()))
        {
            if (IsGrounded())
            {
                inFirstJump = true;
            } else {
                inFirstJump = false;
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // check if the player has ever moved
        if (firstMove && transform.position != previousPosition)
        {
            firstMove = false;
            EnqueueEnemySpawn();
        }

        // save the player position to the attribute PositionsHistory queue
        for (int i = 0; i < EnemyCount(); i++) {
            PositionsHistory[i].Enqueue(transform.position);
            RotationsHistory[i].Enqueue(transform.rotation);
        }

        if (transform.position.y <= (Camera.main.ScreenToWorldPoint(Vector2.zero).y - 0.75)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // update the previous position
        previousPosition = transform.position;
    }

    private bool Pause() 
    {
        return PlayerPrefs.GetInt("Pause", 0) == 1;
    }

    private void EnqueueEnemySpawn()
    {
        for (int i = 0; i < EnemyCount(); i++) {
            StartCoroutine(SpawnEnemy(i, i * EnemyAwakeTime() + EnemyAwakeTime()));
        }
    }

    private IEnumerator SpawnEnemy(int index, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyController>().index = index;
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

    private bool IsGrounded()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 1f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Ground"))
            {
                inFirstJump = false;
                return true;
            }
        }
        return false;
    }

    private bool DoubleJumpAllowed()
    {
        return doubleJump && inFirstJump;
    }

    private int EnemyCount()
    {
        return PlayerPrefs.GetInt("EnemyCount", 1); // default value is 1 enemy (easy)
    }

    private float EnemyAwakeTime()
    {
        return PlayerPrefs.GetFloat("EnemyAwakeTime", 2.5f); // default value is 2.5 seconds (easy)
    }

    private void TheEnd()
    {
        Camera cam = Camera.main.GetComponent<Camera>();
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z), 0.2f);
        cam.orthographicSize = Mathf.SmoothStep(cam.orthographicSize, 0.5f, 0.2f);
        if (cam.orthographicSize <= 0.6f)
        {
            SceneManager.LoadScene("EndComics");
        }
    }
}
