using UnityEngine;
using ExtensionMethods;

public class Gauge : MonoBehaviour
{
    [SerializeField] private float defaultRotation;
    [SerializeField] private float maxRotationAngle;
    [SerializeField] private float lerpTime = 1.0f;
    public float atmValue = 0.0f;
    private float lastPosition = 0.0f;
    private void Start()
    {
        defaultRotation = this.transform.localRotation.y;
    }
    private void Update()
    {
        atmValue = CyclonGlobalData.currentPressure;
        MoveGauge(atmValue);
    }
    public void MoveGauge(float atm)
    {
        atm = Mathf.Clamp(atm, 0, 16);
        atm = Mathf.Lerp(lastPosition, atm, lerpTime * Time.deltaTime);
        this.transform.localRotation = Quaternion.Euler(0f, Extension.MapFloat(atm * 1.013f, 0, 16, defaultRotation, maxRotationAngle), 0f);
        lastPosition = atm;

    }
}