using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    public bool mirandoDerecha = true;

    public void Flip(float direccionX)
    {
        if (direccionX > 0 && !mirandoDerecha)
        {
            // Voltear a la derecha
            transform.localScale = new Vector3(1, 1, 1);
            mirandoDerecha = true;
        }
        else if (direccionX < 0 && mirandoDerecha)
        {
            // Voltear a la izquierda
            transform.localScale = new Vector3(-1, 1, 1);
            mirandoDerecha = false;
        }
    }
}
