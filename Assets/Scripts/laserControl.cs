using System.Media;
using UnityEngine;

public class laserControl : MonoBehaviour
{
    int maxBounces = 5;
    private LineRenderer lr;
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private bool reflectionOnlyMirrorIsPossible;

    [SerializeField]
    private string targetObjectTag = "TargetObject"; // Change this to your target object's tag

    // Audio variables
    [SerializeField]
    private AudioClip hitSound; // Assign your sound effect in the inspector
    private AudioSource audioSource;

   
    private bool soundPlayed = false;




    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, startPoint.position);
        audioSource = GetComponent<AudioSource>();
        
    }

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

                if (hit.transform.CompareTag(targetObjectTag))
                {
                    TriggerAction(hit.transform);
                    break;
                }

                if (hit.transform.CompareTag("ResetPitchObject")) // Example tag to reset pitch
                {
                    
                    soundPlayed = false;
                }

                if (hit.transform.CompareTag("Mirror") || !reflectionOnlyMirrorIsPossible)
                {
                    
                    continue;
                }
                else
                {
                    for (int j = (i + 1); j < maxBounces; j++)
                    {
                        lr.SetPosition(j, hit.point);
                    }
                    break;
                }
            }
            else
            {
                lr.SetPosition(i + 1, position + direction * 100);
                break;
            }
        }
    }

    void TriggerAction(Transform hitObject)
    {
        print("hitobject");
        // Play the sound effect when the target object is hit
        PlayHitSound();
    }

    void PlayHitSound()
    {
        if (!soundPlayed) // Check if the sound has already been played
        {
            print(audioSource);
            
            audioSource.PlayOneShot(hitSound);
            
            soundPlayed = true; // Set the flag to true to prevent further playback
        }

    }
}
