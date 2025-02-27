using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Header("Referencing")]
    public List<Condition> conditionList;
    public List<State> stateList;

    private void Update()
    {
        for (int i = 0; i < conditionList.Count; i++)
        {
            if (conditionList[i].ConditionCheck() == true)
            {
                stateList[i].enabled = true;
            }
            else
            {
                stateList[i].enabled = false;
            }
        }
    }
}
