﻿using UnityEngine;
using UnityEngine.UI;

public class BeatMenu : MonoBehaviour
{
    public GameObject beatsBox;
    public GameObject beatTemplate;
    public float beatDistance;
    public float beatDistanceAddition;

    int? loadedBeatIndex;

    void Start()
    {
        loadedBeatIndex = GameManager.selectedBeatIndex;
        LoadBeats();
    }

    public static void ResetBeats() {
        GameObject[] beats = GameObject.FindGameObjectsWithTag("Beat Button");
        int beatsLength = beats.Length;

        for (int index = 0; index < beatsLength; index++) {
            BeatScript beatScript = beats[index].GetComponent<BeatScript>();

            beats[index].GetComponent<Image>().sprite = beatScript.defaultSprite;
            beats[index].GetComponent<BeatScript>().selected = false;
        }
    }

    void LoadBeats() {
        // Beats array
        AudioClip[] beats = Resources.LoadAll<AudioClip>("Music");
        RectTransform templateRect = beatTemplate.GetComponent<RectTransform>();
        float templateHeight = templateRect.rect.height;
        int beatsLength = beats.Length;
        float lastY = 0;

        SizeBeatsBox(beatsLength);

        for (int index = 0; index < beatsLength; index++) {
            float yPosition = index > 0 ? lastY - templateHeight + beatDistanceAddition : beatDistance;
            lastY = yPosition;

            GameObject beat = Instantiate(beatTemplate, new Vector3(0, yPosition, 0), Quaternion.identity);
            BeatScript beatScript = beat.GetComponent<BeatScript>();

            beat.transform.SetParent(beatsBox.transform, false);
            beat.transform.GetChild(0).GetComponent<Text>().text = beats[index].name;
            
            beatScript.beat = beats[index];
            beatScript.beatIndex = index;
            beatScript.selected = loadedBeatIndex == index ? true : false;

            if (beatScript.selected) beat.GetComponent<Image>().sprite = beatScript.selectedSprite;
        }
    }

    void SizeBeatsBox(int multiplier) {
        if (multiplier < 6) return;

        RectTransform boxRect = beatsBox.GetComponent<RectTransform>();
        boxRect.offsetMin = new Vector2(boxRect.offsetMin.x, 100 * (-multiplier + 5));
    }
}
