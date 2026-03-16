using System;
using UnityEngine;

[Serializable]
public class PlayerMover
{
    private const float GroundStickForce = -2f;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _jumpForce = 7f;

    private CharacterController _controller;
    private float _verticalVelocity;

    public void Initialize(CharacterController controller)
    {
        _controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }

    public void ApplyGravity()
    {
        if (_controller.isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = GroundStickForce;
            return;
        }

        _verticalVelocity -= _gravity * Time.deltaTime;
    }

    public void Jump()
    {
        if (!_controller.isGrounded)
        {
            return;
        }

        _verticalVelocity = _jumpForce;
    }

    public void Move(Vector3 direction)
    {
        Vector3 horizontal = direction * _speed;
        Vector3 velocity = new Vector3(horizontal.x, _verticalVelocity, horizontal.z);

        _controller.Move(velocity * Time.deltaTime);
    }
}