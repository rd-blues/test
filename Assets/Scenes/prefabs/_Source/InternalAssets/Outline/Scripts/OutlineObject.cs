using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLRTK.Outline
{
    public class OutlineObject : MonoBehaviour
    {
        GameObject childOutline;
        bool isActive;
        public bool isActiveOnStart;
        public bool onAll = true;
        Material outlineMaterial;

        private void Start()
        {
            outlineMaterial = Resources.Load("M_Outline", typeof(Material)) as Material;

            CreateOutline();
            ToogleOutline(isActiveOnStart);
            isActive = isActiveOnStart;
        }

        void CreateOutline()
        {
            // Create 
            childOutline = GameObject.Instantiate(this.gameObject, this.transform, false);
            childOutline.transform.localScale = Vector3.one;
            childOutline.transform.localPosition = Vector3.zero;
            childOutline.transform.localEulerAngles = Vector3.zero;
            childOutline.name = this.gameObject.name + " Outlline";

            SetOutlineMaterial(childOutline);
        }

        void SetOutlineMaterial(GameObject obj)
        {
            //foreach (var com in obj.GetComponents<Component>())
            //{
            //    if (!(com is MeshRenderer || com is MeshFilter || com is Transform))
            //        Destroy(com);

            //}

            // Remove some settings from object
            Destroy(obj.GetComponent<OutlineObject>());
            Destroy(obj.GetComponent<Collider>());
            

            // Get all materials on object
            Material[] materials = obj.GetComponent<Renderer>().materials;

            // Set material on object
            for (int j = 0; j < materials.Length; j++)
            {
                materials[j] = outlineMaterial;
            }
            obj.GetComponent<Renderer>().materials = materials;

            // Do for all child or not
            if (onAll)
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    SetOutlineMaterial(obj.transform.GetChild(i).gameObject);

                    if (obj.transform.childCount == 0)
                        break;
                }
            }
            else
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    Destroy(obj.transform.GetChild(i).gameObject);
                }
            }
        }

        public void ToogleOutline(bool value)
        {
            childOutline.SetActive(value);
        }

        private void OnMouseEnter()
        {
            if (PauseContorl.isPaused)
                return;

            if (!isActive)
            {
                ToogleOutline(true);
            }
            isActive = true;
        }

        private void OnMouseExit()
        {
            if (isActive)
            {
                ToogleOutline(false);
            }
            isActive = false;
        }
    }
}

