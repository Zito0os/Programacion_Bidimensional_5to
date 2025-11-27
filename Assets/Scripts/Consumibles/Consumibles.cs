using UnityEngine;

// Script para objetos consumibles (botiquines, recargas de munición)
//este scriptva  en el prefab del consumible y selecciona el tipo y cantidad en el inspector pa ver cuanto le pones
[RequireComponent(typeof(Collider2D))]
public class Consumibles : MonoBehaviour
{
    public enum TipoConsumible
    {
        Botiquin,
        Municion
    }

    [Header("Configuración Consumible")]
    public TipoConsumible tipo = TipoConsumible.Botiquin;
    [Tooltip("Cantidad a agregar (vida o munición según el tipo).")]
    public int cantidad = 5;
    [Tooltip("Destruir el objeto al recogerlo.")]
    public bool destruirAlRecoger = true;

    // Asegurar que el collider sea trigger para una recogida suave
    private void Reset()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true; // por defecto lo ponemos como trigger
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        AplicarEfecto();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // por si el triger no jala se usa tmb el d colision
        if (!collision.gameObject.CompareTag("Player")) return;
        AplicarEfecto();
    }

    private void AplicarEfecto()
    {
        if (GameManager.Instance == null) return; // seguridad

        switch (tipo)
        {
            case TipoConsumible.Botiquin:
                GameManager.Instance.AddHealth(cantidad);
                break;
            case TipoConsumible.Municion:
                GameManager.Instance.AddAmmo(cantidad);
                break;
        }

        if (destruirAlRecoger)
        {
            Destroy(gameObject);
        }
    }
}
