using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public class EagleC : MonoBehaviour
{
    public float speed = 4f;

    public Rigidbody2D cRigidbody;
    public SpriteRenderer cRenderer;
    public Seeker cSeeker;
    public Transform player;

    public float detDistance = 3f;
    public float damageForce = 5f;

    private Path path;
    private int currentPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("UpdatePath", 0f, .5f); //
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentPoint = 0;
        }
    }

    void UpdatePath()
    {
        Vector3 playerDir = player.transform.position - transform.position;

        if (playerDir.magnitude < detDistance && playerDir.x > 0)
        {
            cSeeker.StartPath(cRigidbody.position, player.position, OnPathComplete);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 playerDir = player.transform.position - transform.position; //Posicion jugador - posicion enemigo


        if (playerDir.magnitude < detDistance && playerDir.x > 0) // El jugador esta dentro del rango y lo estoy viendo
        {
            Debug.Log("El enemigo persige al jugador");

            cRigidbody.velocity = new Vector2(playerDir.normalized.x * speed, cRigidbody.velocity.y);
        }
        else
    

        EnemyOrientation();

        if (path != null)
        {
            Vector3 dir = path.vectorPath[currentPoint] - transform.position;
            dir.z = 0;

            cRigidbody.velocity = dir.normalized * speed;

            if (dir.magnitude < 0.01)
                currentPoint = currentPoint + 1;
        } 
    }
    void EnemyOrientation()
    {
        // Accedo al componente Sprite Renderer
        if (cRigidbody.velocity.x < 0)
            cRenderer.flipX = false;
        else if (cRigidbody.velocity.x > 0)
            cRenderer.flipX = true;
    }

}