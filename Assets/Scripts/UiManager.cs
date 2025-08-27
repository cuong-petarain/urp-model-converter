using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private VoidEventHandlerSO _onGameStarted;

    public void OnButtonPlayClicked()
    {
        _onGameStarted.RaiseEvent();
    }
}
