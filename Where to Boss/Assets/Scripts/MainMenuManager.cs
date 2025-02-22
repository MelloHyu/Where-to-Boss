using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class MainMenuManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    [SerializeField] private int loadBuildIndexPlay;
    public void Play()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        SceneManager.LoadScene(loadBuildIndexPlay);
    }

    public void Settings()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
    }

    public void Quit()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }


}
