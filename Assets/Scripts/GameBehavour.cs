using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBehavour : MonoBehaviour {

    public const string PLAYERTURN = "Turno del Jugador";
    public const string ENEMYTURN = "Turno de la IA";

    bool playerTurn;
    GameDataManager gameDataManager;
    public Text titleTurn;

	// Use this for initialization
	void Start () {

        gameDataManager = new GameDataManager();
        gameDataManager.LoadData();
        playerTurn = (Random.value * 100 < 50); //set initial turn
        if (playerTurn)
            titleTurn.text = PLAYERTURN;
        else
            titleTurn.text = ENEMYTURN;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
