using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> checkPoints;
    [SerializeField] Vector3 vectorPoint;
    [SerializeField] float dead;

    private bool ifDeadPlayMusicAndNoMorePls = false;

    private void Update()
    {
        if (player.transform.position.y < -dead)
        {
            
            if (!ifDeadPlayMusicAndNoMorePls)
            {
                AudioManager.Instance.PlaySFX("gameover");
                ifDeadPlayMusicAndNoMorePls = true;
            }
            StartCoroutine(RespawnTimer());
        }

    }

    IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(5f);
        player.transform.position = vectorPoint;
        ifDeadPlayMusicAndNoMorePls = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        vectorPoint = player.transform.position; 
        Destroy(other.gameObject);

    }

}
