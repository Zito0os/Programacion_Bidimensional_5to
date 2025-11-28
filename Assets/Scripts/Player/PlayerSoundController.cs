using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    //Sonidos del jugador
    public AudioSource audioSource;
    public AudioClip sonidoCaminar;
    public AudioClip sonidoSaltar;
    public AudioClip sonidoDisparo;
    public AudioClip sonidoDamage;
    public AudioClip sonidoMorir;

    //Sonidos de consumbibles
    public AudioClip sonidoVida;
    public AudioClip sonidoMunicion;

    public void playCaminar()
    {
        audioSource.PlayOneShot(sonidoCaminar);
    }
    public void playSaltar()
    {
        audioSource.PlayOneShot(sonidoSaltar);
    }
    public void playDisparo()
    {
        audioSource.PlayOneShot(sonidoDisparo);
    }
    public void playDamage()
    {
        audioSource.PlayOneShot(sonidoDamage);
    }
    public void playMorir()
    {
        audioSource.PlayOneShot(sonidoMorir);
    }
    public void playVida()
    {
        audioSource.PlayOneShot(sonidoVida);
    }
    public void playMunicion()
    {
        audioSource.PlayOneShot(sonidoMunicion);
    }


}