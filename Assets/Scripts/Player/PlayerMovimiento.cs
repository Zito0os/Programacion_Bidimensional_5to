using Unity.Android.Gradle.Manifest;
using UnityEngine;


public class PlayerMovimiento : MonoBehaviour
{

    public Animator animator; // Arrastras el Animator en el inspector
    public SpriteRenderer spriteRenderer; // Para voltear el personaje
    public SpriteRenderer Mano_arma; // Para voltear el personaje
    //public SpriteRenderer spawnbala; // Para voltear el personaje
    public SpriteRenderer cabeza; // Para voltear el personaje
    //public SpriteRenderer spriteArma; // Para voltear el personaje

    public Rigidbody2D Rigidbody;
    public float speed = 5f;

    public float gravity = -9.8f;
    Vector2 velocity;

    // Comprobaci�n de suelo
    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;
    bool isGrounded;


    public Transform crosshair; // la mirilla

    // Salto
    public float jumpHeight = 3f;

    void Update()
    {
        // Detectar si est� en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, sphereRadius, groundMask);

        // Movimiento horizontal
        float x = Input.GetAxis("Horizontal");
        Rigidbody.linearVelocity = new Vector2(x * speed, Rigidbody.linearVelocity.y);



        // Animación caminar/idle
        animator.SetFloat("Speed", Mathf.Abs(x));

      




        // Dirección a la que estoy apuntando
        bool aimingRight = (crosshair.position.x > transform.position.x);

        // Dirección a la que me estoy moviendo
        bool movingRight = (x > 0);

        // Sprite del jugador se voltea SOLO segun hacia dónde apunto
        spriteRenderer.flipX = !aimingRight;
        Mano_arma.flipY = !aimingRight;
        cabeza.flipX = !aimingRight;
        //spawnbala.flipX = false;

        // Detectar si estoy caminando hacia atrás
        if (x != 0) // solo si hay movimiento
        {
            animator.SetBool("WalkBackwards", (aimingRight != movingRight));
        }
        else
        {
            animator.SetBool("WalkBackwards", false);
        }




















        //// Voltear sprite
        //if (x > 0)
        //{
        //    Mano_arma.flipX = false;

        //}
        //else if (x < 0)
        //    Mano_arma.flipX = true;






        //Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            
            Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, Mathf.Sqrt(jumpHeight * -2f * gravity));
            //animator.SetTrigger("Jump");


        }
        animator.SetBool("ensuelo", isGrounded);
    }
}
