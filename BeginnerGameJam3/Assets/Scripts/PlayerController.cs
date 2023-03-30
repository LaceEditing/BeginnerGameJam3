using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Player Select")]
    public bool _isPlayer;
    public bool _isEnemy;

    [Header("Default Settings")]
    [SerializeField] float _moveSpeed = 10.0f;
    [SerializeField] bool _isGrounded = true;
    [SerializeField] float _jumpheight = 300.0f;
    [SerializeField] Animator _anim;
    float _horizontalInput;
    Rigidbody _rb;
    


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (_isPlayer == true)
        {
            _horizontalInput = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.forward * _horizontalInput * _moveSpeed *Time.deltaTime);

            if (_isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _rb.AddForce(new Vector3(0, 1, 0) * _jumpheight);
                }
            }
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = false;
        }
    }
}


