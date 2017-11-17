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
    public GameObject playerLayoutAnswers;
    public Button prefabButtton;

    public Image gridPlayerQuestions;

    // Use this for initialization
    void Start () {

        gameDataManager = new GameDataManager();
        gameDataManager.LoadData();
        playerTurn = (Random.value * 100 < 50); //set initial turn
        if (playerTurn)
            titleTurn.text = PLAYERTURN;
        else
            titleTurn.text = ENEMYTURN;

        loadList();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadList()
    {
        foreach (Sentence sentence in gameDataManager.heroSentences.sentences) {
            Button newButton = Instantiate(prefabButtton);
            newButton.transform.SetParent(gridPlayerQuestions.transform, false);
            newButton.GetComponentInChildren<Text>().text = sentence.text;
        }
        
    }
}
