using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts;

public class CanvasController : MonoBehaviour
{
    public Transform mainMenu;
    public Transform mainCharacter;
    public Transform highscore;
    public Text finalScore;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Cancel") && GameController.GetGameState() == GameController.GameState.PLAYING)
        {
            mainMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            GameController.SetGameState(0);
        }   
    }

    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        GameController.SetGameState(1);
    }
    public void setHighscore(float highscore)
    {

    }
    public void Highscore()
    {
        Time.timeScale = 0;
        IWallet wallet = mainCharacter.GetComponent<TrumpController>();
        if(wallet != null)
        {
            finalScore.text = "FINAL SCORE: " + wallet.GetMoney();
        }
        else
        {
            finalScore.text = "NO SCORE YET!";
        }
        highscore.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        GameController.SetGameState(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameController.SetGameState(0);
    }
}
