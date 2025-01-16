using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxControllpickup : MonoBehaviour
{
    public spinBox spinibox;


    public void Start()
    {
       
        //spinibox = GetComponent<spinBox>();
        spinibox.enabled = false;
       
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
            spinibox.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            spinibox.enabled = false;
        }
    }
}
