using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformasHorizontales : MonoBehaviour
{
    public Transform[] waypoints; // Array de waypoints
    public float velocidad = 1.5f;  // Velocidad de movimiento plataforma
    public float tiempoDeEspera = 1f; // Tiempo que la plataforma espera en cada waypoint
    

    private int start = 0; // Índice del waypoint actual
    private bool esperando = false; // Si la plataforma está esperando en el waypoint actual

    void Update()
    {
        if (!esperando)
        {
            // Mover la plataforma al waypoint actual
            MoverHaciaWaypoint();
        }
    }

    void MoverHaciaWaypoint()
    {
        // Mover la plataforma hacia el waypoint objetivo
        Vector3 dir = waypoints[start].position - transform.position;

        transform.position += dir.normalized * velocidad * Time.deltaTime;

        // Si la plataforma ha llegado al waypoint
        if (dir.magnitude < 0.1f)
        {
            esperando = true;
            // Esperar un tiempo antes de mover hacia el siguiente waypoint
            Invoke("CambiarWaypoint", tiempoDeEspera);
        }


    }

    void CambiarWaypoint()
    {
        // Pasar al siguiente waypoint
        start++;

        // Si llegamos al final del array de waypoints, volvemos al primer waypoint
        if (start >= waypoints.Length)
        {
            start = 0;
        }

        esperando = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;

        }
    
    }

     void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }

    }
}
