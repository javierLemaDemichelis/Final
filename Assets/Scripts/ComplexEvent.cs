using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexEvent : MonoBehaviour
{
    [SerializeField]
    AdventureEvents historyEvents;
    [SerializeField]
    AdventureEvents fillerEvents;
    [SerializeField]
    int minFillerEventBetweenHistoryEvents = 5;
    [SerializeField]
    int MaxFillerEventBetweenHistoryEvents = 10;
    [SerializeField]
    int finalNumberOfFillerEvents = 0;
    [SerializeField]
    bool complexEventFinalized=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
