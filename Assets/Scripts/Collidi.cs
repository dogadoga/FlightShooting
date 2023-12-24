using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidi : MonoBehaviour
{
    public GameObject explosionPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Instantiate(explosionPrefab);
            Debug.Log("水没");
        }
        else
        {
            Debug.Log("当たった");
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
