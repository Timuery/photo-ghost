using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    public PlayerController playerController;
 
    public void StunPlayer()
    {
        playerController.ApplyEffect(PlayerEffect.Stunning);
    }
    public void HitPlayer()
    {
        playerController.ApplyEffect(PlayerEffect.Hit);
    }

}
