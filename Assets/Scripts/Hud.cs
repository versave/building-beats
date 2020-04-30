using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject gameOverMenu;
    public GameObject gameFinishMenu;
    public Text timeText;
    public Text attemptsText;

    bool openMenu = true;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.initialPlay) {
            startMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Open game over menu
        if (GameManager.gameOver && openMenu) {
            gameOverMenu.SetActive(true);
            timeText.text = SecondsToTime(AudioSpectrum.audioSource.time);
            openMenu = false;
        }

        // Open finish menu
        if (GameManager.gameFinish && Player.topReached && openMenu) {
            gameFinishMenu.SetActive(true);
            attemptsText.text = GameManager.deaths.ToString();
            openMenu = false;
        }
    }

    string SecondsToTime(float secs) {
        float minutes = Mathf.Floor(secs / 60);
        float seconds = Mathf.Floor(secs % 60);

        string secondsString = "0" + seconds + "";
        secondsString = secondsString.Length > 2 ? secondsString.Substring(1) : secondsString;

        string minutesString = "0" + minutes + "";
        minutesString = minutesString.Length > 2 ? minutesString.Substring(1) : minutesString;

        return minutesString + ":" + secondsString;
    }
}
