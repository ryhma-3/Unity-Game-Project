using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{

    private WinScreen win;
    public VictoryCondition condition1;
    public VictoryCondition condition2;
    public VictoryCondition condition3;
    public VictoryCondition condition4;

    // Start is called before the first frame update
    void Start()
    {
        win = GameObject.FindWithTag("UI").GetComponent<WinScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        if (condition1.BossRuntimeValue && condition2.BossRuntimeValue && condition3.BossRuntimeValue && condition4.BossRuntimeValue)
        {
            win.ActivateVictory();
        }
    }
}
