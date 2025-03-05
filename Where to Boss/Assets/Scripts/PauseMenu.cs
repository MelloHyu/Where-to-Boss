using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1f ;
    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        
    }

}
