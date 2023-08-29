using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float _movementSpeed = 5;

    [SerializeField]
    float _startingSpeed = 20;

    [SerializeField]
    float _jumpForce = 10;

    [SerializeField]
    float _airMultiplier = 0.5f;

    [SerializeField]
    float _runMultiplier = 2f;

    [SerializeField]
    AudioSource _footstepsAudio;

    [SerializeField]
    AudioClip[] _footstepsSFX;

    [SerializeField]
    Transform _toFollow;

    [SerializeField]
    Transform _groundRayFront;

    [SerializeField]
    Transform _groundRayBack;

    [SerializeField]
    float _playerHeight;

    [SerializeField]
    LayerMask _whatIsGround;

    [SerializeField]
    float _groundDrag;

    bool _isGrounded;
    bool _isJumping;

    RaycastHit _groundInfoFront;
    RaycastHit _groundInfoBack;

    Rigidbody _playerRb;
    Vector3 _moveDirection;

    float _movementX;
    float _movementY;
    float _normalY;

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _movementSpeed = _startingSpeed;
    }

    void Update()
    {
        Jump();

        Movement();

        Run();
    }

    private void FixedUpdate()
    {
        if (_isJumping)
        {
            _isJumping = false;

            _playerRb.velocity = new Vector3(_playerRb.velocity.x, 0f, _playerRb.velocity.z);
            _playerRb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }

        if (_isGrounded)
        {
            _playerRb.AddForce(_moveDirection.normalized * _movementSpeed, ForceMode.Force);
        }
        else
        {
            _playerRb.AddForce(
                _moveDirection.normalized * _movementSpeed * _airMultiplier,
                ForceMode.Force
            );
        }
    }

    void Jump()
    {
        Physics.Raycast(
            _groundRayBack.position,
            Vector3.down,
            out _groundInfoBack,
            _playerHeight * 0.5f + 0.5f,
            _whatIsGround
        );

        Physics.Raycast(
            _groundRayFront.position,
            Vector3.down,
            out _groundInfoFront,
            _playerHeight * 0.5f + 0.5f,
            _whatIsGround
        );

        _isGrounded = _groundInfoFront.transform != null || _groundInfoBack.transform != null;

        // _isGrounded = (
        //     Physics.Raycast(
        //         _groundRayFront.position,
        //         Vector3.down,
        //         out _groundInfoFront,
        //         _playerHeight * 0.5f + 0.5f,
        //         _whatIsGround
        //     )
        //     || Physics.Raycast(
        //         _groundRayBack.position,
        //         Vector3.down,
        //         out _groundInfoBack,
        //         _playerHeight * 0.5f + 0.5f,
        //         _whatIsGround
        //     )
        // );

        // Debug.DrawRay(
        //     _groundRayFront.position,
        //     Vector3.down * (_playerHeight * 0.5f + 0.5f),
        //     Color.green,
        //     2,
        //     false
        // );
        // Debug.DrawRay(
        //     _groundRayBack.position,
        //     Vector3.down * (_playerHeight * 0.5f + 0.5f),
        //     Color.green,
        //     2,
        //     false
        // );

        if (_isGrounded)
        {
            _playerRb.drag = _groundDrag;
        }
        else
        {
            _playerRb.drag = 0;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isJumping = true;
        }
    }

    void Movement()
    {
        _movementX = Input.GetAxisRaw("Horizontal");
        _movementY = Input.GetAxisRaw("Vertical");

        if (_movementX != 0 || _movementY != 0)
        {
            _footstepsAudio.enabled = true;
            if (!_footstepsAudio.isPlaying)
            {
                _footstepsAudio.clip = _footstepsSFX[
                    UnityEngine.Random.Range(0, _footstepsSFX.Length)
                ];
                _footstepsAudio.Play();
            }

            if (
                (
                    (_groundInfoFront.normal.y > 0.9f || _groundInfoFront.normal.y < 0.1f)
                    && (_groundInfoBack.normal.y > 0.9f || _groundInfoBack.normal.y < 0.1f)
                ) || !_isGrounded
            )
            {
                _normalY = 0;
            }
            else if (_groundInfoFront.transform != null || _groundInfoBack.transform != null)
            {
                if (
                    _groundInfoFront.transform == null
                    || (
                        _groundInfoBack.transform != null
                        && _groundInfoFront.transform.position.y
                            < _groundInfoBack.transform.position.y
                    )
                )
                {
                    // if (_groundInfoFront.transform != null)
                    // {
                    //     _normalY = -(_groundInfoFront.normal.y);
                    // }
                    // else
                    // {
                    _normalY = -(_groundInfoBack.normal.y);
                    // }
                }
                else
                {
                    // if (_groundInfoBack.transform != null)
                    // {
                    //     _normalY = _groundInfoBack.normal.y;
                    // }
                    // else
                    // {
                    _normalY = _groundInfoFront.normal.y;
                    // }
                }
            }
        }
        else
        {
            _footstepsAudio.enabled = false;
            _normalY = 0;
        }

        _moveDirection = _toFollow.forward * _movementY + _toFollow.right * _movementX;
        _moveDirection = new Vector3(_moveDirection.x, _normalY, _moveDirection.z);

        // print(_moveDirection);
        // print(_groundInfoFront.normal.y);
        // print(_groundInfoBack.normal.y);
    }

    void Run()
    {
        if (Input.GetButtonDown("Run"))
        {
            _movementSpeed *= _runMultiplier;
            _footstepsAudio.pitch *= 1.3f;
        }
        else if (Input.GetButtonUp("Run"))
        {
            _movementSpeed = _startingSpeed;
            _footstepsAudio.pitch /= 1.3f;
        }
    }

    // void SpeedControl()
    // {
    //     Vector3 flatVelocity = new Vector3(_playerRb.velocity.x, 0, _playerRb.velocity.z);
    //     print(flatVelocity.magnitude);
    //     if (flatVelocity.magnitude > _maxSpeed)
    //     {
    //         Vector3 limitedVelocity = flatVelocity.normalized * _maxSpeed;
    //         _playerRb.velocity = new Vector3(
    //             limitedVelocity.x,
    //             _playerRb.velocity.y,
    //             limitedVelocity.z
    //         );
    //     }
    // }

    // private void OnCollisionStay(Collision other)
    // {
    //     if (other.gameObject.tag == "Ground")
    //     {
    //         _isGrounded = true;
    //     }
    // }

    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.tag == "Ground")
    //     {
    //         _isGrounded = false;
    //     }
    // }
}
