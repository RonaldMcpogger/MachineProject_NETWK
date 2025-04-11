using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Game;
    public GameObject GuessInputs;
    public TMP_InputField GuessText;
    public GameObject Buttons;
    // display texts
    public TMP_Text Player1ScoreDisplay;
    public TMP_Text Player2ScoreDisplay;

    public TMP_Text CurrentPlayerDisplay;
    //  public GameObject player2;
    int player1Score;
    int player2Score;

    int currentPlayer;

    int tempGuess;
    int QuesNumber; //the number to be guessed



    void Start()
    {
        QuesNumber = UnityEngine.Random.Range(0, 100);
        currentPlayer = 1;
        player1Score = 0;
        player2Score = 0;
        Buttons.SetActive(false);
        
    }

    // Update is called once per frame
    

    public void GuessNumber()
    {
        Debug.Log("waww");
       
        
            tempGuess = int.Parse(GameObject.Find("AnswerInput").GetComponent<TMP_InputField>().text);
        if(tempGuess == QuesNumber )
        {
            switch(currentPlayer)
            {
                case 1:
                    player1Score++;
                    Player1ScoreDisplay.text = "Player1: "+player1Score.ToString();
                    refresh();
                    currentPlayer = 2;
                    
                    break;
                case 2:
                    player2Score++;
                    Player2ScoreDisplay.text = "Player2: " + player2Score.ToString();
                    refresh();
                    currentPlayer = 1;
                    break;
            }
            DisplayCurrPlayer();
        }
        else
        {
            Buttons.SetActive(true);
            GuessInputs.SetActive(false);
            DisplayCurrPlayer();
        }

    }

    private void refresh()
    {
        GuessText.text = "";
        Buttons.SetActive(false);
        tempGuess = 0;
        QuesNumber = UnityEngine.Random.Range(0, 100);
        GuessInputs.SetActive(true);
     

    }
    public void Lower()
    {
        if(tempGuess > QuesNumber)
        {
            switch (currentPlayer)
            {
                case 1:
                    player1Score++;
                    Player1ScoreDisplay.text = "Player1: " + player1Score.ToString();
                    refresh();
                    currentPlayer = 2;
                    break;
                case 2:
                    player2Score++;
                    Player2ScoreDisplay.text = "Player2: " + player2Score.ToString();
                    refresh();
                    currentPlayer = 1;
                    break;
            }
        }
        else
        {
            switch (currentPlayer)
            {
                case 1:

                    refresh();
                    currentPlayer = 2;
                    break;
                case 2:

                    refresh();
                    currentPlayer = 1;
                    break;
            }
        }
        DisplayCurrPlayer();

    }
    public void Higher()
    {

        if (tempGuess < QuesNumber)
        {
            switch (currentPlayer)
            {
                case 1:
                    player1Score++;
                    Player1ScoreDisplay.text = "Player1: " + player1Score.ToString();
                    refresh();
                    currentPlayer = 2;
                    break;
                case 2:
                    player2Score++;
                    Player2ScoreDisplay.text = "Player2: " + player2Score.ToString();
                    refresh();
                    currentPlayer = 1;
                    break;
            }
        }
        else
        {
            switch (currentPlayer)
            {
                case 1:
                    
                    refresh();
                    currentPlayer = 2;
                    break;
                case 2:
                    
                    refresh();
                    currentPlayer = 1;
                    break;
            }
        }
        DisplayCurrPlayer();

    }
    public void DisplayCurrPlayer()
    {
        CurrentPlayerDisplay.text = "Current Player: " + currentPlayer.ToString();
    }
}
