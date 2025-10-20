using UnityEngine;
using UnityEngine.Audio;

/*
  Title:Settings Menu in UNITY! 2021 Tutorial
  Author: GDTitians 
  Date: 30 May 2021
  Code version: 
  Availability:https://youtu.be/iT49pNcu1jk?si=51lUixeUfwqRK5xx
  */

public class NewMonoBehaviourScript : MonoBehaviour
{
    public AudioMixer mainMixer;
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }
}
