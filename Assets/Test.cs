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
        var mgr = new UpdateManager("https://the.place/you-host/updates");

        // check for new version
        var newVersion = await mgr.CheckForUpdatesAsync();
        if (newVersion == null)
        {
            Debug.Log("No Update available");
            return; // no update available
        }

        // download new version
        await mgr.DownloadUpdatesAsync(newVersion);

        // install new version and restart app
        mgr.ApplyUpdatesAndRestart();
    }
}
