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

    public AudioClip failedSentence;
    public AudioClip successSentence;

    // Use this for initialization
    void Start () {

        gameDataManager.LoadData();

        playerScore = 0;
        enemyScore = 0;
        playerTurn = false; //(Random.value * 100 < 50 to do random) set initial turn
        SetTurn();

       
        //playerLayoutAnswers.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        checkFinalScreen();
    }


    public void checkFinalScreen()
    {
        Debug.Log("Player Score: " + GameObject.Find("ScoreManager").GetComponent<ScoreManager>().playerScore);
        Debug.Log("Enemy Score: " + GameObject.Find("ScoreManager").GetComponent<ScoreManager>().enemyScore);
        if (GameObject.Find("ScoreManager").GetComponent<ScoreManager>().playerScore == MAX_SCORE ||
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().enemyScore == MAX_SCORE)
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
       

        if (enemySentence == null)
        {
            enemySentence = gameDataManager.answers.sentences[Random.Range(0, gameDataManager.enemySentences.sentences.Count)];
            enemyText.text = enemySentence.text;
            
        }

        //si el enemigo dice una frase y nosotros respondemos 
        if (enemySentence.answerId == selectedSentence.id)
        {
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().playerScore++;
            SoundManager.instance.RandomizeSfx(successSentence);
        }
        else
        {
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().enemyScore++;
            SoundManager.instance.RandomizeSfx(failedSentence);
        }
        yield return new WaitForSeconds(2);
           
        titleTurn.text = PLAYER_TURN;
        playerTurn = false;
        SetTurn();
    }
}
