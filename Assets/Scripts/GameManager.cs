using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public bool godMode;

    public static AudioClip songClip;
    public AudioSource audioContainer;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);

        if(GameObject.FindGameObjectsWithTag("Game Manager").Length > 1) {
            Destroy(GameObject.FindGameObjectsWithTag("Game Manager")[0]);
        }
    }

    private void Update() {
        if(Player.gameOver && !godMode) {
            Player.gameOver = false;

            LoadScene(1);
        }    
    }

    public void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void QuitGame() {
        Application.Quit();
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
}
