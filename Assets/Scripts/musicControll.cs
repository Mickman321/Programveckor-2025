using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class musicControll : MonoBehaviour
{
    private bool entered;
    public AudioClip bling;
    public AudioSource source;
    private int count = 1;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        entered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (entered == true)
            source.PlayOneShot(bling);


    }

   



}
