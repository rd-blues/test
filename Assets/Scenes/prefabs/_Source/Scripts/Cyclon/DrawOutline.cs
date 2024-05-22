using UnityEngine;

[RequireComponent(typeof(Outline))]
public class DrawOutline : MonoBehaviour
{
    private void OnMouseEnter()
    {
        GetComponent<Outline>().enabled = true;
    }
    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
    }

}