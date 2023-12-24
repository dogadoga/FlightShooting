using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : DesignPatterns.Singleton<EnemyManager>
{
    public float EnemyLife = 100f;
    public float gunDamage = 5f;
    public float EnemyLifeLeft { get; private set; }
    public GameObject explosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HitEnemy();
            Debug.Log("hit enemy");
            GameObject explosion = Instantiate(explosionPrefab, collision.gameObject.transform.position,collision.gameObject.transform.rotation);
            explosion.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f); // ”š”­‚Ì‘å‚«‚³‚ð•Ï‚¦‚é
        }
    }
    void Start()
    {
        EnemyLifeLeft = EnemyLife;
    }
    /// <summary>
    /// ’e‚ª“G‚É“–‚½‚Á‚½‚Æ‚«‚É”­“®
    /// </summary>
    public void HitEnemy()
    {
        Debug.Log("Hit");
        EnemyLifeLeft -= 5f;
        if (EnemyLifeLeft < 0) Debug.Log("Enemy Defeated");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
