using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int completedLevels = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (completedLevels == 0)
            {
                completedLevels++;
                StartCoroutine(MoveCamera());
            }
        }
    }

    private IEnumerator MoveCamera()
    {
        Vector3 targetPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 5, Camera.main.transform.position.z);
        while (Vector3.Distance(Camera.main.transform.position, targetPosition) > 0.01f)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, targetPosition, 0.1f);
            yield return null; // Wait for the next frame
        }
        Camera.main.transform.position = targetPosition; // Ensure the camera reaches the exact target position
    }
}
