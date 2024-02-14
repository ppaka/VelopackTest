using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Velopack;

public class Test : MonoBehaviour
{
    private UpdateManager _mgr;
    public TMP_Text stateText;
    public TMP_Text versionText;
    private const string TextBase = "Current State: ";
    private UpdateInfo _newVersion;

    public Button checkBtn, downloadBtn, applyBtn;
    
    private void Awake()
    {
        VelopackApp.Build().Run();
    }

    private void Start()
    {
        Debug.Log(Application.version);
        versionText.SetText(Application.version);
        _mgr = new UpdateManager("https://github.com/ppaka/VelopackTest/releases/latest");
        downloadBtn.gameObject.SetActive(false);
        applyBtn.gameObject.SetActive(false);
        checkBtn.onClick.AddListener(OnClickCheckUpdate);
        downloadBtn.onClick.AddListener(OnClickDownloadUpdate);
        applyBtn.onClick.AddListener(OnClickApplyUpdate);
    }

    private async void OnClickCheckUpdate()
    {
        checkBtn.gameObject.SetActive(false);
        
        Debug.Log("Checking");
        stateText.SetText(TextBase+"Checking");
        _newVersion = await _mgr.CheckForUpdatesAsync();
        if (_newVersion == null)
        {
            Debug.Log("No Update available");
            stateText.SetText(TextBase+"No Update available!");
            checkBtn.gameObject.SetActive(true);
            return;
        }
        
        downloadBtn.gameObject.SetActive(true);
    }

    private async void OnClickDownloadUpdate()
    {
        downloadBtn.gameObject.SetActive(false);
        Debug.Log("Downloading");
        stateText.SetText(TextBase+"Downloading");
        await _mgr.DownloadUpdatesAsync(_newVersion, i =>
        {
            stateText.SetText(TextBase+$"Download:{i}%");
        });
        
        applyBtn.gameObject.SetActive(true);
    }
    
    private void OnClickApplyUpdate()
    {
        // install new version and restart app
        Debug.Log("Applying");
        _mgr.ApplyUpdatesAndRestart(_newVersion);
    }
}
