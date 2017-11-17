using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataManager {


    public ListSentence enemySentences;
    public ListSentence heroSentences;
    public ListSentence answers;

    private string gameDataAnswersFilePath = "../StreamingAssets/data_answers.json";
    private string gameDataQuestionsEnemyFilePath = "../StreamingAssets/data_questions_enemy.json";
    private string gameDataQuestionsHeroFilePath = "../StreamingAssets/data_questions_hero.json";
    
    public void LoadData()
    {
        //file data
        heroSentences = GetData(gameDataQuestionsHeroFilePath);
        enemySentences = GetData(gameDataQuestionsEnemyFilePath);
        answers = GetData(gameDataAnswersFilePath);
    }

    public ListSentence GetData(string path)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, path);
        ListSentence list = new ListSentence();

        if (File.Exists(filePath)) {
            string data = File.ReadAllText(filePath);
            list = JsonUtility.FromJson<ListSentence>(data);
        }
        else
        {
            Debug.LogError("File doesnt exist" +filePath);
        }

        return list;
    }
}