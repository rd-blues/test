using UnityEngine;
using ExtensionMethods;
using System.Diagnostics.Tracing;
using System;

public class Sand : MonoBehaviour
{
    [Header("Bones")]
    [SerializeField] private GameObject sandClearBone;
    [SerializeField] private GameObject sandSolidBone;

    [Header("Bone positions")]
    [SerializeField] private float sandDefaultPosition;
    [SerializeField] private float sandClearBonePosition;
    private float sandClearBonePositionLast;
    [SerializeField] private float sandSolidBonePosition;
    private float sandSolidBonePositionLast;
    [SerializeField] private float lerpSpeed;

    [Header("Parameters")]
    [SerializeField] private float defaultYPosition;
    [SerializeField] private float maxYPosition;
    [SerializeField] private float scaleBarMaxValue;
    [SerializeField] private float precent;
    private enum DataType
    {
        Map,
        Curve
    }
    [SerializeField] private DataType dataType = DataType.Map;
    [SerializeField] private AnimationCurve sandClearCurve;
    [SerializeField] private AnimationCurve sandSolidCurve;
    // [SerializeField] private float minYPosition;

    private void Awake()
    {
        // minYPosition = sandClearBone.transform.localPosition.y;
    }
    private void Update()
    {
        if (dataType == DataType.Map)
        {
            sandClearBonePosition = Extension.MapFloat(CyclonGlobalData.rotameterPosition, 35f, 89f, 90f, 155f);
            sandClearBonePosition = Mathf.Clamp(sandClearBonePosition, sandDefaultPosition, 280f);
            sandSolidBonePosition = sandDefaultPosition - sandClearBonePosition * (precent / 100f);
        }
        else
        {
            sandClearBonePosition = Mathf.Lerp(sandClearBonePositionLast, sandClearCurve.Evaluate(CyclonGlobalData.rotameterPosition), lerpSpeed);
            sandSolidBonePosition = Mathf.Lerp(sandSolidBonePositionLast, sandSolidCurve.Evaluate(CyclonGlobalData.rotameterPosition), lerpSpeed);

            sandClearBonePositionLast = sandClearBonePosition;
            sandSolidBonePositionLast = sandSolidBonePosition;
        }

        sandClearBone.transform.localPosition = new Vector3(sandClearBone.transform.localPosition.x, Extension.MapFloat(sandClearBonePosition, 0f, 280f, 0f, maxYPosition), sandClearBone.transform.localPosition.z);
        sandSolidBone.transform.localPosition = new Vector3(sandSolidBone.transform.localPosition.x, Extension.MapFloat(sandSolidBonePosition, 0f, 280f, 0f, maxYPosition), sandSolidBone.transform.localPosition.z);
    }
}