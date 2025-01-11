using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlataformasVerticales : MonoBehaviour
{
    public Transform[] waypoints;
    public float velocidad = 1.5f;
    public float tiempoDeEspera = 1f;
    public Transform player;
    public Transform plataforma;
    public BoxCollider2D PCollider;

    private int start = 0;
    private bool esperando = false;

    void Update()
    {
        //si no esta esperando, se movera al siguiente waypoint
        if (!esperando)
        {
            MoveToWaypoint();
        }

        Collision();
    }

    void MoveToWaypoint()
    {
        //La plataforma se movera hacia el siguiente waypoint
        Vector3 dir = waypoints[start].position - transform.position;

        transform.position += dir.normalized * velocidad * Time.deltaTime;

        //Cuando la plataforma llegue al waypoint, esperara un tiempo antes de volver al siguiente lado
        if (dir.magnitude < 0.1f)
        {
            esperando = true;
            Invoke("CambiarWaypoint", tiempoDeEspera);
        }


    }

    void CambiarWaypoint()
    {
        //Para que pueda cambiar del waypoint 0 al 1, se incrementa el valor y asi se inicia de nuevo el proceso de movimiento
        start++;

        //si el valor start es mayor que la cantidad de waypoints, este valor se reiniciara a 0
        if (start >= waypoints.Length)
        {
            start = 0;
        }

        esperando = false;
    }

    //Metodo para la colision de la plataforma vertical
    void Collision()
    {
        //si la posicion vertical del jugador es mayor a la de la plataforma, esta gana colision
        if (player.transform.position.y > plataforma.transform.position.y)
        {
            //se activa el boxcollider2D de la plataforma para darle una hitbox
            plataforma.GetComponent<BoxCollider2D>().enabled = true;
            
        }
        else
        {
            //si el personaje esta por debajo de la plataforma, esta pierde colision para poder atravesarla
            plataforma.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Cuando entra en collision con el Jugador comparando si tiene la tag de "Player", convierte al Jugador en hijo de la plataforma para que se mueva junto a ella
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;

        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //quita el padre-hijo de la plataforma-jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }

    }
}
