using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour {
    public const string WINNER_TEXT = "Fin de partida.\n Ganador: ";
    public const string PLAYER = "Jugador";
    public const string ENEMY = "Enemigo";
    public const int MAX_SCORE = 3;

    public Text winnerText;
	// Use this for initialization
	void Start () {
        if (GameObject.Find("ScoreManager").GetComponent<ScoreManager>().playerScore == MAX_SCORE)
            winnerText.text = WINNER_TEXT + PLAYER;
        else
            winnerText.text = WINNER_TEXT + ENEMY;

        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().playerScore = 0;
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().enemyScore = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
