using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private int restartLevel;


    public void RestartLevel()
    {
        SceneManager.LoadScene(restartLevel);
    }
}
