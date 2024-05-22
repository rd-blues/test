using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public class Vial : MonoBehaviour
{
    [SerializeField] private string _name;
    public float currentMass;

    [SerializeField] private float minMass;
    [SerializeField] private float maxMass;
    public bool isEmpty = true;
    [SerializeField] private List<GameObject> objects = new List<GameObject>();
    private void Start()
    {
        currentMass = UnityEngine.Random.Range(minMass, maxMass);
    }
    private void Update()
    {
        if (Extension.RayCastChek(this.gameObject, 10.0f))
        {
            if (Input.GetKeyDown(KeyCode.R))
                ChangeValue(-100.0f);
            this.GetComponent<Outline>().enabled = true;
        }
        else
            this.GetComponent<Outline>().enabled = false;
        SandValue();
    }
    void OnGUI()
    {
        if (CyclonGlobalData.debugMode)
        {
            // Set the label position
            Vector3 labelPosition = Camera.main.WorldToScreenPoint(transform.position);
            labelPosition.y = Screen.height - labelPosition.y;

            // Display the label
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();
            style.fontSize = h * 2 / 100;
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(labelPosition.x, labelPosition.y, 200, 20), String.Format("'{0}': {1:0.00} g", _name, CyclonGlobalData.vialValue), style);
        }
    }
    public void ChangeValue(float value)
    {
        CyclonGlobalData.vialValue += value;

    }
    private void SandValue()
    {
        CyclonGlobalData.vialValue = Mathf.Clamp(CyclonGlobalData.vialValue, 0.0f, 100.0f);
        int id = Extension.MapInt(Convert.ToInt32(CyclonGlobalData.vialValue), 0, 100, 0, objects.Count);
        id = Mathf.Clamp(id, 0, objects.Count - 1);
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
        if (CyclonGlobalData.vialValue > 0.0f)
        {
            isEmpty = false;
            objects[id].SetActive(true);
        }
        else
        {
            isEmpty = true;
        }
        // currentMass += addedMass;
    }
}