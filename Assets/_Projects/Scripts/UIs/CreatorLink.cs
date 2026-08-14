using UnityEngine;

public class CreatorLink : MonoBehaviour
{
    [SerializeField] private string url;

    public void OpenByURL()
    {
        Application.OpenURL(url);
    }    
}
