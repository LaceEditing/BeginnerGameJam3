using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerTopDown : MonoBehaviour
{

    [SerializeField] Vector3 _cameraOffset = new Vector3(0.0f, 3.0f, -4.0f);
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _enemy;


    void Start()
    {
        _player = GameObject.Find("Player");
        _enemy = GameObject.Find("Enemy");
    }



    void Update()
    {
        transform.position = _player.transform.position + _cameraOffset;
    }
}
