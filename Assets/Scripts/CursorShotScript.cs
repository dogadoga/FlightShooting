using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorShotScript : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;
    [SerializeField] public GameObject ballPrefab;
    [SerializeField] public GameObject gunpoint; // �e���̈ʒu

    private float shootVelocity = 100f;
    private float destroyTimer = 2.0f;
    public LayerMask ignoreLayer;

    // Start is called before the first frame update
    void Start()
    {
        // �J�[�\�����e�N�X�`���ɕύX
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);    
    }

    // Update is called once per frame
    void Update()
    {
        // �E�N���b�N�Ō���
        if (Input.GetButtonDown("Fire1"))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            //// ��������I�u�W�F�N�g�����O����Ray�̓����蔻����s��
            //if (Physics.Raycast(ray,out hit, Mathf.Infinity, -ignoreLayer))
            //{
            //    // ray�����������ꍇ
            //    Vector3 shootDirection = (hit.point - transform.position).normalized;

            //    ShootBall(shootDirection);
            //}

            // �}�E�X�̃N���b�N�����ʒu���擾
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // ��ʂ���̋�����ݒ�

            // �}�E�X�̃N���b�N�����ʒu�����[���h���W�ɕϊ�
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // ���˂��鋅�̕������v�Z
            Vector3 shootDirection = (worldPosition - transform.position).normalized;

            //Debug.Log((worldPosition - transform.position).normalized);

            // ���𔭎˂���֐����Ăяo��
            ShootBall(shootDirection);
        }
    }

    /// <summary>
    /// �e�e�𔭎�
    /// </summary>
    /// <param name="direction"></param>
    void ShootBall(Vector3 direction)
    {
        GameObject ballInstance = Instantiate(ballPrefab, gunpoint.transform.position, gunpoint.transform.rotation);

        Rigidbody ballRigidbody = ballInstance.GetComponent<Rigidbody>();

        ballRigidbody.velocity += direction * shootVelocity;
        //ballRigidbody.AddForce(gunpoint.transform.forward * shootPower);
        Destroy(ballInstance, destroyTimer);
        //ballRigidbody.velocity = direction * shootSpeed;
    }
    
    /// <summary>
    /// �G������
    /// </summary>
    void Shot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Enemy")))
        {
            Debug.Log("Hit Enemy");
        }
    }
}
