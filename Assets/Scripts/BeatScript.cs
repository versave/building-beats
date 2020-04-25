using UnityEngine;
using UnityEngine.UI;

public class BeatScript : MonoBehaviour
{
    public AudioClip beat;
    public bool selected;
    public int beatIndex;

    public void SetActiveBeat() {
        GameManager.songClip = beat;
        GameManager.selectedBeatIndex = beatIndex;

        foreach(GameObject beatEl in GameObject.FindGameObjectsWithTag("Beat Button")) {
            beatEl.GetComponent<Image>().color = Color.white;
            beatEl.GetComponent<BeatScript>().selected = false;
        }

        GetComponent<Image>().color = new Color(0, 207, 241, 255);
        GetComponent<BeatScript>().selected = true;
    }
}
