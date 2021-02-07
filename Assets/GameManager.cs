using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        audioSource.Stop();
        Destroy(player);
        gameOverPanel.SetActive(true);
    }

    public void Win()
    {
        audioSource.Stop();
        winPanel.SetActive(true);
    }


    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
