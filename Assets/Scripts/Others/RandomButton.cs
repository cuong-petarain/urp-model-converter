using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomButton : MonoBehaviour
{
    public void OnButtonClicked()
    {
        Application.ExternalCall("openStore");
    }
}
