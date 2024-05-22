using UnityEngine;

namespace ExtensionMethods
{
    public static class Extension
    {
        public static float MapFloat(this float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
        public static int MapInt(this int x, int in_min, int in_max, int out_min, int out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        public static bool RayCastChek(GameObject rayTarget, float rayLength)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
            {
                if (hit.transform == rayTarget.transform)
                {
                    // Debug.Log(hit.transform.name);
                    return true;
                }
            }
            return false;
        }
    }
}