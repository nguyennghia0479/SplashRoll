using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TMP_Text pcGuideText;
    [SerializeField] private TMP_Text mobileGuideText;

    private void Awake()
    {
        mobileGuideText.gameObject.SetActive(false);
        pcGuideText.gameObject.SetActive(true);
      
#if UNITY_ANDROID
        mobileGuideText.gameObject.SetActive(true);
        pcGuideText.gameObject.SetActive(false);
#endif

       
    }
}
