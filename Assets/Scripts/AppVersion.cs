using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AppVersion : MonoBehaviour
{
    private TextMeshProUGUI verText;
    private void Awake()
    {
        verText = GetComponent<TextMeshProUGUI>();
        verText.text = "v"+Application.version;
    }
}
