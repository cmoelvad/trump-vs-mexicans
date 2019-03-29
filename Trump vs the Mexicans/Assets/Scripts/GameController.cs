using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform mainCharacter;
    public Text moneyText;

    private void Update()
    {
        IWallet wallet = mainCharacter.GetComponent<IWallet>();
        if (wallet != null)
        {
            moneyText.text = wallet.GetMoney() + "";
        }
    }

}
