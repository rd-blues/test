using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
public class Monometer : MonoBehaviour
{
    [SerializeField] private GameObject leftPointer;
    [SerializeField] private GameObject rightPointer;
    [SerializeField] private float zeroPositionLeft;
    [SerializeField] private float zeroPositionRight;
    [SerializeField] private float minPositionRight;
    [SerializeField] private float maxPositionLeft;
    [SerializeField] private float maxPrecent;
    [SerializeField] private float maxPositionPrecent;
    [SerializeField] private float _easeBaseSpeed = 1.5F;
    private enum DataType
    {
        Map,
        Curve
    }
    [SerializeField] private DataType _dataType = DataType.Map;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float pressureLeftRight = 0.0f;
    float lastPositionLeft = 0.0f;
    float lastPositionRight = 0.0f;
    float newPositionLeft = 0.0f;
    float newPositionRight = 0.0f;
    private void Start()
    {
        zeroPositionLeft = leftPointer.transform.localPosition.y;
        zeroPositionRight = rightPointer.transform.localPosition.y;
        minPositionRight = zeroPositionLeft - (maxPositionLeft - zeroPositionLeft);
    }

    private void Update()
    {
        // if (CyclonGlobalData.tankValue == 0)
        // {
        //     pressureLeftRight = Extension.MapFloat(CyclonGlobalData.rotameterPosition, 5, 35, 2, 11) / 2;
        // }
        // else
        // {
        if (CyclonGlobalData.valve2State)
        {
            pressureLeftRight = Extension.MapFloat(CyclonGlobalData.rotameterPosition, 5f, 35f, 1.5f, 9f) / 2;
        }
        else
        {
            if (_dataType == DataType.Map)
            {
                pressureLeftRight = Extension.MapFloat(CyclonGlobalData.rotameterPosition, 35f, 89f, 190f, 312f) / 2f;

            }
            else
            {
                pressureLeftRight = _curve.Evaluate(CyclonGlobalData.rotameterPosition) / 2f;
            }
        }
        // }




        if (CyclonGlobalData.currentPressure >= 6)
        {
            newPositionLeft = Extension.MapFloat(pressureLeftRight, 0, 260, zeroPositionLeft, maxPositionLeft);
            newPositionRight = Extension.MapFloat(pressureLeftRight, 0, 260, zeroPositionRight, minPositionRight);

            // newPositionLeft = Mathf.Clamp(newPositionLeft, zeroPositionLeft, Extension.MapFloat(maxPositionPrecent, 0, maxPrecent, zeroPositionLeft, maxPositionLeft));
            // newPositionRight = Mathf.Clamp(newPositionRight, Extension.MapFloat(maxPositionPrecent, 0, maxPrecent, zeroPositionRight, minPositionRight), zeroPositionRight);

            newPositionLeft = Mathf.Lerp(lastPositionLeft, newPositionLeft, _easeBaseSpeed);
            newPositionRight = Mathf.Lerp(lastPositionRight, newPositionRight, _easeBaseSpeed);

            leftPointer.transform.localPosition = new Vector3(leftPointer.transform.localPosition.x, newPositionLeft, leftPointer.transform.localPosition.z);
            rightPointer.transform.localPosition = new Vector3(rightPointer.transform.localPosition.x, newPositionRight, rightPointer.transform.localPosition.z);

            lastPositionLeft = newPositionLeft;
            lastPositionRight = newPositionRight;
        }
        else
        {
            newPositionLeft = CyclonGlobalData.currentPressure.MapFloat(0, 6, zeroPositionLeft, Extension.MapFloat(pressureLeftRight, 0, 260, zeroPositionLeft, maxPositionLeft));
            newPositionRight = CyclonGlobalData.currentPressure.MapFloat(0, 6, zeroPositionRight, Extension.MapFloat(pressureLeftRight, 0, 260, zeroPositionRight, minPositionRight));

            // newPositionLeft = Mathf.Clamp(newPositionLeft, zeroPositionLeft, Extension.MapFloat(maxPositionPrecent, 0, maxPrecent, zeroPositionLeft, maxPositionLeft));
            // newPositionRight = Mathf.Clamp(newPositionRight, Extension.MapFloat(maxPositionPrecent, 0, maxPrecent, zeroPositionRight, minPositionRight), zeroPositionRight);

            newPositionLeft = Mathf.Lerp(lastPositionLeft, newPositionLeft, _easeBaseSpeed);
            newPositionRight = Mathf.Lerp(lastPositionRight, newPositionRight, _easeBaseSpeed);

            leftPointer.transform.localPosition = new Vector3(leftPointer.transform.localPosition.x, newPositionLeft, leftPointer.transform.localPosition.z);
            rightPointer.transform.localPosition = new Vector3(rightPointer.transform.localPosition.x, newPositionRight, rightPointer.transform.localPosition.z);

            lastPositionLeft = newPositionLeft;
            lastPositionRight = newPositionRight;
        }
    }
    void OnGUI()
    {
        if (CyclonGlobalData.debugMode)
        {

            // Set the label position
            Vector3 labelPositionLeft = Camera.main.WorldToScreenPoint(leftPointer.transform.position);
            Vector3 labelPositionRight = Camera.main.WorldToScreenPoint(rightPointer.transform.position);

            labelPositionLeft.y = Screen.height - labelPositionLeft.y;
            labelPositionRight.y = Screen.height - labelPositionRight.y;

            // Display the label
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();
            style.fontSize = h * 2 / 100;
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(labelPositionLeft.x, labelPositionLeft.y, 200, 20), String.Format("{0:0.0}mm", Extension.MapFloat(newPositionLeft, zeroPositionLeft, maxPositionLeft, 0, 50)), style);
            GUI.Label(new Rect(labelPositionRight.x, labelPositionRight.y, 200, 20), String.Format("{0:0.0}mm", Extension.MapFloat(newPositionRight, zeroPositionRight, minPositionRight, 0, 50)), style);
        }
    }
}