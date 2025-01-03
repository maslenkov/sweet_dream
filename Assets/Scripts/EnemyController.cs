using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// require SceneMangement to restart the game
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{ 
    private PlayerController player;
    public int index;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {   
        if (Pause() || Finshed()) return; // if pause is true, do not do anything

        transform.position = player.PositionsHistory[index].Dequeue();
        transform.rotation = player.RotationsHistory[index].Dequeue();
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

    private bool Finshed()
    {
        return player.finished;
    }
}
