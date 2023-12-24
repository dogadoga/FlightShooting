using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidi : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject plane;
    private void OnCollisionEnter(Collision collision)
    {
        if(!GameManager.I.isEnded)
        if (collision.gameObject.CompareTag("Water") || collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosionPrefab,plane.transform.position,plane.transform.rotation);
            explosionPrefab.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
            GameManager.I.isPlayerDefeated = true;
            GameManager.I.isEnded = true;
            GameManager.I.EndGame();
            Debug.Log("è’ìÀ");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("aaaa");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
