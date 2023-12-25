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
    public float maxThrust = 200f;�@// thrust = ����
    [Tooltip("How responsive the plane is when rolling, pitching and yawing.")]
    public float responsiveness = 10f;
    [Tooltip("How much lift force this plane generates as it gains speed.")]
    public float lift = 135f; // �X�s�[�h���オ��Ɨg�͂�����

    private float planeMass = 400f;
    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;
    // �����̑���
    
    private float responseModifier{
        get{
            return (rb.mass/10f)*responsiveness;
        }
    }

    /// <summary>
    /// �G���W����
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
            // �G���W�������X���b�g���ɕ����đ傫������
            engineSound.volume = throttle * 0.01f;

        }
        else
        {
            engineSound.volume = 0f;
        }
        updateHud();


        propella.Rotate(Vector3.right * throttle);
    }

    // ��莞�Ԃ��ƂɌĂ΂��
    // �����I�ȋ������g���Ƃ���FixedUpdate()���g��
    private void FixedUpdate()
    {
        // Apply force to our plane.
        rb.AddForce(transform.forward * maxThrust * throttle); // ���i��
        rb.AddTorque(transform.up * yaw * responseModifier); // ��]�� yaw ����
        rb.AddTorque(-transform.right * pitch * responseModifier); // ��]�� pitch �@��̏グ����
        rb.AddTorque(-transform.forward * roll * responseModifier); // ��]�� roll 

        rb.AddForce(Vector3.up * rb.velocity.magnitude * lift); // �g��

    }

    /// <summary>
    /// HUD���X�V
    /// </summary>
    private void updateHud()
    {
        hud.text = "Throttle: " + throttle.ToString("F0") + "%\n"; //F0�Ƃ��ď����_�ȉ�������
        hud.text += "AirSpeed: " + (rb.velocity.magnitude * 3.6f).ToString("F0") + "km/h\n"; // m/s -> km/h
        hud.text += "Altitude: " + transform.position.y.ToString("F0") + " m";
    }

}
