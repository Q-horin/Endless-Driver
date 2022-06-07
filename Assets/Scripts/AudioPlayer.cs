using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Van")]
    [SerializeField] AudioClip horn;
    [SerializeField] [Range(0f, 1f)] float hornVolume = 1f;
    [SerializeField] AudioClip engineStart;
    [SerializeField] AudioClip engineLoop;
    [SerializeField] [Range(0f, 1f)] float vanSoundsVolume = 1f;

    [Header("Radio Music")]
    [SerializeField] AudioClip mainTheme;
    [SerializeField] [Range(0f, 1f)] float radioVolume = 1f;

    static AudioPlayer instance;
    AudioSource[] audioSources;
    AudioSource audioSourceA;
    AudioSource audioSourceB;
    private bool musicFadeOutEnabled;

    public bool hasEnginSoundEnded;
 
    void Awake() 
    {
        //audioSourceA = GetComponent<AudioSource>();   
        audioSources = GetComponents<AudioSource>();
        audioSourceA = audioSources[0];
        audioSourceB = audioSources[1];

    }

    void PlayClip(AudioClip clip, float volume)
    {   
        if (clip != null)
        {
        Vector3 cameraPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

    // AUDIO PLAYER METHODS:

    IEnumerator PlayMainTheme()
    {
        yield return new WaitForSecondsRealtime(2f);
        musicFadeOutEnabled = false;
        audioSourceB.clip = mainTheme;
        audioSourceB.loop = true;
        audioSourceB.volume = radioVolume;
        audioSourceB.Play(); 
    }

    public void StartMusicAndSounds()
    {
        StartCoroutine(PlayEngineSound());
        StartCoroutine(PlayMainTheme());
    }

    IEnumerator PlayEngineSound()
    {
        audioSourceA.clip = engineStart;
        audioSourceA.volume = vanSoundsVolume;
        audioSourceA.Play();
        yield return new WaitForSeconds(audioSourceA.clip.length);
        hasEnginSoundEnded = true;
        audioSourceA.clip = engineLoop;
        audioSourceA.loop = true;
        audioSourceA.Play();
    }
    
    public void PlayHornClip()
    {
        PlayClip(horn, hornVolume);
    }
    public void FadeOutMusic()
    {
    musicFadeOutEnabled = true;
    }

    public bool HasEnginSoundEnded()
    {
        return hasEnginSoundEnded;
    }

    public void StopClips()
    {
        audioSourceA.Stop();
    }
}
