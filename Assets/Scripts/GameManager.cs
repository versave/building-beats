﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static AudioClip songClip;
    public AudioClip defaultClip;
    public AudioSource audioContainer;
    public GameObject player;
    
    public static int? selectedBeatIndex = 3;
    public static bool playIntro = true;
    public static bool initialPlay = false;
    public static bool gameOver = false;
    public static bool gameFinish = false;

    public bool godMode;

    int deaths;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);

        if(songClip == null) songClip = defaultClip;

        if (GameObject.FindGameObjectsWithTag("Game Manager").Length > 1) {
            Destroy(GameObject.FindGameObjectsWithTag("Game Manager")[0]);
        }

        if(deaths == 0) {
            initialPlay = false;
        }
    }

    private void Update() {
        if(!player) {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (gameOver && !godMode) {
            deaths++;   
            GameOver();
        }

        if(initialPlay && !AudioSpectrum.audioSource.isPlaying && AudioSpectrum.audioSource.time > 0) {
            gameFinish = true;
        }
    }

    void GameOver() {
        float volume = AudioSpectrum.audioSource.volume;

        player.GetComponent<Player>().PlayerFall();
        if(volume > 0) FadeVolume(volume, 0.002f);
    }

    void FadeVolume(float volume, float value) {
        AudioSpectrum.SetVolume(volume - value);
    }

    public void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public static IEnumerator GetAudioClip(string path) {
        string songPath = @"file://" + path + "";

        // Place path here
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(songPath, AudioType.MPEG)) {
            yield return www.SendWebRequest();

            if (www.isNetworkError) {
                Debug.Log(www.error);
            } else {
                // Send to game
                songClip = DownloadHandlerAudioClip.GetContent(www);
            }
        }
    }

    public void OpenFileBrowser() {
        StartCoroutine(GetComponent<SongBrowser>().ShowLoadDialogCoroutine());
    }

    public void StartGame() {
        initialPlay = true;
    }

    public void BackToMenu() {
        ResetGame();
        LoadScene(0);
    }

    public void RestartGame() {
        ResetGame();
        LoadScene(1);
    }

    void ResetGame() {
        gameOver = false;
        gameFinish = false;
        Player.topReached = false;
    }
}
