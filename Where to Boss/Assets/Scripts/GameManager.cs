using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private int restartLevel;
    [SerializeField] public AudioSource stageMusic;
    public void RestartLevel()
    {
        SceneManager.LoadScene(restartLevel);
    }

    public static GameManager Instance;

        [SerializeField] private TMP_Text scoreText; // Assign in Unity UI
        private float startTime;
        private bool isGameRunning = true;
        public float timeTook = 0.0f;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        void Start()
        {
            startTime = Time.time; // Capture game start time
         timeTook = 0f;
        }

        void Update()
        {
            if (isGameRunning)
            {
                float elapsedTime = Time.time - startTime; // Real elapsed time
                float score = elapsedTime; // Score based on real-time
                timeTook = score;
                if (scoreText != null)
                    scoreText.text = "Time: " + Mathf.FloorToInt(score).ToString();
            }
        }

        public void StopScoring()
        {
            stageMusic.Stop();
            isGameRunning = false;
        }

        public float GetElapsedTime()
        {
            return Time.time - startTime; // Get total elapsed time
        }
    
}
   

