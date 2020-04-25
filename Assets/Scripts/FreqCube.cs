using UnityEngine;
using UnityEngine.UI;

public class FreqCube : MonoBehaviour
{
    public int band;
    public float startScale;
    public float scaleMultiplier;

    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AudioSpectrum.freqBands[band] > 2) {
            GetComponent<Image>().color = new Color32(240, 52, 52, 255);
        } else {
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        rect.sizeDelta = new Vector2(rect.rect.width, (AudioSpectrum.freqBands[band] * scaleMultiplier) + startScale);
        transform.position = new Vector2(transform.position.x, rect.rect.height / 2);
    }
}
