using UnityEngine;
using UnityEngine.UI;

public class BeatMenu : MonoBehaviour
{
    void Start()
    {
        int? loadedBeatIndex = GameManager.selectedBeatIndex;

        foreach(GameObject beat in GameObject.FindGameObjectsWithTag("Beat Button")) {
            BeatScript beatScript = beat.GetComponent<BeatScript>();

            if(beatScript.beatIndex == loadedBeatIndex) {
                beat.GetComponent<Image>().color = new Color(0, 207, 241, 255);
                beat.GetComponent<BeatScript>().selected = true;
            } else {
                beat.GetComponent<Image>().color = Color.white;
                beat.GetComponent<BeatScript>().selected = false;
            }
        }
    }

    public static void ResetBeats() {
        foreach (GameObject beat in GameObject.FindGameObjectsWithTag("Beat Button")) {
            BeatScript beatScript = beat.GetComponent<BeatScript>();

            beat.GetComponent<Image>().color = Color.white;
            beat.GetComponent<BeatScript>().selected = false;
        }
    }
}
