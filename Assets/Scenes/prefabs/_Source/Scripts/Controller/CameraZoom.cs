using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public PlayerController playerController;
    [SerializeField] private int zoom = 10;
    [SerializeField] private int normal = 60;
    [SerializeField] private float smooth = 5;
    [Header("Zoom sensitivity settings")]
    public float sensivityZoomed = 3.0f;
    public float sensivityDefault = 5.0f;



    private bool isZoomed = false;


    private void Update()
    {
        if (Input.GetKeyDown(SettingsKey.KeyZoom))
        {
            isZoomed = !isZoomed;
        }

        if (isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
            playerController.MouseSensetivity = sensivityZoomed;

        }
        else
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
            playerController.MouseSensetivity = sensivityDefault;


        }
    }

}