using CustomInspector;
using UnityEngine;

[System.Serializable]
public class GameStateContext
{
    public GameStateMachine stateMachine { get; set; }

    [HorizontalLine("MAIN MENU CONFIG", 1, FixedColor.CloudWhite)]
    [SerializeField] public bool isStartGame;

    [HorizontalLine("GAME PLAY CONFIG", 1, FixedColor.CloudWhite)]
    [SerializeField] public bool isPauseGame;
    [SerializeField] public bool isWaveEnd;
    [SerializeField] public bool isGameEnd;
    [SerializeField] public bool isReWave;

    [HorizontalLine("GAME PAUSE CONFIG", 1, FixedColor.CloudWhite)]
    [SerializeField] public bool isContinueGame;

    [HorizontalLine("GAME UPGRADE CONFIG", 1, FixedColor.CloudWhite)]
    [SerializeField] public bool isSelectedUppgrade;

    [HorizontalLine("GAME END CONFIG", 1, FixedColor.CloudWhite)]
    [SerializeField] public bool isRestartGame;
}
