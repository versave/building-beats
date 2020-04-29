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
                beat.GetComponent<Image>().sprite = beatScript.selectedSprite;
                beat.GetComponent<BeatScript>().selected = true;
            } else {
                beat.GetComponent<Image>().sprite = beatScript.defaultSprite;
                beat.GetComponent<BeatScript>().selected = false;
            }
        }
    }

    public static void ResetBeats() {
        foreach (GameObject beat in GameObject.FindGameObjectsWithTag("Beat Button")) {
            BeatScript beatScript = beat.GetComponent<BeatScript>();

            beat.GetComponent<Image>().sprite = beatScript.defaultSprite;
            beat.GetComponent<BeatScript>().selected = false;
        }
    }
}
