using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    //Define Variable

    public string StuckObjectTag = "Wall";
    // Inicializacion
    void Awake () 
    {
        //We cannot be sure that other awakes are finished
        //Executed Once
        //Executed if the object is inactive
        Debug.Log(transform.position);
        Debug.Log(transform.rotation);
        Debug.Log(transform.localScale);

        Vector2 pos2D = new Vector2(2, 5);
        Vector3 pos3D = new Vector3(2, 5, 8);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Executed Once
        //This is executed after awake is called
        //Executed if the object is active
       
    }

    // Update is called once per frame
    void Update()
    {
        //Executed every single frame of the game
        //Used for gameplay logic
        
        
        transform.eulerAngles +=  new Vector3(0, 0, -500f) * Time.deltaTime;
        transform.position += new Vector3(15f, 0, 0) * Time.deltaTime;
        transform.localScale += new Vector3(0.001f, 0.001f, 0.001f) * Time.deltaTime;
    }

    void FixedUpdate()
    {
        //Executed every fixed step
        //Used for physics
        
    }

    void LateUpdate()
    {
        //Executed every single frame of the game
        //Executed  at the end of the frame
        //For camera control
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(StuckObjectTag))
            {

            GetComponent<Rigidbody>().isKinematic = true;

        }

    }
}
