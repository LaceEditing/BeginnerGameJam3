using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{


    public PlayerController _playerController;
    public GameObject _otherPlayer;
    public GameObject characterObj;
    public GameObject _attackBox;
    public bool isBlocking;
    public bool isPunching;
    public bool isDead;
    Animator _anim;
    public GameObject _healthBar;
    public GameObject _staminaBar;

    [Header("Damage System")]

    [Range(0, 1)] public float _totalHealth = 1.0f;
    [Range(0, 1)] public float _totalStamina = 1.0f;

    public float lightAttackDamage;
    public float mediumAttackDamage;
    public float heavyAttackDamage;
    [Space]
    public float blockDamageOffset;




    // Start is called before the first frame update
    void Start()
    {
        _anim = characterObj.GetComponent<Animator>();
        isBlocking = false;
        _playerController = GetComponent<PlayerController>();
        _otherPlayer = GameObject.Find("Player2");
    }

    // Update is called once per frame
    void Update()
    {

        if(_playerController._isPlayer == true)
        {

            if(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
            {
                if (isPunching == false)
                {
                    Attack1();
                }
            }
        
            if(Input.GetButton("Fire2"))
            {
                isBlocking = true;
                _anim.SetBool("blocking", isBlocking);
            }

            if(Input.GetButtonUp("Fire2"))
            {
                isBlocking = false;
                _anim.SetBool("blocking", isBlocking);
            }
        }

        if (_totalHealth <= 0)
        {
            DeathSequence();
        }


        Image _playerHealth = _healthBar.GetComponent<Image>();
        _playerHealth.fillAmount = _totalHealth;
        Image _playerStamina = _staminaBar.GetComponent<Image>();
        _playerStamina.fillAmount = _totalStamina;

        //PURELY FOR DEBUG - PLEASE REMOVE//

        if (Input.GetKeyDown(KeyCode.F1))
        {
            LightAttack();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            MediumAttack();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            HeavyAttack();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            DeathSequence();
        }

    }


    [ContextMenu("Attack 1")]
    public void Attack1()
    {
        isBlocking = false;
        _anim.SetBool("blocking", isBlocking); 
        _anim.SetTrigger("punching");
        isPunching = true;
        _attackBox.SetActive(true);
        StartCoroutine(ActivateAttackBox());
    }

    IEnumerator ActivateAttackBox()
    {
        yield return new WaitForSeconds(0.3f);
        _attackBox.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        isPunching = false;
    }

    public void OnTriggerEnter(Collider other) 
    {
        if (other.name == "Player2")
        {
            _otherPlayer.GetComponent<CombatController>().LightAttack();
        }
    }

    public void SetBlocking()
    {
        if(isBlocking)
        {
            isBlocking = false;
        }
        else if (!isBlocking)
        {
            isBlocking = true;
        }

        _anim.SetBool("blocking", isBlocking);
    }

    public void LightAttack()
    {
        if (isBlocking)
        {
            _totalHealth = _totalHealth - (lightAttackDamage / blockDamageOffset);
        }
        else
        {
            _totalHealth -= lightAttackDamage;
            _anim.SetTrigger("hit_light");
        }
        
    }

    public void MediumAttack()
    {
        if (isBlocking)
        {
            _totalHealth = _totalHealth - (mediumAttackDamage / blockDamageOffset);
        }
        else
        {
            _totalHealth -= mediumAttackDamage;
            _anim.SetTrigger("hit_medium");
        }
    }

    public void HeavyAttack()
    {
        if (isBlocking)
        {
            _totalHealth = _totalHealth - (heavyAttackDamage / blockDamageOffset);
        }
        else
        {
            _totalHealth -= heavyAttackDamage;
            _anim.SetTrigger("hit_hard");
        }
    }

    public void DeathSequence()
    {
        isDead = true;
        _anim.SetTrigger("death");

    }

}
