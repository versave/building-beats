using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject gameOverMenu;
    public Text timeText;

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
        if (GameManager.gameOver && openMenu) {
            gameOverMenu.SetActive(true);
            timeText.text = SecondsToTime(AudioSpectrum.audioSource.time);
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
