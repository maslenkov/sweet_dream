using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// require SceneMangement to restart the game
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{ 
    private PlayerController player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {   
        if (Pause()) return; // if pause is true, do not do anything

        transform.position = player.PositionsHistory.Dequeue();
        transform.rotation = player.RotationsHistory.Dequeue();
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
        return PlayerPrefs.GetInt("Pause", 0) == 1;
    }
}
