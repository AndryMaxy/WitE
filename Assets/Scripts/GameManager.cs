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
        //gameOverPanel.SetActive(true);
        SceneManager.LoadScene(SceneConstants.GAME_OVER, LoadSceneMode.Additive);
    }

    public void Win()
    {
        int level = Utils.parseLevel(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt(PlayerPrefsConstants.COMPLETED_LEVEL, level);
        player.enabled = false;
        backgroundAudio.Stop();
        SceneManager.LoadScene(SceneConstants.LEVEL_COMPLETED, LoadSceneMode.Additive);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(SceneConstants.MENU);
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
