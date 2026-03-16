using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerShooter _shooter;
    [SerializeField] private Animator _animator;

    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _aimPivot;
    [SerializeField] private LayerMask _hitMask;

    [SerializeField] private Image _crosshair;

    [Header("Settings")]
    [SerializeField] private float _aimSensitivity = 1.5f;
    [SerializeField] private float _rotationSpeed = 15f;
    [SerializeField] private float _minPitch = -40f;
    [SerializeField] private float _maxPitch = 60f;
    [SerializeField] private float _stepInterval = 0.4f;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private CharacterController _characterController;
    private Transform _cameraTransform;

    private PlayerControls _controls;

    private Vector2 _moveInput;
    private float _currentPitch;

    private float _stepTimer;

    public PlayerControls PlayerControls => _controls;

    public event Action OnMoveSound;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraTransform = _camera.transform;

        _controls = new PlayerControls();

        _mover.Initialize(_characterController);

        BindInput();
    }

    private void Start()
    {
        _shooter.Init();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleAnimator();
        HandleSteps();

        _shooter.Cooldown();
        _mover.ApplyGravity();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = CalculateMoveDirection();
        _mover.Move(moveDirection);
    }

    private void HandleRotation()
    {
        if (_moveInput.sqrMagnitude > 0.01f)
        {
            RotatePlayerToCamera();
        }

        _aimPivot.localRotation = Quaternion.Euler(_currentPitch, 0f, 0f);
    }

    private void HandleAnimator()
    {
        float speed = _moveInput.magnitude;
        _animator.SetFloat(SpeedHash, speed, 0.1f, Time.deltaTime);
    }

    private void HandleSteps()
    {
        if (_moveInput.sqrMagnitude < 0.01f)
        {
            return;
        }

        _stepTimer -= Time.deltaTime;

        if (_stepTimer <= 0f)
        {
            OnMoveSound?.Invoke();
            _stepTimer = _stepInterval;
        }
    }

    private Vector3 CalculateMoveDirection()
    {
        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;

        forward.y = 0; 
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * _moveInput.y + right * _moveInput.x;
    }

    private void RotatePlayerToCamera()
    {
        Vector3 cameraForward = _cameraTransform.forward;
        cameraForward.y = 0f;

        if (cameraForward.sqrMagnitude < 0.001f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void BindInput()
    {
        _controls.Player.Move.performed += OnMove;
        _controls.Player.Move.canceled += OnMoveCanceled;

        _controls.Player.Look.performed += OnLook;

        _controls.Player.Jump.performed += OnJump;

        _controls.Player.Attack.performed += OnAttack;

        _controls.Player.Reload.performed += OnReload;

        _controls.Player.Weapon1.performed += OnWeapon1;
        _controls.Player.Weapon2.performed += OnWeapon2;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        _moveInput = Vector2.zero;
    }

    private void OnLook(InputAction.CallbackContext ctx)
    {
        Vector2 lookInput = ctx.ReadValue<Vector2>();

        transform.Rotate(Vector3.up * (lookInput.x * _aimSensitivity));

        _currentPitch -= lookInput.y * _aimSensitivity;
        _currentPitch = Mathf.Clamp(_currentPitch, _minPitch, _maxPitch);
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        _mover.Jump();
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        _shooter.Attack(_camera, transform, _hitMask);
    }

    private void OnReload(InputAction.CallbackContext ctx)
    {
        _shooter.Reload();
    }

    private void OnWeapon1(InputAction.CallbackContext ctx)
    {
        _shooter.SelectWeapon(0);
    }

    private void OnWeapon2(InputAction.CallbackContext ctx)
    {
        _shooter.SelectWeapon(1);
    }
}