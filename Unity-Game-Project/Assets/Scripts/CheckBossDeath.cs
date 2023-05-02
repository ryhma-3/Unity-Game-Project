using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossDeath : MonoBehaviour
{
    public VictoryCondition BossDeath;

    private void OnDisable()
    {
        if (!this.gameObject.scene.isLoaded) return;
        BossDeath.BossRuntimeValue = true;
    }
}
