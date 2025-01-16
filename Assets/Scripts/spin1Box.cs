using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin1Box : MonoBehaviour
{

    public float rotationSpeed = 100f; // Speed of rotation

    void Update()
    {
        // Check for "D" key press for right rotation
        if (Input.GetKey(KeyCode.Q))
        {
            Rotate(Vector3.up); // Rotate around the Y-axis to the right
        }

        // Check for "A" key press for left rotation
        if (Input.GetKey(KeyCode.E))
        {
            Rotate(Vector3.down); // Rotate around the Y-axis to the left
        }
    }

    private void Rotate(Vector3 direction)
    {
        // Rotate the object based on the direction and speed
        transform.Rotate(direction * rotationSpeed * Time.deltaTime);
    }
}
