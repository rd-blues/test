using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;


public class Rotameter : MonoBehaviour
{
    public string _name;
    [Header("Settings")]
    [SerializeField] private GameObject floater;
    [SerializeField] private float defaultPositionY;
    [SerializeField] private float maxPositionY;
    [SerializeField] private float maxPositionPrecent;
    [SerializeField] private bool debug = false;
    [Header("Floater Spring")]
    [SerializeField] private float _easeBaseSpeed = 1.5F; //initial acceleration

    private AudioSource airbrushSource;
    [Header("Audio")]
    [SerializeField] private float maxVolume = 0.2f;


    //
    float lastPosition = 0.0f;
    float newPosition = 0.0f;

    private void Start()
    {
        airbrushSource = GetComponent<AudioSource>();
        defaultPositionY = floater.transform.localPosition.y;
        lastPosition = defaultPositionY;
    }
    private void Update()
    {
        airbrushSource.volume = CyclonGlobalData.valve1Angle.MapFloat(0, 90, 0, maxVolume) * Mathf.Clamp(CyclonGlobalData.currentPressure.MapFloat(0, 5, 0, 1), 0, 1);

        if (CyclonGlobalData.currentPressure >= 6)
        {
            newPosition = Extension.MapFloat(CyclonGlobalData.valve1Angle, 0, 90, defaultPositionY, maxPositionY);
            // newPosition = Mathf.Clamp(newPosition, defaultPositionY, Extension.MapFloat(maxPositionPrecent, 0, 100, defaultPositionY, maxPositionY));

            floater.transform.localPosition = new Vector3(floater.transform.localPosition.x, newPosition, floater.transform.localPosition.z);

            lastPosition = newPosition;
        }
        else
        {
            newPosition = CyclonGlobalData.currentPressure.MapFloat(0, 6, defaultPositionY, Extension.MapFloat(CyclonGlobalData.valve1Angle, 0, 90, defaultPositionY, maxPositionY));
            // newPosition = Mathf.Clamp(newPosition, defaultPositionY, Extension.MapFloat(maxPositionPrecent, 0, 100, defaultPositionY, maxPositionY));

            floater.transform.localPosition = new Vector3(floater.transform.localPosition.x, newPosition, floater.transform.localPosition.z);

            lastPosition = newPosition;
        }
        CyclonGlobalData.rotameterPosition = Extension.MapFloat(newPosition, defaultPositionY, maxPositionY, 0, 100);
        CyclonGlobalData.gasFlow = Extension.MapFloat(CyclonGlobalData.rotameterPosition, 10, 60, 1.0f, 2.5f);
    }
    void OnGUI()
    {
        if (CyclonGlobalData.debugMode)
        {

            // Set the label position
            Vector3 labelPosition = Camera.main.WorldToScreenPoint(floater.transform.position);
            labelPosition.y = Screen.height - labelPosition.y;

            // Display the label
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();
            style.fontSize = h * 2 / 100;
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(labelPosition.x, labelPosition.y, 200, 20), String.Format("'{0}': {1:0}%", _name, CyclonGlobalData.rotameterPosition), style);
        }
    }

}