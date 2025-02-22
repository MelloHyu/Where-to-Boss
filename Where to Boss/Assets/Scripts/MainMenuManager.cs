using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
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

}
