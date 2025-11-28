using UnityEngine;
using System.Collections;

public class EnemigoGordo : MonoBehaviour
{
   
    [SerializeField] private float velocidad = 1.2f;
    [SerializeField] private float vida = 250f;
    [SerializeField] private float rangoDeteccion = 8f;
    [SerializeField] private float rangoAtaque = 1.8f;
    [SerializeField] private float fuerzaEmpuje = 18f;
    [SerializeField] private float tiempoEntreAtaques = 1.2f;
    [SerializeField] private float dañoGolpe = 20f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private Transform jugador;
    private bool puedeAtacar = true;
    private bool mirandoDerecha = true;
    private const float umbralFlip = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        if (GameObject.FindWithTag("Player") != null)
            jugador = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        animator.SetBool("islive", true); 

        if (jugador == null) return;

        float distanciaTotal = Vector2.Distance(transform.position, jugador.position);
        float distanciaX = jugador.position.x - transform.position.x;

        VoltearSprite(distanciaX);

        if (distanciaTotal < rangoDeteccion)
        {
            if (distanciaTotal <= rangoAtaque)
            {
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("caminando", false);

                
                if (puedeAtacar && !animator.GetBool("atacando"))
                {
                    StartCoroutine(Atacar());
                }
            }
            else
            {
                animator.SetBool("caminando", true);
                float direccionX = Mathf.Sign(distanciaX);
                Vector2 movimiento = new Vector2(direccionX, 0f);
                rb.MovePosition(rb.position + movimiento * velocidad * Time.fixedDeltaTime);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("caminando", false);
        }
    }

    private void VoltearSprite(float deltaX)
    {
        if (Mathf.Abs(deltaX) < umbralFlip || sprite == null) return;
        bool irDerecha = deltaX > 0;
        if (irDerecha != mirandoDerecha)
        {
            mirandoDerecha = irDerecha;
            sprite.flipX = !irDerecha;
        }
    }

    IEnumerator Atacar()
    {
        puedeAtacar = false;
        animator.SetBool("atacando", true);

        yield return new WaitForSeconds(0.6f); // Tiempo al impacto

        if (jugador != null && Vector2.Distance(transform.position, jugador.position) <= rangoAtaque + 0.5f)
        {
            Rigidbody2D rbJugador = jugador.GetComponent<Rigidbody2D>();
            if (rbJugador != null)
            {
                Vector2 direccionEmpuje = (jugador.position - transform.position).normalized;
                rbJugador.velocity = Vector2.zero;
                rbJugador.AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);
                GameManager.Instance.ReduceHealth((int)dañoGolpe);
            }
        }

        
        yield return new WaitForSeconds(Mathf.Max(0f, tiempoEntreAtaques - 0.6f));
        animator.SetBool("atacando", false);
        puedeAtacar = true;
    }

    public void QuitarVida(float cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
        {
            Morir();
        }
       
    }

    private void Morir()
    {
        animator.SetBool("muerto", true);
        animator.SetBool("islive", false);
        enabled = false;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }
        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}