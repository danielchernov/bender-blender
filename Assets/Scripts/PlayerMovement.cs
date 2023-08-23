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
    float _playerHeight;

    [SerializeField]
    LayerMask _whatIsGround;

    [SerializeField]
    float _groundDrag;

    bool _isGrounded;
    bool _isJumping;

    Rigidbody _playerRb;
    Vector3 _moveDirection;

    float _movementX;
    float _movementY;

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _movementSpeed = _startingSpeed;
    }

    void Update()
    {
        //print("Grounded:" + _isGrounded);
        //print(_playerRb.velocity.magnitude);
        //        print(_movementSpeed);

        Movement();

        // SpeedControl();

        Run();

        Jump();
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
        _isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            _playerHeight * 0.5f + 0.5f,
            _whatIsGround
        );

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
        }
        else
        {
            _footstepsAudio.enabled = false;
        }

        _moveDirection = _toFollow.forward * _movementY + _toFollow.right * _movementX;
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
