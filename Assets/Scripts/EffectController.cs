using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// Эффекты не трогаем
[Flags]

public enum PlayerEffect
{
    None = 0,           // когда игрок стоит, 0
    Walk = 1 << 0,      // Когда игрок ходит 1
    Hit = 1 << 1,  // Когда игрок застанен 2
    Stunning = 1 << 2,       // Когда игрок получает урон 4
    Photo = 1 << 3      // Когда игрок находится в режиме фотографии 8
}

public enum Status
{

}


// Эффекты не трогаем

/// <summary>
/// Состояния игрока
/// </summary>
public class EffectController: MonoBehaviour
{

    private PlayerController playerController;
    // Аудиоклипы для эффектов (настраиваем в инспекторе)
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip runSound;
    [SerializeField] private AudioClip stunSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip photoSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }

    public List<PlayerEffect> NowStatus { get; private set; } = new List<PlayerEffect>();
    private PlayerEffect currentStatus = PlayerEffect.None;
    private AudioSource audioSource;
    // Добавляем эффект и обновляем текущий статус
    public void AddEffect(PlayerEffect effect)
    {
        if (!NowStatus.Contains(effect))
        {
            NowStatus.Add(effect);
            ReCheck();
            PlayEffectSound(effect);
        }
    }
    // Удаляем эффект и обновляем статус
    public void RemoveEffect(PlayerEffect effect)
    {
        if (NowStatus.Contains(effect))
        {
            EndEffect(effect);
            NowStatus.Remove(effect);
            ReCheck();
        }
    }

    // Определяем самый важный эффект (по максимальному значению в enum)
    private void ReCheck()
    {
        if (NowStatus.Count == 0)
        {
            currentStatus = PlayerEffect.None;
            return;
        }

        // Выбираем эффект с максимальным значением флага
        currentStatus = NowStatus.OrderByDescending(e => e).First();
        IniEffect(currentStatus);
    }

    // Возвращаем текущий статус
    public PlayerEffect GetEffect() => currentStatus;

    // Проигрываем звук эффекта
    private void PlayEffectSound(PlayerEffect effect)
    {
        if (audioSource == null) return;

        switch (effect)
        {
            case PlayerEffect.Walk:
                if (walkSound != null) audioSource.PlayOneShot(walkSound);
                break;
            case PlayerEffect.Stunning:
                if (stunSound != null) audioSource.PlayOneShot(stunSound);
                break;
            case PlayerEffect.Hit:
                if (hitSound != null) audioSource.PlayOneShot(hitSound);
                break;
            case PlayerEffect.Photo:
                if (photoSound != null) audioSource.PlayOneShot(photoSound);
                break;
        }
    }

    private void IniEffect(PlayerEffect effect)
    {
        if (effect == PlayerEffect.Photo)
        {
            playerController.photographer.CameraOpen(true);
            playerController.PhotoCamera.SetActive(true);
        }
    }

    private void EndEffect(PlayerEffect effect)
    {
        if (effect == PlayerEffect.Photo)
        {
            playerController.photographer.CameraOpen(false);
            playerController.PhotoCamera.SetActive(false);
        }
    }
}