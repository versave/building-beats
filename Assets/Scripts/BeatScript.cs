using UnityEngine;
using UnityEngine.UI;

public class BeatScript : MonoBehaviour
{
    public AudioClip beat;
    public Sprite defaultSprite;
    public Sprite selectedSprite;

    public bool selected;
    public int beatIndex;

    public void SetActiveBeat() {
        GameManager.songClip = beat;
        GameManager.selectedBeatIndex = beatIndex;

        foreach(GameObject beatEl in GameObject.FindGameObjectsWithTag("Beat Button")) {
            beatEl.GetComponent<Image>().sprite = defaultSprite;
            beatEl.GetComponent<BeatScript>().selected = false;
        }

        GetComponent<Image>().sprite = selectedSprite;
        GetComponent<BeatScript>().selected = true;
    }
}
