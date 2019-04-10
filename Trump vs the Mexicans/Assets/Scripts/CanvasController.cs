using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts;
using System.IO;
using System.Net;
using System;
using UnityEngine.Networking;
using System.Text;

public class CanvasController : MonoBehaviour
{
    public Transform mainMenu;
    public Transform highscore;
    public Transform enemySpawn;
    public InputField playerName;
    private List<ScoreObject> scores = new List<ScoreObject>();
    public Text finalScore;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetLeaderboard());
        Time.timeScale = 0;
        finalScore.text = "";
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetButton("Cancel") && GameController.GetGameState() == GameController.GameState.PLAYING)
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
        try
        {

       
        Time.timeScale = 0;
        var enemySpawnObj = enemySpawn.GetComponent<EnemySpawn>();
        if (enemySpawnObj != null)
        {
           

            finalScore.text = "Your score was " + (enemySpawnObj.healthToAdd + 500) + "\n\n";

            var name = playerName.text;
            StartCoroutine(PutScoreToLeaderboard(new ScoreObject
            {
                name = name,
                score = (enemySpawnObj.healthToAdd + 500)
            }));
                
            scores.Sort();
            scores.Reverse();

                Debug.Log("size of array: "+scores.ToArray().Length);
            int ranking = 1;
            foreach (var score in scores)
            {
                finalScore.text += ranking + ") " + score.name + " (" + score.score + ") \n";
                ranking++;
            }

            }
        else
        {
            finalScore.text = "NO SCORE YET!";
        }
        highscore.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        GameController.SetGameState(2);
        }catch(Exception e)
        {

        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator GetLeaderboard()
    {
        Debug.Log("get leaderboard");
        string url = "http://mbirch.dk/trump/api/index.php?method=get";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            //Debug.Log(www.downloadHandler.text);
            var line = www.downloadHandler.text;

            var dataArray = line.Split(';');
            Debug.Log("leaderboard contained: " + dataArray.Length + " pairs");
            foreach (string data in dataArray)
            {
                var scoreName = data.Split('[');
                //Debug.Log(data);

                if (data == "" || data == " ")
                {   
                    continue;
                }
                //Debug.Log("from string to int... score: " + scoreName[0]);
                //Debug.Log("name: " + scoreName[1]);
                scores.Add(new ScoreObject
                {
                    name = scoreName[1],
                    score = int.Parse(scoreName[0])
                });
            }


        }

    }
    private IEnumerator PutScoreToLeaderboard(ScoreObject scoreObject)
    {
        string url = "http://mbirch.dk/trump/api/index.php?method=put&score="+scoreObject.score + "[" + scoreObject.name;
        //Debug.Log("url: " + url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            //Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameController.SetGameState(0);
    }
}
