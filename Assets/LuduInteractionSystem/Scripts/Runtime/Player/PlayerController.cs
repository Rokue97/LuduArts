using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController m_CharacterController;
    private const float k_DefaulCameraPitch = 0f;
    private float m_CameraPitch = k_DefaulCameraPitch;

    [SerializeField] private Transform m_CameraTransform;

    [Header("Player Settings")]
    private const float k_DefaultMoveSpeed = 5f;
    private const float k_DefaultMouseSensitivity = 50f;
    private const float k_DefaultCameraClampAngle = 85f;

    [SerializeField] private float m_MoveSpeed = k_DefaultMoveSpeed;
    [SerializeField] private float m_MouseSensitivity = k_DefaultMouseSensitivity;
    [SerializeField] private float m_CameraClampAngle = k_DefaultCameraClampAngle;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference m_MoveAction;
    [SerializeField] private InputActionReference m_LookAction;

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Player Movement

        var inputValue = m_MoveAction.action.ReadValue<Vector2>();
        var moveVector = inputValue.x * transform.right + inputValue.y * transform.forward;

        var finalMove = moveVector * m_MoveSpeed;
        m_CharacterController.Move(finalMove * Time.deltaTime);

        // Player Look
        var lookInputValue = m_LookAction.action.ReadValue<Vector2>();
        var mouseX = lookInputValue.x * m_MouseSensitivity * Time.deltaTime;
        var mouseY = lookInputValue.y * m_MouseSensitivity * Time.deltaTime;

        m_CameraPitch -= mouseY;
        m_CameraPitch = Mathf.Clamp(m_CameraPitch, -m_CameraClampAngle, m_CameraClampAngle);
        m_CameraTransform.localRotation = Quaternion.Euler(m_CameraPitch, 0, 0);

        transform.Rotate(Vector3.up * mouseX);

    }
}
