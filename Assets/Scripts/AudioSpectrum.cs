﻿using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioSpectrum : MonoBehaviour
{
    public static AudioSource audioSource;

    public static float[] freqBands = new float[8];
    public static float[] audioBand = new float[8];
    public static bool beatFinished = false;

    readonly float[] samples = new float[512];
    readonly float[] freqBandHighest = new float[8];
    
    bool playAudio = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = GameManager.songClip;
        beatFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.initialPlay) return;
        if(!GameManager.initialPlay && playAudio) {
            audioSource.Play();
            playAudio = false;
        }

        if(!audioSource.isPlaying && !beatFinished) {
            beatFinished = true;
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

    public static void SetVolume(float volume) {
        audioSource.volume = volume;
    }
}
