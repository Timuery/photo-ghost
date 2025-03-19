using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HitEffectController : MonoBehaviour
{
    [SerializeField] private Volume postProcessVolume; 
    private ColorAdjustments colorAdjustments; // вроде цвет меняет
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); 

        if (postProcessVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.colorFilter.value = default; // дефолт цвет
        }
    }

    void Update()
    {

        ApplyHitEffect();
        if (playerController != null)
        {
            if (playerController.effectController.HasEffect(PlayerEffect.Hit))
            {
                ApplyHitEffect();
            }
            else
            {
                RemoveHitEffect();
            }
        }
    }

    private void ApplyHitEffect()
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.colorFilter.value = new Color(1f, 0.3f, 0.3f); // красный
        }
    }

    private void RemoveHitEffect()
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.colorFilter.value = default; 
        }
    }
}

