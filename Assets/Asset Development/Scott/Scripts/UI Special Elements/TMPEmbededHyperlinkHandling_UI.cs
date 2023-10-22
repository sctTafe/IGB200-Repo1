using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class TMPEmbededHyperlinkHandling_UI: MonoBehaviour, IPointerClickHandler 
{
    public UnityEvent<string> OnLinkClicked;
    public bool _isInWindowsMode = true;

    private TMP_Text tmpText;

    private void Awake() {
        tmpText = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(tmpText, eventData.position, null);
        if (linkIndex != -1) {
            TMP_LinkInfo linkInfo = tmpText.textInfo.linkInfo[linkIndex];
            OnLinkClicked.Invoke(linkInfo.GetLinkID());
            if (_isInWindowsMode)
                OpenLink(linkInfo.GetLinkID());
        }
    }

    private void OpenLink(string url) {
        Application.OpenURL(url);
    }

}