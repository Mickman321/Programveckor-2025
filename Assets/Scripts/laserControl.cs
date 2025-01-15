using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserControl : MonoBehaviour
{
    int maxBounces = 5;
    private LineRenderer lr;
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private bool reflectionOnlyMirrorIsPossible;

    // Specify the tag of the object you want to trigger an action on hit
    [SerializeField]
    private string targetObjectTag = "TargetObject"; // Change this to your target object's tag

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, startPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        CastLaser(transform.position, -transform.forward);
    }

    void CastLaser(Vector3 position, Vector3 direction)
    {
        lr.SetPosition(0, startPoint.position);
        for (int i = 0; i < maxBounces; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, 1))
            {
                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                lr.SetPosition(i + 1, hit.point);

                // Check if the hit object has the specified tag
                if (hit.transform.CompareTag(targetObjectTag))
                {
                    // Trigger your action here
                    TriggerAction(hit.transform);
                    break; // Exit the loop if the target object is hit
                }

                if (hit.transform.CompareTag("Mirror") || !reflectionOnlyMirrorIsPossible)
                {
                    // Continue reflecting if it's a mirror or reflection is allowed
                    continue;
                }
                else
                {
                    // If it's not a mirror and reflection is not allowed, stop the laser
                    for (int j = (i + 1); j < maxBounces; j++)
                    {
                        lr.SetPosition(j, hit.point);
                    }
                    break;
                }
            }
            else
            {
                // If no hit, set the last position to the end of the ray
                lr.SetPosition(i + 1, position + direction * 100);
                break;
            }
        }
    }

    void TriggerAction(Transform hitObject)
    {

        print("hitobject");
        // Implement the action you want to trigger when the laser hits the target object
        //Debug.Log("Laser hit the target object: " + hitObject.name);
        // You can start a class, call a method, or perform any action here
    }

}
