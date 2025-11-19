using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SUIManager : MonoBehaviour
{
    [Header("UI Buttons References")]
    private Button[] mTweeningButtons;
    private void Start()
    {
        mTweeningButtons = FindObjectsByType<Button>(FindObjectsSortMode.None); //finding all buttons in scene
        SetupButtons();
        if(mTweeningButtons.Length == 0)
        {
            Debug.Log("No buttons in the list");
        }
    }
    #region ButtonTweening
    private void SetupButtons() //setting up and adding listeners to all buttons in scene
    {
        Debug.Log("This is being called outside the foreach loop so no listeners are being added");
        foreach (Button buttons in mTweeningButtons)
        {
            Debug.Log("Add listeners are being added");
            buttons.onClick.AddListener(() => StartCoroutine(TweeningButton(buttons, new Vector3(0.9f, 0.9f, 0.9f), 0.1f)));
        } 
    }
    private IEnumerator TweeningButton(Button button, Vector3 targetScale, float duration) //tweening the button using local scale
    {
        Vector3 startscale = button.transform.localScale;
        float elapsed = 0;
        while (elapsed < duration) //lerping local scale to give smooth feeling
        {
            button.interactable = false;
            elapsed += Time.deltaTime;
            button.transform.localScale = Vector3.Lerp(startscale, targetScale, elapsed / duration);
            yield return null;
        }
        elapsed = 0;
        while (elapsed < duration) //lerping local scale to give smooth feeling
        {
            elapsed += Time.deltaTime;
            button.transform.localScale = Vector3.Lerp(targetScale, startscale, elapsed / duration);
            yield return null;
        }
        button.interactable = true;
    }
    #endregion
}
