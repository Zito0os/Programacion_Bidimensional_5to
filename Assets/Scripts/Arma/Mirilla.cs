using UnityEngine;

public class Mirilla : MonoBehaviour
{

    

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // En 2D, dejamos Z en 0
        transform.position = mousePos;
        Cursor.visible = false;
    }
}
