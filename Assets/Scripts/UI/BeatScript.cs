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

        GameObject[] beatButtons = GameObject.FindGameObjectsWithTag("Beat Button");
        int length = beatButtons.Length;

        for(int index = 0; index < length; index++) {
            beatButtons[index].GetComponent<Image>().sprite = defaultSprite;
            beatButtons[index].GetComponent<BeatScript>().selected = false;
        }

        GetComponent<Image>().sprite = selectedSprite;
        GetComponent<BeatScript>().selected = true;
    }
}
