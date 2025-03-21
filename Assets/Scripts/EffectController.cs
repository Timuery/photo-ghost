using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Состояния игрока
/// </summary>
public class EffectController: MonoBehaviour
{
    // Словарь для хранения приоритетов эффектов
    private static readonly Dictionary<PlayerEffect, int> effectPriority = new Dictionary<PlayerEffect, int>
    {
        {PlayerEffect.Stunning, 90},
        {PlayerEffect.Hit, 80},
        {PlayerEffect.Running, 50},
        {PlayerEffect.Walk, 40},
        {PlayerEffect.Photo, 10},
        {PlayerEffect.None, 0}
    };


    private PlayerEffect activeEffects;
    private PlayerController player;
    private float originalSpeed;
    private AudioSource audioSource;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip runClip;
    [SerializeField] private AudioClip stunClip;
    [SerializeField] private AudioClip hurtClip;

    [SerializeField] private float rotateStrength = 10f;
    private PlayerEffect currentSoundEffect;



    public EffectController(PlayerController player, AudioSource audioSource)
    {
        this.player = player;
        this.audioSource = audioSource;
        originalSpeed = player.moveSpeed;
        
    }

    public void AddEffect(PlayerEffect effect)
    {
        
        if (HasEffect(effect))
        {
            RemoveEffect(effect);
        }
        else
        {
            activeEffects |= effect;
            HandleEffectStart(effect);
        }
        UpdateSound();
    }

    public void RemoveEffect(PlayerEffect effect)
    {
        activeEffects &= ~effect;
        HandleEffectEnd(effect);
        UpdateSound();
    }
    private void UpdateSound()
    {
        PlayerEffect highestPriorityEffect = GetHighestPriorityEffect();

        if (highestPriorityEffect == currentSoundEffect)
            return;

        // Мгновенная остановка текущего звука
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        currentSoundEffect = highestPriorityEffect;
        PlayCurrentEffectSound();
    }
    private void PlayCurrentEffectSound()
    {
        AudioClip clip = null;
        bool loop = false;

        switch (currentSoundEffect)
        {
            case PlayerEffect.Walk:
                clip = walkClip;
                loop = true;
                break;
            case PlayerEffect.Running:
                clip = runClip;
                loop = true;
                break;
            case PlayerEffect.Stunning:
                clip = stunClip;
                loop = true;
                break;
            case PlayerEffect.Hit:
                clip = hurtClip;
                loop = false;
                break;
            case PlayerEffect.None:
                clip = null;
                loop = false;
                break;
        }

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }
    }
    public bool HasEffect(PlayerEffect effect) => (activeEffects & effect) == effect;
    private PlayerEffect GetHighestPriorityEffect()
    {
        PlayerEffect highestEffect = PlayerEffect.None;
        int highestPriority = -1;

        foreach (PlayerEffect effect in System.Enum.GetValues(typeof(PlayerEffect)))
        {
            if ((activeEffects & effect) == effect && effectPriority.TryGetValue(effect, out int priority))
            {
                if (priority > highestPriority)
                {
                    highestPriority = priority;
                    highestEffect = effect;
                }
            }
        }

        return highestEffect;
    }
    public void UpdateEffects()
    {
        if (HasEffect(PlayerEffect.Running))
             player.moveSpeed = originalSpeed * player.runningSpeedMultiplier;
        else
            player.moveSpeed = originalSpeed;

        if (HasEffect(PlayerEffect.Stunning))
        {
            player.transform.Rotate(Vector3.up * rotateStrength * Time.deltaTime);
            if (!audioSource.isPlaying) audioSource.Play();
        }
            
        if (HasEffect(PlayerEffect.Hit))
            HandleHitEffect();
        if (HasEffect(PlayerEffect.Photo))
        {
            player.photographer.CameraOpen(true);
        }
    }
    private void HandleEffectStart(PlayerEffect effect)
    {
        switch (effect)
        {
            case PlayerEffect.Stunning:
                player.StartCoroutine(StunCoroutine());
                break;
            case PlayerEffect.Hit:
                break;
            case PlayerEffect.Photo:
                player.photographer.CameraOpen(true);
                player.PhotoCamera.SetActive(true);
                break;
        }
    }
    private void HandleEffectEnd(PlayerEffect effect)
    {
        switch (effect)
        {
            case PlayerEffect.Hit:
                break;
            case PlayerEffect.Photo:
                player.photographer.CameraOpen(false);
                player.PhotoCamera.SetActive(false);
                break;
        }
    }
    private System.Collections.IEnumerator StunCoroutine()
    {
        yield return new WaitForSeconds(player.stunDuration);
        RemoveEffect(PlayerEffect.Stunning);
    }

    private void HandleHitEffect()
    {
        // �������������� ������ ������� ���������
    }
}
