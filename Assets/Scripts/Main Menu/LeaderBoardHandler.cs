using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardHandler : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject leaderBoard;

    public void ToggleLeaderBoard()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        leaderBoard.SetActive(!leaderBoard.activeSelf);
    }
}
