using UnityEngine;

/// <summary>
/// Состояния игрока
/// </summary>
public class EffectController
{
    private PlayerEffect activeEffects;
    private PlayerController player;
    private float originalSpeed;

    [SerializeField] private float rotateStrenght = 10f;

    public EffectController(PlayerController player)
    {
        this.player = player;
        originalSpeed = player.moveSpeed;
    }

    public void AddEffect(PlayerEffect effect)
    {
        activeEffects |= effect;
        HandleEffectStart(effect);
    }

    public void RemoveEffect(PlayerEffect effect)
    {
        activeEffects &= ~effect;
        HandleEffectEnd(effect);
    }

    public bool HasEffect(PlayerEffect effect)
    {
        return (activeEffects & effect) == effect;
    }

    public void UpdateEffects()
    {

        if (HasEffect(PlayerEffect.Walk))
            player.moveSpeed = originalSpeed;

        if (HasEffect(PlayerEffect.Running))
            player.moveSpeed = originalSpeed * player.runningSpeedMultiplier;
        
        if (HasEffect(PlayerEffect.Stunning))
            player.transform.Rotate(Vector3.up * rotateStrenght * Time.deltaTime);
        if (HasEffect(PlayerEffect.Hit))
            HandleHitEffect();
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
                player._mainController.UIcontroller.ToggleCameraUI(true);
                break;
        }
    }

    private void HandleEffectEnd(PlayerEffect effect)
    {
        switch (effect)
        {
            case PlayerEffect.Hit:
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
