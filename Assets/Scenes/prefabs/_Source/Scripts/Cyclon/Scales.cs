using UnityEngine;
using System;
using System.Collections.Generic;
using ExtensionMethods;
using TMPro;


public class Scales : MonoBehaviour
{
    [SerializeField] private Vial holderObject;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject onOffButton;
    [SerializeField] private GameObject resetButton;
    [Header("Interface")]
    [SerializeField] private GameObject UI;
    [Space]
    [SerializeField] private float currentScale;
    [SerializeField] private float resetedScale;
    [SerializeField] private bool isEnabled = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Extension.RayCastChek(onOffButton, 10f))
            {
                isEnabled = isEnabled == false ? true : false;
                currentScale = 0.0f;
                resetedScale = 0.0f;
                text.gameObject.SetActive(isEnabled);
            }
            if (Extension.RayCastChek(resetButton, 10f))
            {
                resetedScale = currentScale;
            }
        }


        if (isEnabled)
        {
            holderObject = this.GetComponentInChildren<Vial>();
            if (holderObject != null)
            {
                UI.SetActive(true);
                currentScale = Mathf.Lerp(currentScale, holderObject.currentMass + CyclonGlobalData.vialValue, 0.25f);
            }
            else
            {
                UI.SetActive(false);
                currentScale = Mathf.Lerp(currentScale, 0, 0.5f); ;
            }
            text.GetComponent<TextMeshPro>().text = String.Format("{0:0.000} g", currentScale - resetedScale);
        }
        else
        {
            UI.SetActive(false);
        }
    }

}