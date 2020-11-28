using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] Music;
    public AudioSource MusicSource;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Playlist");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Playlist()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (!MusicSource.isPlaying)
            {
                if (i != (Music.Length - 1))
                {
                    i++;
                    MusicSource.clip = Music[i];
                    MusicSource.Play();
                }
                else
                {
                    i = 0;
                    MusicSource.clip = Music[i];
                    MusicSource.Play();
                }
            }
        }
    }
}
