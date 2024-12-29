using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpTrigger : MonoBehaviour
{
    private bool active = true;
    private PlayerController player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (active) {
            player.doubleJump = true;
            active = false;
            GetComponent<Animator>().SetBool("catched", true);
        }
    }

    public void DisableAnimation()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
