using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateController : MonoBehaviour
{
   [SerializeField] Rigidbody _rb;
   [SerializeField] Animator _anim;
   bool _isCoroutineRunning = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_rb.velocity.magnitude > 0)
            _anim.SetBool("isWalking", true);
        else _anim.SetBool("isWalking", false);

        if(Input.GetMouseButtonDown(0) && !_isCoroutineRunning)
        {
            _anim.SetBool("isPunching", true);
            StartCoroutine(ResetPunchAnimationAfterDelay(0.4f));
        }        
    }

    IEnumerator ResetPunchAnimationAfterDelay(float delay)
    {
        _isCoroutineRunning = true;
        yield return new WaitForSeconds(delay);
        _anim.SetBool("isPunching", false);
        _isCoroutineRunning = false;
    }
}
