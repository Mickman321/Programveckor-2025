using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepad : MonoBehaviour
{

    public float bounceForce = 10f; // Adjust to set bounce strength

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a Rigidbody
        Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

        if (playerRigidbody != null)
        {
            // Calculate the direction from the bounce pad to the player
            Vector3 bounceDirection = (collision.transform.position - transform.position).normalized;

            // Apply force from the bounce pad to the player
            playerRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }


}
