using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuChanger: MonoBehaviour
{
  
  [SerializeField] private TextMeshPro btnText;

  [SerializeField] private GameObject topBtn;

  [SerializeField] private GameObject bottomBtn;

  [SerializeField] private GameObject[] textBoxes;

  private int textBoxIndex = 0;

  public void ShowMenu() => gameObject.SetActive(true);
    
  public void CycleTextBoxNext() => CycleTextBox(1);

  public void CycleTextBoxPrev() => CycleTextBox(-1);

  private void CycleTextBox(int cycleAmount)
  {
    textBoxes[textBoxIndex].SetActive(false);
    textBoxIndex += cycleAmount;
    if(textBoxIndex < 0) textBoxIndex = 0;
    else if(textBoxIndex >= textBoxes.Length)
    {
      textBoxIndex = 0;
      gameObject.SetActive(false);
    }
    if(textBoxIndex == 0) bottomBtn.SetActive(false);
    else bottomBtn.SetActive(true);
    if(textBoxIndex == textBoxes.Length - 1) topBtn.SetActive(false);
    else topBtn.SetActive(true);
    textBoxes[textBoxIndex].SetActive(true);
  }

}