using UnityEngine;
using System;
using System.Collections.Generic;
using ExtensionMethods;

public class DragObject : MonoBehaviour
{
    private GameObject dragObjectPoint;

    [SerializeField] private List<ListObject> dragObjectsList = new List<ListObject>();
    [SerializeField] private GameObject objectInHand;
    [SerializeField] private Vector3 previousPosition;
    [SerializeField] private Quaternion previousRotation;
    [SerializeField] private GameObject prevparent;

    public static bool objIsTake = false;
    private int selectObjectID = 0;
    private void Start()
    {
        dragObjectPoint = this.gameObject;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2) && objIsTake)
        {
            ResetObject();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (objIsTake)
            {
                foreach (GameObject obj in dragObjectsList[selectObjectID].endPositions)
                {

                    if (Extension.RayCastChek(obj, 10f))
                    {
                        objectInHand.transform.position = obj.transform.Find("HolderPosition").position;
                        objectInHand.transform.rotation = obj.transform.Find("HolderPosition").rotation;
                        objectInHand.transform.SetParent(obj.transform);


                        objectInHand = null;
                        previousPosition = Vector3.zero;
                        prevparent = null;

                        objIsTake = false;
                    }
                }

            }
            else
            {
                for (int i = 0; i < dragObjectsList.Count; i++)
                    if (Extension.RayCastChek(dragObjectsList[i]._object, 10))
                    {
                        selectObjectID = i;
                        prevparent = dragObjectsList[i]._object.transform.parent.gameObject;
                        previousPosition = dragObjectsList[i]._object.transform.position;
                        previousRotation = dragObjectsList[i]._object.transform.rotation;

                        dragObjectsList[i]._object.transform.SetParent(this.gameObject.transform);
                        objectInHand = dragObjectsList[i]._object;
                        dragObjectsList[i]._object.transform.localPosition = this.gameObject.transform.localPosition;
                        dragObjectsList[i]._object.transform.localRotation = this.gameObject.transform.localRotation;

                        objIsTake = true;
                    }
            }
        }
    }
    private void ResetObject()
    {
        objectInHand.transform.position = previousPosition;
        objectInHand.transform.rotation = previousRotation;
        objectInHand.transform.parent = prevparent.gameObject.transform;
        objectInHand = null;
        previousPosition = Vector3.zero;
        prevparent = null;

        objIsTake = false;
    }
}
[Serializable]
class ListObject
{
    public GameObject _object;
    public GameObject[] endPositions;

}