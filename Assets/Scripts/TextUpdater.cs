using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshPro qrText; // Reference to the TextMeshPro component

    /// <summary>
    /// Updates the text displayed in the TextMeshPro component.
    /// </summary>
    /// <param name="newText">The new text to display.</param>
    public void UpdateText(string newText)
    {
        if (qrText == null)
        {
            Debug.LogError("TextMeshPro reference is not assigned!");
            return;
        }

        qrText.text = newText;
        Debug.Log($"QR Text Updated: {newText}");
    }
}
