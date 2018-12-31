using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    Animator doorAnimator;
    GameSessionManager myGameSessionManager;

    // Use this for initialization
    void Start () {
        doorAnimator = GetComponent<Animator>();
        myGameSessionManager = FindObjectOfType<GameSessionManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        doorAnimator.SetTrigger("Open");
        StartCoroutine(myGameSessionManager.OpenNextLevel());
    }

    
}
