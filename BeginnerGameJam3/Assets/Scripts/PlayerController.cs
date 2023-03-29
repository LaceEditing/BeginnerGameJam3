using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Player Select")]
    [SerializeField] bool _isPlayer;
    [SerializeField] bool _isEnemy;

    [Header("Settings")]
    [SerializeField] float _moveSpeed = 10.0f;
    [SerializeField] bool _isGrounded = true;
    [SerializeField] float _jumpheight = 300.0f;
    [SerializeField] GameObject _playerHealthBar;
    [SerializeField] GameObject _enemyHealthBar;
    float _horizontalInput;
    Rigidbody _rb;

    [Header("Damage System")]

    [Range(0, 1)] public float _totalHealth = 1.0f;
    [Range(0, 1)] public float _totalEnemyHealth = 1.0f;
    


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerHealthBar = GameObject.Find("Player_One_Health");
        _enemyHealthBar = GameObject.Find("Player_Two_Health");
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
        

        Image _playerHealth = _playerHealthBar.GetComponent<Image>();
        _playerHealth.fillAmount = _totalHealth;

        Image _enemyHealth = _enemyHealthBar.GetComponent<Image>();
        _enemyHealth.fillAmount = _totalEnemyHealth;

        
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


    

    public void GetHit()
    {

    }

}
