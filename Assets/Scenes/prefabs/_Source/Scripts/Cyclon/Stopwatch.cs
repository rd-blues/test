using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private TextMeshPro textDisplay;
    public static bool _enabled = false;
    public static float currentSeconds = 0.0f;

    private void Start()
    {

    }

    private void Update()
    {
        if (Extension.RayCastChek(this.gameObject, 10))
        {
            this.GetComponent<Outline>().enabled = true;

            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                currentSeconds = 0.0f;
                textDisplay.text = String.Format("{0:0.00} s", currentSeconds);
            }
        }
        else
            this.GetComponent<Outline>().enabled = false;


        if (_enabled)
        {
            currentSeconds += Time.deltaTime;
            textDisplay.text = String.Format("{0:0.00} s", currentSeconds);
        }
    }
}