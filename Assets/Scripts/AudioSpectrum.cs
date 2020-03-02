﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioSpectrum : MonoBehaviour
{
    AudioSource audioSource;
    float[] samples = new float[512];
    public static float[] freqBands = new float[8];
    float[] freqBandHighest = new float[8];
    public static float[] audioBand = new float[8];

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        createFreqBands();
        createAudioBands();
    }

    void GetSpectrumAudioSource() {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void createFreqBands() {
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

    void createAudioBands() {
        for(int i = 0; i < 8; i++) {
            if(freqBands[i] > freqBandHighest[i]) {
                freqBandHighest[i] = freqBands[i];
            }

            audioBand[i] = (freqBands[i] / freqBandHighest[i]);
        }
    }
}