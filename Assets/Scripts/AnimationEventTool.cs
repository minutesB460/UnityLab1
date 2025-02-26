using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTool : MonoBehaviour
{
    // public UnityEvent use;
    [SerializeField] private MagicMushroomPowerup powerup; // Reference to the powerup script

    public void Use()
    {
        Debug.Log("powerup");

        if (powerup != null)
        {
            powerup.SpawnPowerup();
        }
        else
        {
            Debug.LogWarning("Powerup reference is missing in AnimationEventTool.");
        }
    }
}
