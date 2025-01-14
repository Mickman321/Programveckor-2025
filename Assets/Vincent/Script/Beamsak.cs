using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beamsak : MonoBehaviour // vincent
{
    public Transform[] targets; 
    public GameObject beamPrefab; 
    public float beamSpeed = 10f; 
    public float beamDuration = 2f; 

    private GameObject[] activeBeams; 

    void Start()
    {
        if (targets.Length == 0 || beamPrefab == null)
        {
            Debug.LogError("Assign targets and a beam prefab!");
            return;
        }

        
        activeBeams = new GameObject[targets.Length];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBeams();
        }
    }

    void FireBeams()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == null) continue;

            GameObject beam = Instantiate(beamPrefab, transform.position, Quaternion.identity);
            activeBeams[i] = beam;

            StartCoroutine(MoveBeam(beam, targets[i].position));
        }
    }

    IEnumerator MoveBeam(GameObject beam, Vector3 targetPosition)
    {
        float elapsedTime = 0f;

        while (elapsedTime < beamDuration)
        {
            if (beam == null) yield break;

            beam.transform.position = Vector3.MoveTowards(beam.transform.position, targetPosition, beamSpeed * Time.deltaTime);

            Vector3 direction = (targetPosition - beam.transform.position).normalized;
            if (direction != Vector3.zero)
                beam.transform.rotation = Quaternion.LookRotation(direction);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(beam);
    }
}
