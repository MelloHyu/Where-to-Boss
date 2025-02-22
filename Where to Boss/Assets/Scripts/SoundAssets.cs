using UnityEngine;
using UnityEngine.Audio;

public class SoundAssets : MonoBehaviour
{

    public static SoundAssets Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public AudioMixerGroup audioMixerGroup;
    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;

    }


   

}
