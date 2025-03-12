using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI enemiesLeftText;
    [SerializeField] GameObject winText;
    int score = 0;
    int enemiesLeft = 0;
    const string SCORE_STRING = "Score: ";
    const string ENEMIES_LEFT_STRING = "Enemies Left: ";

    public void AdjustEnemiesLeft(int amount){
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();
        if(enemiesLeft <= 0){
            winText.SetActive(true);
        }
    }

    public void AdjustScore(int amount){
        score += amount;
        scoreText.text = SCORE_STRING + score.ToString();
    }
    
    public void NewGameButton(){

    }

    public void ResumeGameButton(){

    }

    public void SettingsPanelButton(){

    }

    public void RestartLevelButton(){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void MainMenuButton(){

    }
    
    public void QuitButton(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
