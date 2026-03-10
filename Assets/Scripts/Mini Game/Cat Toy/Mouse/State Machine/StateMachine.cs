using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateEntry
{
    public Condition condition;
    public State state;
}

public class StateMachine : MonoBehaviour
{
    [SerializeField] private List<StateEntry> stateEntrys = new List<StateEntry>();

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < stateEntrys.Count; i++)
        {
            StateEntry entry = stateEntrys[i];
            if (entry.condition.CheckCondition())
            {
                entry.state.enabled = true;
            }
            else 
                entry.state.enabled = false;
        }
    }
}
