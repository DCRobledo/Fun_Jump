using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Sound[] sounds;

    public static AudioController Instance;


    private void Awake()
    {
        if(Instance == null)
           Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            AudioMixerGroup targetMixer = audioMixer.FindMatchingGroups("Master/" + sound.GetTargetMixer().ToString())[0];

            sound.SetSource(gameObject.AddComponent<AudioSource>(), targetMixer);
            sound.GetSource().playOnAwake = false;
        }
    }


    public void Play(string name)
    {

        
        Sound mySound = Array.Find(sounds, sound => sound.GetName() == name);

        if (mySound == null)
            Debug.LogWarning($"WARNING: Sound {name} not found");

        else if (!mySound.GetSource().isPlaying)
            mySound.GetSource().Play();
    }

    public void Stop(string name)
    {
        Sound mySound = Array.Find(sounds, sound => sound.GetName() == name);

        if (mySound == null)
            Debug.LogWarning($"WARNING: Sound {name} not found");
        else
            mySound.GetSource().Stop();
    }

    public void Pause(string name)
    {
        Sound mySound = Array.Find(sounds, sound => sound.GetName() == name);

        if (mySound == null)
            Debug.LogWarning($"WARNING: Sound {name} not found");
        else
            mySound.GetSource().Pause();
    }

    public void UnPause(string name)
    {
        Sound mySound = Array.Find(sounds, sound => sound.GetName() == name);

        if (mySound == null)
            Debug.LogWarning($"WARNING: Sound {name} not found");
        else
            mySound.GetSource().UnPause();
    }


    public void FadeIn(String soundName, float fadeDuration)
    {
        Sound mySound = Array.Find(sounds, sound => sound.GetName() == soundName);

        Debug.Log("Fading in music: " + soundName);

        if (mySound == null)
            Debug.LogWarning($"WARNING: Sound {soundName} not found");
        else
            StartCoroutine(FadeInCoroutine(mySound, fadeDuration));
    }

    public void FadeOut(String soundName, float fadeDuration)
    {
        Sound mySound = Array.Find(sounds, sound => sound.GetName() == soundName);

        if (mySound == null)
            Debug.LogWarning($"WARNING: Sound {name} not found");
        else
            StartCoroutine(FadeOutCoroutine(mySound, fadeDuration));
    }

    private IEnumerator FadeInCoroutine(Sound sound, float fadeDuration)
    {
        sound.GetSource().Play();

        float currentTime = 0;
        float start = sound.GetSource().volume;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            sound.GetSource().volume = Mathf.Lerp(0, start, currentTime / fadeDuration);
            yield return null;
        }

        yield break;
    }

    private IEnumerator FadeOutCoroutine(Sound sound, float fadeDuration)
    {
        float currentTime = 0;
        float start = sound.GetSource().volume;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            sound.GetSource().volume = Mathf.Lerp(start, 0, currentTime / fadeDuration);
            yield return null;
        }

        sound.GetSource().Stop();

        yield break;
    }

    public bool SoundExists(string name) {
        return Array.Find(sounds, sound => sound.GetName() == name) != null; 
    }

    private void ChangePitchToSound(string name, float pitch){
        Sound mySound = Array.Find(sounds, sound => sound.GetName() == name);
        if (mySound == null)
            Debug.LogWarning($"WARNING: Sound {name} not found");
        else
            mySound.GetSource().pitch = pitch;
    }
}
