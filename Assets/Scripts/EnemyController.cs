using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// require SceneMangement to restart the game
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{ 
    private PlayerController player;
    // private int index = 0;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {   
        if(Pause()) return;

        Vector3 fromQueue = player.PositionsHistory.Dequeue();
        // Log the position of the player
        // Debug.Log(fromQueue);
        // transform.position = Vector2.MoveTowards(transform.position, player.PositionsHistory[index], 0.1f);
        // transform.position = Vector2.MoveTowards(transform.position, fromQueue, 0.1f);
        transform.position = fromQueue;
        // index++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private bool Pause()
    {
        return false;    
    }
}
