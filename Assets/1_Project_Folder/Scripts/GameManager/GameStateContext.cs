using CustomInspector;
using UnityEngine;

[System.Serializable]
public class GameStateContext
{
    public GameStateMachine stateMachine { get; set; }

    [SerializeField] public WaitNextState waitNextState;
    [HorizontalLine("MAIN MENU CONFIG", 1, FixedColor.CloudWhite)]
    [SerializeField] public bool isStartGame;

    [HorizontalLine("GAME PLAY CONFIG", 1, FixedColor.CloudWhite)]
    [SerializeField] public bool isStartDraw;

    [SerializeField] public bool isStartResult;
    [SerializeField] public bool isPass;

    [SerializeField] public bool isStartSetup;

    [HorizontalLine("GAME END", 1, FixedColor.CloudWhite)]
    [SerializeField] public ResultEndGame resultEndGame;
    [SerializeField] public bool isStartEndGame;
    [SerializeField] public bool isRestartGame;
}
