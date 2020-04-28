using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static AudioClip songClip;
    public AudioClip defaultClip;
    public AudioSource audioContainer;
    public GameObject player;
    
    public static int? selectedBeatIndex = 1;
    public static bool playIntro = true;
    public static bool initialPlay = false;
    public static bool gameOver = false;
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
    }

    void GameOver() {
        player.GetComponent<Player>().PlayerFall();
        AudioSpectrum.SetVolume(0.2f);
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
        gameOver = false;
        LoadScene(0);
    }

    public void RestartGame() {
        gameOver = false;
        LoadScene(1);
    }
}
