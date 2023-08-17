using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_ToggleHighlight : MonoBehaviour
{
    public Image highlightImage;
    private bool isHighlighted = false;

    void Start() {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Toggle);
    }

    void Toggle() {
        isHighlighted = !isHighlighted;
        highlightImage.enabled = isHighlighted;
    }
}
