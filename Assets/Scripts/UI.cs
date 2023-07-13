using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField]private TMP_Text gameOver;
    [SerializeField] private TMP_Text playerStatus;

    public void Restart() 
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reset the scene
    }

    public void GameOverTxt(string text)  // gameover text
    {
        gameOver.text = text;
    }
    public void PlayerStatusTxt(string text) //playersts text
    {
        playerStatus.text = text;
    }

    public void Exit() // Quit 
    {
        Application.Quit();
    }
}
