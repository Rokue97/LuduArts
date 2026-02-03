using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource m_AudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySfx(AudioClip clip, float volume = 0.7f)
    {
        m_AudioSource.PlayOneShot(clip, volume);
    }
}
