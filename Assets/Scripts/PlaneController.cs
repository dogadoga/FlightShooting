using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneController : MonoBehaviour
{
    [Header("Plane Stats")]
    [Tooltip("How much the throttle ramps up or down.")]
    public float throttleIncrement = 0.2f;
    [Tooltip("Maximum engine thrust when at 100% throttle.")]
    public float maxThrust = 200f;　// thrust = 推力
    [Tooltip("How responsive the plane is when rolling, pitching and yawing.")]
    public float responsiveness = 10f;
    [Tooltip("How much lift force this plane generates as it gains speed.")]
    public float lift = 135f; // スピードが上がると揚力が発生

    private float planeMass = 400f;
    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;
    // 反応の早さ
    
    private float responseModifier{
        get{
            return (rb.mass/10f)*responsiveness;
        }
    }

    /// <summary>
    /// エンジン音
    /// </summary>
    AudioSource engineSound;

    Rigidbody rb;

    [SerializeField] TextMeshProUGUI hud;

    [SerializeField] Transform propella;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = planeMass;
        engineSound = GetComponent<AudioSource>();

    }

    private void HandleInputs()
    {
        // Set rotational values from our axis inputs.
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");

        // Handle throttle value being sure to clamp it betweeen 0 and 100.
        if (Input.GetKey(KeyCode.Space)) throttle += throttleIncrement;
        else if (Input.GetKey(KeyCode.LeftControl)) throttle -= throttleIncrement;
        throttle = Mathf.Clamp(throttle, 0f, 100f);

    }

    private void Update()
    {
        if (GameManager.I.CurrentState == GameManager.GameState.Play)
        {
            HandleInputs();
            // エンジン音をスロットルに併せて大きくする
            engineSound.volume = throttle * 0.01f;

        }
        else
        {
            engineSound.volume = 0f;
        }
        updateHud();


        propella.Rotate(Vector3.right * throttle);
    }

    // 一定時間ごとに呼ばれる
    // 物理的な挙動を使うときはFixedUpdate()を使う
    private void FixedUpdate()
    {
        // Apply force to our plane.
        rb.AddForce(transform.forward * maxThrust * throttle); // 推進力
        rb.AddTorque(transform.up * yaw * responseModifier); // 回転力 yaw 旋回
        rb.AddTorque(-transform.right * pitch * responseModifier); // 回転力 pitch 機首の上げ下げ
        rb.AddTorque(-transform.forward * roll * responseModifier); // 回転力 roll 

        rb.AddForce(Vector3.up * rb.velocity.magnitude * lift); // 揚力

    }

    /// <summary>
    /// HUDを更新
    /// </summary>
    private void updateHud()
    {
        hud.text = "Throttle: " + throttle.ToString("F0") + "%\n"; //F0として小数点以下を消す
        hud.text += "AirSpeed: " + (rb.velocity.magnitude * 3.6f).ToString("F0") + "km/h\n"; // m/s -> km/h
        hud.text += "Altitude: " + transform.position.y.ToString("F0") + " m";
    }

}
