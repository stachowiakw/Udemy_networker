using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSessionManager : MonoBehaviour {

    [SerializeField] int numberOfLivesOnStart;
    [SerializeField] Text livesText;

    int numberOfLives;

    private void Awake()
    {
        if (FindObjectsOfType<GameSessionManager>().Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start() {
        numberOfLives = numberOfLivesOnStart;
        livesText.text = numberOfLives.ToString();
    }

    public void LevelProcedureOnDeath()
    {
        numberOfLives--;
        livesText.text = numberOfLives.ToString();
        if (numberOfLives <= 0)
        {
            SceneManager.LoadScene(0);
            numberOfLives = numberOfLivesOnStart;
            livesText.text = numberOfLives.ToString();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public IEnumerator OpenNextLevel()
    {
        print("opennextlevel");
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
