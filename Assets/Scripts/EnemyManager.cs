using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : DesignPatterns.Singleton<EnemyManager>
{
    public float EnemyLife = 100f;
    public float gunDamage = 5f;
    public float EnemyLifeLeft { get; private set; }
    public GameObject explosionPrefab;
    [SerializeField] TextMeshProUGUI enemyHP;
    [SerializeField] TextMeshProUGUI goToGoal;

    void Start()
    {
        Init();
    }

    /// <summary>
    /// ������
    /// </summary>
    public void Init()
    {
        EnemyLifeLeft = EnemyLife;
    }

    // Update is called once per frame
    void Update()
    {
        updateEnemyHP();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HitEnemy();
            Debug.Log("hit enemy");
            GameObject explosion = Instantiate(explosionPrefab, collision.gameObject.transform.position,collision.gameObject.transform.rotation);
            explosion.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f); // �����̑傫����ς���
            Destroy(collision.gameObject);
        }
    }
    /// <summary>
    /// �e���G�ɓ��������Ƃ��ɔ���
    /// </summary>
    public void HitEnemy()
    {
        Debug.Log("Hit");
        if (EnemyLifeLeft > 0)
        {
            EnemyLifeLeft -= 5f;
        }
        else 
        {
            GameManager.I.isEnemyDefeated = true;
            Debug.Log("Enemy Defeated"); 
        }
    }
    private void updateEnemyHP()
    {
        if(EnemyLifeLeft > 0) 
        {
            enemyHP.text = "Tofu HP: " + EnemyLifeLeft.ToString("F0");
        }
        else
        {
            enemyHP.text = "Tofu ���������I";
            goToGoal.text = "�z�B��֌������Ă�������";
        }
    }

}
