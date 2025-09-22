using UnityEngine;
using UnityEngine.Audio;


//Title:Settings Menu in UNITY! 2021 Tutorial
//Author: GDTitans
//Date: 30 may 2021
//Code version;n/a
//Availability; https://youtu.be/iT49pNcu1jk?si=wks94tCgCGHnWH2e

public class NewMonoBehaviourScript : MonoBehaviour
{
    public AudioMixer mainMixer;
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }
}
