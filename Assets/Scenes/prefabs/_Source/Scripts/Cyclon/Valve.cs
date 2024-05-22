using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
[RequireComponent(typeof(Outline))]
public class Valve : MonoBehaviour
{
    public string _name;
    private Compressor _compressor;
    [SerializeField] private float minRotationAngle = 0.0f;
    [SerializeField] private float maxRotationAngle = 90.0f;
    private Quaternion originalRotation;
    private bool isMouseOver = false;
    private bool isMouseDrag = false;
    [SerializeField] private bool reverseRotation = false;
    public float valveAngle = 0.0f;
    public float inpactOnPressure = 0.0f;

    private float factorPumping;
    private float factorDescent;



    private void Start()
    {
        _compressor = GameObject.FindObjectOfType<Compressor>();


        factorDescent = _compressor.factorDescent;
        factorPumping = _compressor.factorPumping;

        originalRotation = transform.localRotation;
        valveAngle = originalRotation.y;
    }

    private void Update()
    {

        if (isMouseDrag || isMouseOver)
            GetComponent<Outline>().enabled = true;
        else
            GetComponent<Outline>().enabled = false;

        if (isMouseDrag)
        {
            if (!reverseRotation)
                valveAngle += Input.GetAxis("Mouse X");
            else
                valveAngle -= Input.GetAxis("Mouse X");

            valveAngle = Mathf.Clamp(valveAngle, minRotationAngle, maxRotationAngle);
            int valveAngleX = Convert.ToInt32(valveAngle);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.localRotation = Quaternion.Euler(0, valveAngleX.MapInt(0, 90, 0, 9) * 10, 0);
                CyclonGlobalData.valve1Angle = Mathf.Clamp(valveAngleX.MapInt(0, 90, 0, 9) * 10, 0, 90);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, valveAngleX, 0);
                CyclonGlobalData.valve1Angle = Mathf.Clamp(valveAngle, 0, 90);
            }



            _compressor.factorDescent = factorDescent + valveAngle.MapFloat(0, 90, 0, inpactOnPressure * 0.35f);
            _compressor.factorPumping = factorPumping - valveAngle.MapFloat(0, 90, 0, inpactOnPressure);
        }
    }
    private void OnMouseDrag()
    {
        isMouseDrag = true;
        PlayerController.instance.state = PlayerController.State.Stop;
    }
    private void OnMouseUp()
    {
        isMouseDrag = false;
        PlayerController.instance.state = PlayerController.State.Move;
    }
    private void OnMouseEnter()
    {
        isMouseOver = true;
    }
    private void OnMouseExit()
    {
        isMouseOver = false;
    }
    void OnGUI()
    {
        if (CyclonGlobalData.debugMode)
        {
            // Get the name of the object and the rotation angle
            float rotationAngle = Quaternion.Angle(originalRotation, transform.localRotation);

            // Set the label position
            Vector3 labelPosition = Camera.main.WorldToScreenPoint(transform.position);
            labelPosition.y = Screen.height - labelPosition.y;

            // Display the label
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();
            style.fontSize = h * 2 / 100;
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(labelPosition.x, labelPosition.y, 200, 20), String.Format("'{0}': угол поворота: {1:0}°", _name, rotationAngle), style);
        }
    }
}
