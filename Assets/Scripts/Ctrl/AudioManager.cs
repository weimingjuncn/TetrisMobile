using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    Ctrl ctrl;
    public AudioClip cursor;
    public AudioClip drop;
    public AudioClip control;
    public AudioClip lineClear;

    bool isMute;
    AudioSource audioSource;

    private void Awake()
    {
        ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Ctrl>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCursor()
    {
        PlayAudio(cursor);
    }

    public void PlayDrop()
    {
        PlayAudio(drop);
    }

    public void PlayControl()
    {
        PlayAudio(control);
    }

    public void PlayLineClear()
    {
        PlayAudio(lineClear);
    }

    private void PlayAudio(AudioClip clip)
    {
        if (isMute) return;

        audioSource.clip = clip;
        audioSource.Play();
    }
    public void OnAudioButtonClick()
    {
        isMute = !isMute;
        ctrl.view.SetMuleActive(isMute);
        if(isMute == false)
        {
            PlayCursor();
        }
    }
}
