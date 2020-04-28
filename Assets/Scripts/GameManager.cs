using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static AudioClip songClip;
    public AudioClip defaultClip;
    public AudioSource audioContainer;
    
    public static int? selectedBeatIndex = 1;
    public static bool playIntro = true;
    public static bool gameIsPlaying = false;
    public bool godMode;

    int deaths;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);

        if(songClip == null) songClip = defaultClip;

        if (GameObject.FindGameObjectsWithTag("Game Manager").Length > 1) {
            Destroy(GameObject.FindGameObjectsWithTag("Game Manager")[0]);
        }

        if(deaths == 0) {
            gameIsPlaying = false;
        }
    }

    private void Update() {
        if(Player.gameOver && !godMode) {
            Player.gameOver = false;
            deaths++;

            LoadScene(1);
        }    
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
        gameIsPlaying = true;
    }
}
