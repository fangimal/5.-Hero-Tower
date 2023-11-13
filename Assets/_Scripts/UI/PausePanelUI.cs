using System;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelUI : MonoBehaviour
{
    [SerializeField] private Button nextPointButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backButton;

    public event Action OnNextPoint;
    public event Action OnContinue;
    public event Action OnBack;
    private void Awake()
    {
        nextPointButton.onClick.AddListener(() =>
        {
            OnNextPoint?.Invoke();
        });
        
        continueButton.onClick.AddListener(() =>
        {
            OnContinue?.Invoke();
        });
        
        backButton.onClick.AddListener(() =>
        {
            OnBack?.Invoke();
        });
    }

    public void SetInteractableNextPointButton(bool isInteractable)
    {
        nextPointButton.interactable = isInteractable;
    }
}
