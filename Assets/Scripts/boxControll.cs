using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxControll : MonoBehaviour
{

    public spin1Box spinibox1;


    public void Start()
    {

        //spinibox = GetComponent<spinBox>();
        spinibox1.enabled = false;

    }

    public void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        print("entered");
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            print("it ws the player");
            spinibox1.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            spinibox1.enabled = false;
        }
    }

    
}
