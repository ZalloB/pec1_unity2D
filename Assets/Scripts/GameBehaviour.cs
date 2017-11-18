using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour {

    public const string PLAYER_TURN = "Turno del Jugador";
    public const string ENEMY_TURN = "Turno del Enemigo";
    public const string DEFAULT_TEXT_SENTENCE = "...";
    public const int MAX_SCORE = 3;

    GameDataManager gameDataManager = new GameDataManager();
    public Text titleTurn;
    public GameObject playerLayoutAnswers;
    public Button prefabButtton;
    public Image gridPlayerQuestions;


    bool playerTurn;
    Sentence enemySentence;
    public Text enemyText;
    public Text heroText;

    int playerScore;
    int enemyScore;

    // Use this for initialization
    void Start () {

        gameDataManager.LoadData();

        playerScore = 0;
        enemyScore = 0;
        playerTurn = (Random.value * 100 < 50); //set initial turn
        SetTurn();

       
        //playerLayoutAnswers.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        checkFinalScreen();
    }


    public void checkFinalScreen()
    {
        Debug.Log("Player Score: " + playerScore);
        Debug.Log("Enemy Score: " + enemyScore);
        if (playerScore == MAX_SCORE || enemyScore == MAX_SCORE)
        {
            GameObject.FindWithTag("SceneLoaderManager").GetComponent<SceneLoadManager>().LoadScreen("End");
        }
    }
    /// <summary>
    /// Method to defind the next turn
    /// </summary>
    public void SetTurn() {
        //enabled
        if (playerTurn)
        {
            titleTurn.text = PLAYER_TURN;
            LoadList(gameDataManager.heroSentences.sentences);
        }
        else
        {
            enemySentence = gameDataManager.enemySentences.sentences[Random.Range(0, gameDataManager.enemySentences.sentences.Count)];
            enemyText.text = enemySentence.text;
            heroText.text = DEFAULT_TEXT_SENTENCE;
            LoadList(gameDataManager.answers.sentences);
        }
    }

    /// <summary>
    /// Method that reset the grid and add the sentences that you pass
    /// </summary>
    /// <param name="sentences"></param>
    public void LoadList(List<Sentence> sentences)
    {
        ResetObjectChilds(gridPlayerQuestions.transform);

        foreach (Sentence sentence in sentences) {
            Button newButton = Instantiate(prefabButtton);
            newButton.transform.SetParent(gridPlayerQuestions.transform, false);
            newButton.GetComponent<ButtonSentence>().assingSentence = sentence;
            newButton.GetComponent<Button>().onClick.AddListener(newButton.GetComponent<ButtonSentence>().CheckPlayerSelection);
            newButton.GetComponentInChildren<Text>().text = sentence.text;
        }        

    }

    
    /// <summary>
    /// Generic method that reset all the child of a object
    /// </summary>
    /// <param name="transform"></param>
    public void ResetObjectChilds(Transform transform) {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Game logic, set the score to the player and the enemy
    /// </summary>
    /// <param name="selectedSentence"></param>
    /// <returns></returns>
    public IEnumerator CheckSelection(Sentence selectedSentence)
    {
        //disabled selection
        heroText.text = selectedSentence.text;
        enemyText.text = DEFAULT_TEXT_SENTENCE;
        titleTurn.text = ENEMY_TURN;
        yield return new WaitForSeconds(2);

        if (enemySentence == null)
        {
            enemySentence = gameDataManager.answers.sentences[Random.Range(0, gameDataManager.enemySentences.sentences.Count)];
            enemyText.text = enemySentence.text;
            
        }

        //si el enemigo dice una frase y nosotros respondemos 
        if (enemySentence.answerId == selectedSentence.id)
            playerScore++;
        else
            enemyScore++;

        playerTurn = false;
        SetTurn();
    }
}
