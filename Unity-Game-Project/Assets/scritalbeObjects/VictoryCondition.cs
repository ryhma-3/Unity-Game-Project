using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VictoryCondition : ScriptableObject, ISerializationCallbackReceiver
{
    public bool BossDead;

    [HideInInspector]
    public bool BossRuntimeValue;


    public void OnAfterDeserialize()
    {
        BossRuntimeValue = BossDead;

    }
    public void OnBeforeSerialize()
    {

    }
}
