using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorShotScript : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;
    [SerializeField] public GameObject ballPrefab;
    [SerializeField] public GameObject gunpoint; // 銃口の位置

    private float shootVelocity = 100f;
    private float destroyTimer = 2.0f;
    public LayerMask ignoreLayer;

    // Start is called before the first frame update
    void Start()
    {
        // カーソルをテクスチャに変更
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);    
    }

    // Update is called once per frame
    void Update()
    {
        // 右クリックで撃つ
        if (Input.GetButtonDown("Fire1"))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            //// 無視するオブジェクトを除外してRayの当たり判定を行う
            //if (Physics.Raycast(ray,out hit, Mathf.Infinity, -ignoreLayer))
            //{
            //    // rayが当たった場合
            //    Vector3 shootDirection = (hit.point - transform.position).normalized;

            //    ShootBall(shootDirection);
            //}

            // マウスのクリックした位置を取得
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // 画面からの距離を設定

            // マウスのクリックした位置をワールド座標に変換
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // 発射する球の方向を計算
            Vector3 shootDirection = (worldPosition - transform.position).normalized;

            //Debug.Log((worldPosition - transform.position).normalized);

            // 球を発射する関数を呼び出し
            ShootBall(shootDirection);
        }
    }

    /// <summary>
    /// 銃弾を発射
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
    /// 敵を撃つ
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
