using UnityEngine;
using UnityEngine.SceneManagement;
public class WinManager : MonoBehaviour
{
    [SerializeField] int loadNextSceneIndex;
    [SerializeField] GameObject winScreen;

    private void Start()
    {
        winScreen.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        winScreen.SetActive(true);
    }

    public void NextStage()
    {
        SceneManager.LoadScene(loadNextSceneIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
