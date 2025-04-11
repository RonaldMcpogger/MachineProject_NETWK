using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class GameHandler2 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Game;
    public TMP_InputField GuessText;

    // display texts

    public TMP_Text Player1GuessDisplay;
    public TMP_Text Player2GuessDisplay;
    public TMP_Text CurrentPlayerDisplay;

    //  public GameObject player2;

    public static int currentPlayer;

    int tempGuess;
    int QuesNumber; //the number to be guessed

    public GameObject MenuHandlerObject;
    public GameObject Client;

    public static int PlayerNumber;
    public GameObject WinnerText;



    void Start()
    {
        WinnerText.SetActive(false);
        QuesNumber = UnityEngine.Random.Range(0, 100);
        currentPlayer = 1;
    }
    
    void Update()
    {
        if (TCPClient.update)
        {
            GameUpdate();
            TCPClient.update = false;
        }
    }
    // Update is called once per frame


    public void GuessNumber()
    {
        if (GameObject.Find("AnswerInput").GetComponent<TMP_InputField>().text == string.Empty
            || GameObject.Find("AnswerInput").GetComponent<TMP_InputField>().text == null)
            return;

            WinnerText.SetActive(false);
            tempGuess = int.Parse(GameObject.Find("AnswerInput").GetComponent<TMP_InputField>().text);
            Debug.Log("Number Guessed:" + tempGuess);
            Debug.Log("Actual Number:" + QuesNumber);

        
            string judgement;
            if (QuesNumber < tempGuess)
            {
                judgement = "Too High";
                Debug.Log("Higher");
            }
            else if (QuesNumber > tempGuess)
            {
                judgement = "Too Low";
                Debug.Log("Lower");
            }
            else
            judgement = "Winnder";

            Client.GetComponent<TCPClient>().SendMessageToServer(tempGuess.ToString());
            Client.GetComponent<TCPClient>().SendMessageToServer(judgement);

            //DisplayCurrPlayer();

            /*if (currentPlayer == 1)
            {
                this.Player1GuessDisplay.text = "Player1: " + tempGuess + " (" + judgement + ")";
                currentPlayer = 2;
            }
                
            else
            {
                this.Player2GuessDisplay.text = "Player2: " + tempGuess + " (" + judgement + ")"; ;
                currentPlayer = 1;
            }*/




        


    }

    public void GameUpdate()
    {
        if (TCPClient.Judgement == "Winner")
        {
            WinnerText.SetActive(true);
            switch (currentPlayer)
            {
                case 1:
                    GameReset();
                    WinnerText.GetComponent<TMP_Text>().text = "Player 1 Guessed Correctly! : " + TCPClient.Guess;
                    break;


                case 2:
                    WinnerText.GetComponent<TMP_Text>().text = "Player 2 Guessed Correctly! : " + TCPClient.Guess;
                    GameReset();
                    break;
            }
        }
        else
        {
            if (currentPlayer == 1)
            {
                this.Player1GuessDisplay.text = "Player1: " + TCPClient.Guess + " (" + TCPClient.Judgement + ")";
                currentPlayer = 2;
            }

            else
            {
                this.Player2GuessDisplay.text = "Player2: " + TCPClient.Guess + " (" + TCPClient.Judgement + ")"; ;
                currentPlayer = 1;
            }

            if (PlayerNumber == currentPlayer)
                MenuHandlerObject.GetComponent<MenuHandler>().GameActive();
            else
                MenuHandlerObject.GetComponent<MenuHandler>().LoadingActive();

            DisplayCurrPlayer();
        }
    }

    private void GameReset()
    {
        QuesNumber = UnityEngine.Random.Range(0, 100);
        currentPlayer = 1;
        this.Player1GuessDisplay.text = "Player1: ";
        this.Player2GuessDisplay.text = "Player2: ";
        this.CurrentPlayerDisplay.text = "Current Player: " + currentPlayer.ToString();

        if (PlayerNumber == currentPlayer)
            MenuHandlerObject.GetComponent<MenuHandler>().GameActive();
        else
            MenuHandlerObject.GetComponent<MenuHandler>().LoadingActive();
    }

    public void Set()
    {
        if (PlayerNumber == currentPlayer)
            MenuHandlerObject.GetComponent<MenuHandler>().GameActive();
        else
            MenuHandlerObject.GetComponent<MenuHandler>().LoadingActive();
    }
    public void DisplayCurrPlayer()
    {
        this.CurrentPlayerDisplay.text = "Current Player: " + currentPlayer.ToString();

    }
}
