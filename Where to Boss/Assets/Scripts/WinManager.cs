using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WinManager : MonoBehaviour
{
    [SerializeField] int loadNextSceneIndex;
    [SerializeField] GameObject winScreen;
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        winScreen.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            winScreen.SetActive(true);
            GameManager.Instance.StopScoring();
            if (scoreText != null)
                scoreText.text = "Time: " + Mathf.FloorToInt(GameManager.Instance.timeTook).ToString();
        }
        
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
