using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnergyBox : ScriptToUse
{
    [SerializeField] private List<Opened> energyMassive = new List<Opened>();
    public AudioClip clip;
    AudioSource audioSource;
    bool isAudioPlaying;

    public override void Toggle()
    {
        active = !active;
        ToggleAudio(active);
        ToggleBoolInOpened(active);
    }
    public void ToggleBoolInOpened(bool state)
    {
        foreach (Opened opened in energyMassive)
        {
            opened.SetState(state);
        }
    }
    public void ToggleAudio(bool active)
    {
        try
        {
            if (active && !isAudioPlaying)
            {
                audioSource.Play();
                isAudioPlaying = true;
            }
            else if (!active && isAudioPlaying)
            {
                audioSource.Stop();
                isAudioPlaying = false;

            }
        }
        catch
        {
            Debug.Log("Ã”«€ ¿ ¬ ƒ¬»∆Œ  Õ≈ ”—“¿ÕŒ¬À≈Õ¿");
        }

    }
    public void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        ToggleBoolInOpened(active);
    }
}


