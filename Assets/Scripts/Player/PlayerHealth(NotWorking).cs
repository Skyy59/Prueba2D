using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealth : MonoBehaviour
{

    public int maxlives = 5;
    public int lives;
    public int dmgDelay = 5;

    private UIHealth health;
   
    // Start is called before the first frame update
    void Start()
    {
        lives = maxlives;
        health = FindObjectOfType<UIHealth>();  

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage()
    {
        if (lives > 0)
            lives = lives - 1;

        PlayerController hCtr = gameObject.GetComponent<PlayerController>();
        hCtr.animator.SetBool("Damaged", true);

        health.UpdateHealth(lives);

        PlayerAudioController hAudio = gameObject.GetComponent<PlayerAudioController>();
        hAudio.Damage();    
    }

    public void regeneration()
    {
        lives = lives + 1;

    }
    

}
