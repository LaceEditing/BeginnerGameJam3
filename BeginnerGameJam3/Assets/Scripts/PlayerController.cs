using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Player Select")]
    [SerializeField] bool _isPlayer;
    [SerializeField] bool _isEnemy;

    [Header("Default Settings")]
    [SerializeField] float _moveSpeed = 10.0f;
    [SerializeField] bool _isGrounded = true;
    [SerializeField] float _jumpheight = 300.0f;
    [SerializeField] GameObject _playerHealthBar;
    [SerializeField] GameObject _enemyHealthBar;
    [SerializeField] Animator _anim;
    float _horizontalInput;
    Rigidbody _rb;


    [Header("Damage System")]

    [Range(0, 1)] public float _totalHealth = 1.0f;
    [Range(0, 1)] public float _totalEnemyHealth = 1.0f;
    bool _isCoroutineRunning = false;
    


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerHealthBar = GameObject.Find("Player_One_Health");
        _enemyHealthBar = GameObject.Find("Player_Two_Health");
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
        

        Image _playerHealth = _playerHealthBar.GetComponent<Image>();
        _playerHealth.fillAmount = _totalHealth;

        Image _enemyHealth = _enemyHealthBar.GetComponent<Image>();
        _enemyHealth.fillAmount = _totalEnemyHealth;
        

        if (_rb.velocity.magnitude > 0)
            _anim.SetBool("isWalking", true);
        else _anim.SetBool("isWalking", false);

        if(Input.GetMouseButtonDown(0) && !_isCoroutineRunning)
        {
            _anim.SetBool("isPunching", true);
            StartCoroutine(ResetPunchAnimationAfterDelay(0.9f));
        }  

        if(_isGrounded == false)
            _anim.SetBool("isJumping", true);
        else if (_isGrounded == true)
            _anim.SetBool("isJumping", false);
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

    IEnumerator ResetPunchAnimationAfterDelay(float delay)
    {
        _isCoroutineRunning = true;
        yield return new WaitForSeconds(delay);
        _anim.SetBool("isPunching", false);
        _isCoroutineRunning = false;
    }

}
