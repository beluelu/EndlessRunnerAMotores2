using UnityEngine;

public class AnimationBridge : MonoBehaviour
{
    private PlayerAnimation playerAnim;

    void Start()
    {
        playerAnim = GetComponentInParent<PlayerAnimation>();
    }

    
    public void EndRoll()
    {
        if (playerAnim != null)
        {
            playerAnim.EndRoll();
        }
    }
}
