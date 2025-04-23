using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    public float minPitch;
    public float maxPitch;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(minPitch, maxPitch);
    }
}
