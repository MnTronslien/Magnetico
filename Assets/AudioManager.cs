using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audsor;



    private void Start()
    {
        instance = this;
        audsor = instance.GetComponent<AudioSource>();
    }

    public static void Play(AudioClip a)
    {
        instance.audsor.PlayOneShot(a);
    }


    // Start is called before the first frame update



}
