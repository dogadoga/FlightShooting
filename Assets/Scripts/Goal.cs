using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject plane;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            if (collision.gameObject.CompareTag("Plane") && GameManager.I.isEnemyDefeated)
            {
                GameManager.I.EndGame();
            }
            else
            {
                Instantiate(explosionPrefab, plane.transform.position, plane.transform.rotation);
                explosionPrefab.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                GameManager.I.isPlayerDefeated = true;
                GameManager.I.EndGame();
            }
        }
        
    }
}
