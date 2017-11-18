using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSentence : MonoBehaviour {

    public Sentence assingSentence;
    public GameObject gameBehaviour;
    public AudioClip interfaceSong;

    void Start()
    {
        gameBehaviour = GameObject.FindWithTag("GameBehaviour");
    }
    public void CheckPlayerSelection()
    {
        SoundManager.instance.RandomizeSfx(interfaceSong);
        StartCoroutine(gameBehaviour.GetComponent<GameBehaviour>().CheckSelection(assingSentence));
       
    }
}
