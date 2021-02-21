using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject map;

    private AudioSource backgroundAudio;

    void Start()
    {
        backgroundAudio = GetComponent<AudioSource>();
        backgroundAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        backgroundAudio.Stop();
        Destroy(player);
        gameOverPanel.SetActive(true);
    }

    public void Win()
    {
        player.enabled = false;
        backgroundAudio.Stop();
        winPanel.SetActive(true);
    }


    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowMap()
    {
        player.Disable();
        map.SetActive(true);
    }

    public void HideMap()
    {
        player.Enable();
        map.SetActive(false);
    }
}
