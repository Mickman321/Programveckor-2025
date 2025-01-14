using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_SOTC : MonoBehaviour // Vincent
{
    // 
    public Transform[] targets; 
    public KeyCode raycastKey = KeyCode.Space; 
    public float raycastDistance = 100f; 
    public LayerMask poi; 

    void Update()
    {

        if (Input.GetKeyDown(raycastKey))
        {
            ShootRays();
        }
    }

    void ShootRays()
    {
        foreach (Transform target in targets)
        {
            if (target == null) continue;

            Vector3 direction = target.position - transform.position;


            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, raycastDistance, poi))
            {
                Debug.Log("Raycast hit " + hit.collider.gameObject.name + " at " + hit.point);
            }
            else
            {
                Debug.Log("Raycast towards "+ target.name + "did not hit any object.");
            }


            Debug.DrawLine(transform.position, target.position, Color.red, 10f);
        }
    }

}
