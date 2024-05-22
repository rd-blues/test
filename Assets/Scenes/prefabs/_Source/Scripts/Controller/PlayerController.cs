using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    public enum State
    {
        Move,
        Stop
    }
    public State state;
    public static PlayerController instance;

    //Camera_Contoroll
    GameObject cam;
    CharacterController controller;
    float LookUp = 0.0f;
    float LookAside = 0.0f;
    float MovementForward = 0.0f;
    float MovementAside = 0.0f;

    [Header("Control Settings")]
    public float MovementSpeed = 3.0F;
    public float MouseSensetivity = 5.0f;
    //public float MouseSensetivityY = 5.0f;
    public float MaxCamAngle = 60.0f;
    public float MinCamAngle = -60.0f;

    //Othr_Variabels    
    public Texture2D Tex2D_Aim;
    [Range(0.01f, 1000.0f)]
    public float Tex2D_Aim_Scale = 1.0f;

    void Start()
    {
        // Application.targetFrameRate = 30;
        // Singelton
        instance = this;

        //Controller
        controller = GetComponent<CharacterController>();
        cam = transform.GetChild(0).gameObject;
        //state = State.Move;
    }

    State prevStaet;
    void Update()
    {
        if (!PauseContorl.isPaused)
        {
            if (Input.GetKeyDown(SettingsKey.KeyStopController))
            {
                state = state == State.Stop ? State.Move : State.Stop;
            }
        }
        // if (Input.GetKey(KeyCode.LeftControl))
        //     CyclonGlobalData.debugMode = true;
        // else
        //     CyclonGlobalData.debugMode = false;


        switch (state)
        {
            case State.Move: Move(); break;
            case State.Stop: Stop(); break;
        }

        //Ray mouse_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Ray aim_ray = new Ray(cam.transform.position, cam.transform.forward);
        //RaycastHit hit;

        //if (!EventSystem.current.IsPointerOverGameObject()) // For block UI raycast
        //{
        //    if ((Physics.Raycast(aim_ray, out hit, 100) || Physics.Raycast(mouse_ray, out hit, 100)))
        //    {
        //        ToogleOutLine();
        //    }
        //}
    }


    void Move()
    {
        if (Input.GetKeyDown(SettingsKey.KeySeat))
        {
            Seat();
        }

        MovementForward = Input.GetAxis("Vertical") * MovementSpeed;
        MovementAside = Input.GetAxis("Horizontal") * MovementSpeed;
        LookAside += Input.GetAxis("Mouse X") * MouseSensetivity;
        LookUp += Input.GetAxis("Mouse Y") * MouseSensetivity;
        LookUp = Mathf.Clamp(LookUp, MinCamAngle, MaxCamAngle);

        // Cursor settings
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Camera Rotation
        transform.rotation = Quaternion.Euler(0f, LookAside, 0f);
        cam.transform.localRotation = Quaternion.Euler(-LookUp, 0f, 0f);


        // transform.Rotate(0, LookAside, 0);
        // cam.transform.localRotation *= Quaternion.AngleAxis(LookUp, Vector3.left);
        // cam.transform.localRotation = ClampRotationAroundXAxis(cam.transform.localRotation);

        // Move 
        Vector3 vector = transform.TransformDirection((MovementForward * Vector3.forward + MovementAside * Vector3.right));
        controller.SimpleMove(vector);

        // Clapm rotation
        // Quaternion ClampRotationAroundXAxis(Quaternion q)
        // {
        //     q.x /= q.w;
        //     q.y /= q.w;
        //     q.z /= q.w;
        //     q.w = 1.0f;

        //     float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        //     angleX = Mathf.Clamp(angleX, MinCamAngle, MaxCamAngle);

        //     q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        //     return q;
        // }
    }

    void Stop()
    {
        // Cursor settings
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Camera Rotation
        cam.transform.localRotation = cam.transform.localRotation;
    }
    bool isSeat = false;
    void Seat()
    {
        if (!controller.isGrounded)
            return;

        if (!isSeat)
        {
            controller.height = 0.9f; // Seat 
        }
        else
        {
            controller.height = 1.77f; // Up
        }

        isSeat = !isSeat;
    }

    //tern_On_off_OutlineEffect
    cakeslice.Outline outlineObject;

    void ToogleOutLine()
    {
        //Mouse_Ray
        //Ray mouse_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Ray aim_ray = new Ray(cam.transform.position, cam.transform.forward);
        //RaycastHit hit;

        //if(Physics.Raycast(mouse_ray, out hit, 100))
        //{

        //}


        //DrawLine
        //Debug.DrawLine(mouse_ray.origin, mouse_ray.direction * 1300, Color.red);

        //if (!Cursor.visible)
        //{
        //    if (Physics.Raycast(aim_ray, out hit, 100))
        //    {
        //        if (hit.collider.gameObject.GetComponent<cakeslice.Outline>() != null)
        //        {
        //            if (outlineObject != null)
        //            {
        //                outlineObject.enabled = false;
        //                outlineObject = hit.collider.GetComponent<cakeslice.Outline>();
        //                outlineObject.enabled = true;
        //            }
        //            else
        //            {
        //                outlineObject = hit.collider.GetComponent<cakeslice.Outline>();
        //                outlineObject.enabled = true;
        //            }

        //        }
        //        else
        //        {
        //            if (outlineObject != null)
        //            {
        //                outlineObject.enabled = false;
        //                outlineObject = null;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (outlineObject != null)
        //        {
        //            outlineObject.enabled = false;
        //            outlineObject = null;
        //        }
        //    }
        //}

        //if (Cursor.visible)
        //{
        //    if (Physics.Raycast(mouse_ray, out hit, 100))
        //    {

        //        if (hit.collider.gameObject.GetComponent<cakeslice.Outline>() != null)
        //        {
        //            if (outlineObject != null)
        //            {
        //                outlineObject.enabled = false;
        //                outlineObject = hit.collider.GetComponent<cakeslice.Outline>();
        //                outlineObject.enabled = true;
        //            }
        //            else
        //            {
        //                outlineObject = hit.collider.GetComponent<cakeslice.Outline>();
        //                outlineObject.enabled = true;
        //            }
        //        }
        //        else
        //        {
        //            if (outlineObject != null)
        //            {
        //                outlineObject.enabled = false;
        //                outlineObject = null;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (outlineObject != null)
        //        {
        //            outlineObject.enabled = false;
        //            outlineObject = null;
        //        }
        //    }
        //}
    }

    // FPS
    float deltaTime = 0.0f;
    private void OnGUI()
    {
        if (!Cursor.visible)
        {
            Vector2 Tex2D_Aim_offset = new Vector2(Tex2D_Aim.width / Tex2D_Aim_Scale, Tex2D_Aim.height / Tex2D_Aim_Scale);
            GUI.DrawTexture(new Rect((Screen.width - Tex2D_Aim_offset.y) / 2, (Screen.height - Tex2D_Aim_offset.x) / 2, Tex2D_Aim_offset.y, Tex2D_Aim_offset.x), Tex2D_Aim);
        }


#if UNITY_EDITOR
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
#endif
    }
}
