using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public void WinGame() => MazePhaseManager.Instance.WinGame();
}
