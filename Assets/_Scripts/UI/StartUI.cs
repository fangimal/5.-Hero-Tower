using _Scripts.Infrastructure.States;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : WindowBase
{
    [SerializeField] private Button newGame;

    private void Awake()
    {
        newGame.onClick.AddListener(LodLevel);
    }

    private void OnDestroy()
    {
        newGame.onClick.RemoveAllListeners();
    }

    private void LodLevel()
    {
        gameStateMachine.Enter<LoadSceneState, int>(2);
    }
}
