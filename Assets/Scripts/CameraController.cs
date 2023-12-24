using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの追従スクリプト
/// これをつけずにメインカメラを飛行機の子オブジェクトとしても良いが，自然な見た目にならない
/// 飛行機からDelayを入れる
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("An array of transforms representing camera positions")]
    [SerializeField] Transform[] povs;
    [Tooltip("The speed at which the camera follows the plane")]
    [SerializeField] float speed;

    private int index = 1;
    private Vector3 target;

    private void Awake()
    {
        transform.position = povs[index].position;
    }
    private void Update()
    {
        // Numbers 1-4 represent different povs (you can add more)
        if (Input.GetKeyDown(KeyCode.Alpha1)) index = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) index = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) index = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) index = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5)) index = 4;

        // Set our target to the relevant POV.
        target = povs[index].position;
    }

    private void FixedUpdate()
    {
        // Move camera to desired position/orientation. Must be in FixedUpdate to avoid camera jitters.
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.forward = povs[index].forward;
    }
}
