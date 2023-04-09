using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
 
    public AudioSource _menuSounds;
    public AudioClip _hoverSound;
    public AudioClip _clickSound;

     public void Hoversound()
    {
        _menuSounds.PlayOneShot(_hoverSound);
    }

    public void ClickSound()
    {
        _menuSounds.PlayOneShot(_clickSound);
    }
}
