using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum GameState {  MAINMENU, PLAYING, HIGHSCORE}
    public Transform mainCharacter;
    public Text moneyText;
    public CanvasController cc;
    private static GameState gameState;

    private void Update()
    {
        IWallet wallet = mainCharacter.GetComponent<IWallet>();
        if (wallet != null)
        {
            moneyText.text = wallet.GetMoney() + "";
        }
    }

    public void GoToHighScore()
    {
        cc.Highscore();
    }

    public static void SetGameState(int state)
    {
        switch (state)
        {
            case 0:
                gameState = GameState.MAINMENU;
                break;
            case 1:
                gameState = GameState.PLAYING;
                break;
            case 2:
                gameState = GameState.HIGHSCORE;
                break;
        }
    }

    public static GameState GetGameState()
    {
        return gameState;
    }
}
