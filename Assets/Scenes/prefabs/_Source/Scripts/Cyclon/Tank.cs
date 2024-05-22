using System;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public class Tank : MonoBehaviour
{
    [SerializeField] private Vial holderObject;
    [SerializeField] private string _name;
    [SerializeField] private bool isEmpty = true;
    [SerializeField] private List<GameObject> objects = new List<GameObject>();
    [SerializeField] private GameObject valve;
    [SerializeField] private string valveName;
    private string[] state = { "Закрыт", "Открыт" };
    [SerializeField] private bool valveIsOpen = false;
    [SerializeField] private GameObject output;
    [SerializeField] private Vial outputVial;
    private float lastTankValue = 0;
    private void Update()
    {
        holderObject = this.GetComponentInChildren<Vial>();
        outputVial = output.GetComponentInChildren<Vial>();
        if (outputVial == null && CyclonGlobalData.tankValue > 0)
        {
            valveIsOpen = false;
            CyclonGlobalData.valve2State = valveIsOpen;
        }
        if (!valveIsOpen) Stopwatch._enabled = false;

        if (Extension.RayCastChek(this.gameObject, 10))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ChangeValue(-100);
            }
        }
        if (Extension.RayCastChek(valve, 10))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                valveIsOpen = valveIsOpen == true ? false : true;
                CyclonGlobalData.valve2State = valveIsOpen;
            }
        }


        if (Extension.RayCastChek(valve, 10))
            valve.GetComponent<Outline>().enabled = true;
        else
            valve.GetComponent<Outline>().enabled = false;


        if (holderObject != null && !holderObject.isEmpty)
        {
            ChangeValue(CyclonGlobalData.vialValue);
        }

        SandValue();
        RotationValve();
        SandCleaning();
    }
    void OnGUI()
    {
        if (CyclonGlobalData.debugMode)
        {
            // Set the label position
            Vector3 labelPositionTank = Camera.main.WorldToScreenPoint(transform.position);
            labelPositionTank.y = Screen.height - labelPositionTank.y;

            Vector3 labelPositionValve = Camera.main.WorldToScreenPoint(valve.transform.position);
            labelPositionValve.y = Screen.height - labelPositionValve.y;

            // Display the label
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();
            style.fontSize = h * 2 / 100;
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(labelPositionTank.x, labelPositionTank.y, 200, 20), String.Format("'{0}': {1:0.00} g", _name, CyclonGlobalData.tankValue), style);
            GUI.Label(new Rect(labelPositionValve.x, labelPositionValve.y, 200, 20), String.Format("'{0}': {1}", valveName, state[Convert.ToInt32(valveIsOpen)]), style);

        }
    }
    public void ChangeValue(float value)
    {
        CyclonGlobalData.tankValue += value;
        CyclonGlobalData.vialValue = 0f;
    }
    private void SandValue()
    {
        CyclonGlobalData.tankValue = Mathf.Clamp(CyclonGlobalData.tankValue, 0, 100);
        lastTankValue = CyclonGlobalData.tankValue;

        int id = Extension.MapInt(Convert.ToInt32(CyclonGlobalData.tankValue), 0, 60, 0, objects.Count);
        id = Mathf.Clamp(id, 0, objects.Count - 1);
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
        if (CyclonGlobalData.tankValue > 0.0f)
        {
            isEmpty = false;
            objects[id].SetActive(true);
        }
        else
        {
            isEmpty = true;
        }
    }
    private void RotationValve()
    {
        if (valveIsOpen)
            valve.transform.localRotation = Quaternion.Euler(0, 0, 0);
        else
            valve.transform.localRotation = Quaternion.Euler(-90, 0, 0);
    }
    private void SandCleaning()
    {
        if (valveIsOpen && outputVial != null && !isEmpty && CyclonGlobalData.currentPressure > 6.0f)
        {
            Stopwatch._enabled = true;
            CyclonGlobalData.vialValue += Time.deltaTime * Mathf.Clamp(Extension.MapFloat(CyclonGlobalData.rotameterPosition, 0, 80, 0.69f, 0.025f), 0.025f, 0.69f);
            CyclonGlobalData.tankValue -= Time.deltaTime * Mathf.Clamp(Extension.MapFloat(CyclonGlobalData.rotameterPosition, 0, 80, 0.7f, 0.027f), 0.027f, 0.7f);

            if (CyclonGlobalData.tankValue <= 0)
            {
                isEmpty = true;
                Stopwatch._enabled = false;
            }
        }
    }
}