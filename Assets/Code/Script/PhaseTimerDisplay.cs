using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhaseTimerDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerTMP;
    void Update()
    {
        _timerTMP.text = "Time remaining : " + (int)GameManager.s_Instance.LaserPhaseTimer;
    }
}
