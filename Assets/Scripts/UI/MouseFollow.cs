using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    //private void Update()
    //{
    //    FaceMouse();
    //}
    private void FixedUpdate()
    {
        FaceMouse();
    }
    private void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;   
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction; 
    }
}
