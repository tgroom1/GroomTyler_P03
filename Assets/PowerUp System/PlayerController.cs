using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelecity;

    [SerializeField] private float _moveSpeed;

    // gravity
    private float _gravity = -9.81f;
    [SerializeField] private float _gravityMultiplier = 3.0f;
    private float _velocity;

    // Jumping
    [SerializeField] private float _jumpPower;


    private bool _isSpeedBoostActive;
    private bool _isJumpBoostActive;
    private bool _isSizeBoostActive;
    private bool _isZeroGravityActive;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
    }

    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelecity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void ApplyMovement()
    {
        _characterController.Move(_direction * _moveSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * _gravityMultiplier * Time.deltaTime;
        }
        _direction.y = _velocity;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded()) return;

        _velocity += _jumpPower;
    }

    private bool IsGrounded() => _characterController.isGrounded;

    public void SetMoveSpeed(float newSpeedAdjustment)
    {
        _moveSpeed += newSpeedAdjustment;
    }


    void ActivateSpeedBoost(float duration, float increaseAmount)
    {
        if (!_isSpeedBoostActive)
        {
            _isSpeedBoostActive = true;
            _moveSpeed += increaseAmount;
            StartCoroutine(SpeedBoostCooldown(duration, increaseAmount));
        }
    } 

    IEnumerator SpeedBoostCooldown(float duration, float increaseAmount)
    {
        yield return new WaitForSeconds(duration);
        _isSpeedBoostActive = false;
        _moveSpeed -= increaseAmount;
    }

    void ActivateJumpBoost(float duration, float increaseAmount)
    {
        if (!_isJumpBoostActive)
        {
            _isJumpBoostActive = true;
            _jumpPower += increaseAmount;
            StartCoroutine(JumpBoostCooldown(duration, increaseAmount));
        }
    }

    IEnumerator JumpBoostCooldown(float duration, float increaseAmount)
    {
        yield return new WaitForSeconds(duration);
        _isJumpBoostActive = false;
        _jumpPower -= increaseAmount;
    }

    void ActivateSizeBoost(float duration, float increaseAmount)
    {
        PlayerController player = GetComponent<PlayerController>();
        if (!_isSizeBoostActive)
        {
            _isSizeBoostActive = true;
            player.transform.localScale *= increaseAmount;
            StartCoroutine(SizeBoostCooldown(duration, increaseAmount));
        }
    }

    IEnumerator SizeBoostCooldown(float duration, float increaseAmount)
    {
        PlayerController player = GetComponent<PlayerController>();
        yield return new WaitForSeconds(duration);
        _isSizeBoostActive = false;
        player.transform.localScale /= increaseAmount;
    }

    void ActivateZeroGravity(float duration, float increaseAmount)
    {
        if (!_isZeroGravityActive)
        {
            _isZeroGravityActive = true;
            _gravity += increaseAmount;
            StartCoroutine(ZeroGravityCooldown(duration, increaseAmount));
        }
    }

    IEnumerator ZeroGravityCooldown(float duration, float increaseAmount)
    {
        yield return new WaitForSeconds(duration);
        _isZeroGravityActive = false;
        _gravity -= increaseAmount;
    }

    public void ActivatePowerUp (int id, float duration, float increaseAmount, GameObject powerUp)
    {
        if (id == 0)
        {
            if (!_isSpeedBoostActive)
            {
                ActivateSpeedBoost(duration, increaseAmount);
                Destroy(powerUp);
            }
        }

        if (id == 1)
        {
            if (!_isJumpBoostActive)
            {
                ActivateJumpBoost(duration, increaseAmount);
                Destroy(powerUp);
            }
        }
        if (id == 2)
        {
            if (!_isSizeBoostActive)
            {
                ActivateSizeBoost(duration, increaseAmount);
                Destroy(powerUp);
            }
        }
        if (id == 3)
        {
            if (!_isZeroGravityActive)
            {
                ActivateZeroGravity(duration, increaseAmount);
                Destroy(powerUp);
            }
        }
    }
}
