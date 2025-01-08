using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour

{
    public float DamageForce = 5f;

    private Collider2D Collider;
    private PlayerHealth vida;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth vida = collision.gameObject.GetComponent<PlayerHealth>();
        vida.Damage();

        Rigidbody2D CRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        CRigidbody.AddForce(Vector2.up * DamageForce, ForceMode2D.Impulse);

       
    }
}
