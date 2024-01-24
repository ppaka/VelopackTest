using System.Threading.Tasks;
using UnityEngine;
using Velopack;

public class Test : MonoBehaviour
{
    private void Start()
    {
        VelopackApp.Build().Run();
        Debug.Log(Application.version);
        Task.Run(UpdateMyApp);
    }
    
    private static async Task UpdateMyApp()
    {
        var mgr = new UpdateManager("https://github.com/ppaka/VelopackTest/releases/latest");

        // check for new version
        Debug.Log("Checking");
        var newVersion = await mgr.CheckForUpdatesAsync();
        if (newVersion == null)
        {
            Debug.Log("No Update available");
            return; // no update available
        }

        // download new version
        Debug.Log("Downloading");
        await mgr.DownloadUpdatesAsync(newVersion);

        // install new version and restart app
        Debug.Log("Restarting");
        mgr.ApplyUpdatesAndRestart();
    }
}
