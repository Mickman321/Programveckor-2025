using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;

// skapad av vincent fajersson
public class AudioManager : MonoBehaviour
{
    // Ljud - Vincent
    public static AudioManager Instance; // g?r s? att allting kan anv?nda denna script

    public Sound[] musicSounds, sfxSounds; // skapar en array f?r att ha informationen av ljud 
    public AudioSource musicSource, sfxSource; // skapar en array f?r att ha informationen av musik 
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider mainSlider;
    private void Update(){
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetMainVolume(mainSlider.value);

    }
    private void Awake()
    {
        if (Instance == null) // ifall det inte finns en AudioManager i spelet s? g?r den en AudioManager 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // f?rst?r INTE AudioManager n?r spelet
        }
        else
        {
            Destroy(gameObject); // ifall det redan finns en AudioManager i spelet s? f?rst?r den denna objekt s? det inte blir massor med errors.
        }
    }

    private void Start()
    {
        PlayMusic("game"); // spelar musiken n?r man ?ppnar spelet
    }

    public void PlayMusic(string name) // g?r igenom array som vi skapade i b?rjan f?r att hitta musik
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found"); // s?ger till ifall den inte kan hitta ljud effekt som vi ska anv?nda
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name); // g?r igenom array som vi skapade i b?rjan f?r att hitta ljud effekter

        if (s == null)
        {
            Debug.Log("Sound not found"); // s?ger till ifall den inte kan hitta ljud effekt som vi ska anv?nda
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume); 
        //print("garg Music " + volume);
    }

    // Method to set the volume of the SFX
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume); 
       // print("garg SFX " + volume);
    }

    public void SetMainVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume); 
        //print("garg Master " + volume);
    }
}
