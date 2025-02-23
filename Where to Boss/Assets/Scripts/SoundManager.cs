using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager
{
    
   
    public enum Sound
    {
        CarHorn,
        ButtonClick,
        CarBreak,
        Crash1,
        Crash2,
        Crash3,
        Crash4,
        PlayerVoiceLine,
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = SoundAssets.Instance.audioMixerGroup;
        audioSource.clip = GetAudioClip(sound);
        audioSource.Play();
        Object.Destroy(soundGameObject, audioSource.clip.length);
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = SoundAssets.Instance.audioMixerGroup;
        audioSource.PlayOneShot(GetAudioClip(sound));
        Object.Destroy(soundGameObject, audioSource.clip.length);
    }


    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.Instance.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + "not found");
        return null;
    }


}
