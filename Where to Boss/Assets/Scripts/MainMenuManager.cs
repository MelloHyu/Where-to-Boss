using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class MainMenuManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    [SerializeField] private int loadBuildIndexPlay;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Play()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick, transform.position);
    }

    public void Settings()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick, transform.position);
    }

    public void Quit()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick, transform.position);
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

    public void StageButtons(int stageIndex)
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick, transform.position);
        SceneManager.LoadScene(stageIndex);
    }


}
