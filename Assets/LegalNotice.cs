using UnityEngine;

public class LegalNotice : MonoBehaviour {

    public void OpenLegalNoticeWebsite() => Application.OpenURL(Constants.legalNoticeURL);
}
