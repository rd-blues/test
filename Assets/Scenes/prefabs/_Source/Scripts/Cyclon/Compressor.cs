using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public class Compressor : MonoBehaviour
{
    public enum State
    {
        stop, //выключеное состояние
        descend, //спускание, пи достижении максимального давления
        pumping //накачивание
    }
    public State state;
    public static Compressor instance;
    [SerializeField] private float currentPressure = 0.0f;
    [Header("Compressor Settings")]
    [SerializeField] private string _name;
    [SerializeField] private float minPressure = 0.0f;
    [SerializeField] private float maxPressure = 16.0f;
    [SerializeField] private float maxPressureShutdown = 15.0f;
    [SerializeField] private float minPressureResume = 5.0f;
    public float factorPumping = 1f;
    public float factorDescent = 0.1f;
    [Header("Audio")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float audioVolume = 1.0f;
    private AudioSource audioCompressor;
    private AudioSource audioButton;
    [SerializeField] private AudioClip workAudio;
    [SerializeField] private AudioClip playStopAudio;
    [Header("Button Settings")]
    [SerializeField] private GameObject button;
    [SerializeField] private bool buttonState = false;
    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Vector3 movedPosition;

    [Header("Ray settings")]
    [SerializeField] private float rayLength = 10.0f;
    private string[] currentState = { "Выключен", "Накачан", "Накачивание" };

    private void Start()
    {
        audioCompressor = GetComponent<AudioSource>();
        audioButton = button.GetComponent<AudioSource>();

        audioCompressor.clip = workAudio;

        instance = this;
        defaultPosition = button.transform.localPosition;
    }
    private void Update()
    {
        currentPressure = CyclonGlobalData.currentPressure;
        audioCompressor.volume = audioVolume;
        audioButton.volume = audioVolume;

        if (Extension.RayCastChek(button, rayLength))
        {
            button.GetComponent<Outline>().enabled = true;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                state = state == State.stop ? State.pumping : State.stop;
            }
        }
        else
        {
            button.GetComponent<Outline>().enabled = false;
        }
        MoveButton((int)state);
        switch (state)
        {
            case State.stop: Stop(); break;
            case State.pumping: Pumping(); break;
            case State.descend: Descend(); break;
        }
    }
    public void Pumping()
    {
        if (CyclonGlobalData.currentPressure <= maxPressureShutdown)
        {
            CyclonGlobalData.currentPressure += Time.deltaTime * factorPumping;
            if (!audioCompressor.isPlaying)
            {
                audioCompressor.Play();
                audioButton.PlayOneShot(playStopAudio);
            }
        }
        else
        {
            audioButton.PlayOneShot(playStopAudio);
            state = State.descend;
        }
    }
    public void Stop()
    {
        if (CyclonGlobalData.currentPressure >= minPressure)
        {
            CyclonGlobalData.currentPressure -= Time.deltaTime * factorDescent;
            if (audioCompressor.isPlaying)
            {
                audioCompressor.Stop();
                audioButton.PlayOneShot(playStopAudio);
            }
        }
    }
    public void Descend()
    {
        if (CyclonGlobalData.currentPressure >= minPressureResume)
        {
            CyclonGlobalData.currentPressure -= Time.deltaTime * factorDescent;
            if (audioCompressor.isPlaying)
            {
                audioCompressor.Stop();
                audioButton.PlayOneShot(playStopAudio);
            }
        }

        else
        {
            state = State.pumping;
            audioButton.PlayOneShot(playStopAudio);
        }
    }
    public void MoveButton(int state)
    {
        switch (state)
        {
            case 0:
                button.transform.localPosition = new Vector3(defaultPosition.x, defaultPosition.y, defaultPosition.z);
                break;
            case 2:
                button.transform.localPosition = new Vector3(movedPosition.x, movedPosition.y, movedPosition.z);
                break;
            default:
                break;
        }
    }

    void OnGUI()
    {
        if (CyclonGlobalData.debugMode)
        {
            // Get the name of the object and the rotation angle

            // Set the label position
            Vector3 labelPosition = Camera.main.WorldToScreenPoint(button.transform.position);
            labelPosition.y = Screen.height - labelPosition.y;

            // Display the label
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();
            style.fontSize = h * 2 / 100;
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(labelPosition.x, labelPosition.y, 200, 20), String.Format("'{0}': {1}", _name, currentState[(int)state]), style);
        }
    }
}
