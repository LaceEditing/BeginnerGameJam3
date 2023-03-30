using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LoadScreenHints : MonoBehaviour
{
    public String[] listOfHints;
    [SerializeField] TextMeshProUGUI TextObject;
    public void Start()
    {
        int rand = UnityEngine.Random.Range(0, listOfHints.Length);
        TextObject.text = listOfHints[rand];
    }
}
