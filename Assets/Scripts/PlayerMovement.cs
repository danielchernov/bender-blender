using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float _movementSpeed = 5;

    [SerializeField]
    float _jumpForce = 10;

    Rigidbody _playerRb;

    bool _isGrounded;

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();

        Run();

        Jump();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            //transform.Translate(Vector3.up * jumpForce);
            _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    void Movement()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(movementX, 0, movementY);

        transform.Translate(direction * _movementSpeed * Time.deltaTime);
    }

    void Run()
    {
        if (Input.GetButtonDown("Run"))
        {
            _movementSpeed *= 3;
        }
        else if (Input.GetButtonUp("Run"))
        {
            _movementSpeed /= 3;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = false;
        }
    }
}
