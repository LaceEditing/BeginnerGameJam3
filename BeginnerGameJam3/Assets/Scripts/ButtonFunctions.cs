using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public AudioSource mySounds;
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public float _scaleTransform;

    void Start()
    {
    }

    public void HoverSound()
    {
        mySounds.PlayOneShot(hoverSound);
        
        
    }

    public void ClickSound()
    {
        mySounds.PlayOneShot(clickSound);
    }
}
