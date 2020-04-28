using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioSpectrum : MonoBehaviour
{
    public static AudioSource audioSource;

    bool playAudio = true;

    public static float[] freqBands = new float[8];
    public static float[] audioBand = new float[8];

    float[] samples = new float[512];
    float[] freqBandHighest = new float[8];

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = GameManager.songClip;
    }

    // Update is called once per frame
    void Update()
    {

        if(!GameManager.initialPlay) return;
        if(GameManager.initialPlay && playAudio) {
            audioSource.Play();
            playAudio = false;
        }
        


        GetSpectrumAudioSource();
        CreateFreqBands();
        CreateAudioBands();
    }

    void GetSpectrumAudioSource() {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void CreateFreqBands() {
        int count = 0;

        for(int i = 0; i < 8; i++) {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if(i == 7) {
                sampleCount += 2;
            }

            for(int j = 0; j < sampleCount; j++) {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;
            freqBands[i] = average * 10;
        }
    }

    void CreateAudioBands() {
        for(int i = 0; i < 8; i++) {
            if(freqBands[i] > freqBandHighest[i]) {
                freqBandHighest[i] = freqBands[i];
            }

            audioBand[i] = (freqBands[i] / freqBandHighest[i]);
        }
    }

    public void StopAudio() {
        audioSource.Stop();
    }

    public static void SetVolume(float volume) {
        audioSource.volume = volume;
    }
}
