using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts;
using System.IO;

public class CanvasController : MonoBehaviour
{
    public Transform mainMenu;
    public Transform highscore;
    public Transform enemySpawn;

    public Text finalScore;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        finalScore.text = "";
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

    public void Highscore()
    {
        Time.timeScale = 0;
        var enemySpawnObj = enemySpawn.GetComponent<EnemySpawn>();
        if (enemySpawnObj != null)
        {
            ArrayList scores = new ArrayList();
            Debug.Log("im in highscore!");
            finalScore.text = "Your score was " + (enemySpawnObj.healthToAdd + 500) + "\n\n";

            FileStream highScoreFile;
            if (!File.Exists("highscores.txt"))
            {
                highScoreFile = File.Create("highscores.txt");
                Debug.Log("Created new highscore");
            } else
            {
                highScoreFile = File.Open("highscores.txt",FileMode.Append);
                Debug.Log("highscore already existed");
            }
         
            using (StreamWriter streamWriter = new StreamWriter(highScoreFile))
            {
                streamWriter.WriteLine((enemySpawnObj.healthToAdd + 500));
                Debug.Log("writing " + (enemySpawnObj.healthToAdd + 500) + "to file");
            }

            var allLines = File.ReadAllLines("highscores.txt");
            foreach (string line in allLines)
            {
                scores.Add(int.Parse(line));
            }
            scores.Sort();
            scores.Reverse();
            Debug.Log("there was " + scores.ToArray().Length + "nr of scores");
                
            
            int ranking = 1;
            foreach (int score in scores)
            {
                finalScore.text += ranking + ") " + score + "\n";
                ranking++;
            }

            highScoreFile.Close();
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
