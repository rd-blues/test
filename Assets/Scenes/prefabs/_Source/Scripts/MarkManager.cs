using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkManager : MonoBehaviour
{
    public static MarkManager instance;

    public float Mark { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        Mark = -1f;
    }

    public void SetMark(float value)
    {
        Mark = value;
    }

    private void OnApplicationQuit()
    {
        if(Mark >= 0)
        {
            System.IO.File.WriteAllText(System.Environment.CurrentDirectory + "//result.txt", Mark.ToString());
        }
    }
}
