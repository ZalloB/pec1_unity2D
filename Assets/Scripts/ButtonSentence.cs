using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSentence : MonoBehaviour {

    public Sentence assingSentence;
    public GameObject gameBehaviour;

    void Start()
    {
        gameBehaviour = GameObject.FindWithTag("GameBehaviour");
    }
    public void CheckPlayerSelection()
    {
        StartCoroutine(gameBehaviour.GetComponent<GameBehaviour>().CheckSelection(assingSentence));
       
    }
}
