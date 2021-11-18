using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

// This class encapsulates all of the metrics that need to be tracked in your game. These may range
// from number of deaths, number of times the player uses a particular mechanic, or the total time
// spent in a level. These are unique to your game and need to be tailored specifically to the data
// you would like to collect. The examples below are just meant to illustrate one way to interact
// with this script and save data.
public class MetricManager : MonoBehaviour
{
    // You'll have more interesting metrics, and they will be better named.
    static int playerDeathNumber;
    static int fireHookNumber;
    private float starttime;

    public static int[] deathDistribution = new int[10];
    public static int[] hookDistribution = new int[10];

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        starttime = Time.time;
    }


    // Public method to add to Metric 1.
    static public void AddToMetric1(int valueToAdd)
    {
        playerDeathNumber += valueToAdd;
    }

    /*
    public void AddToThisSceneDeath(int value)
    {
        this_scene_death += value;
    }*/

    // Public method to add to Metric 2.
    static public void AddToMetric2(int valueToAdd)
    {
        fireHookNumber += valueToAdd;
    }

    public static void AddToDeathDis(int scene)
    {
        deathDistribution[scene]++;
    }

    public static void AddToHookDis(int scene)
    {
        hookDistribution[scene]++;
    }

    // Converts all metrics tracked in this script to their string representation
    // so they look correct when printing to a file.
    private string ConvertMetricsToStringRepresentation()
    {
        string metrics = "Here are my metrics:\n";
        metrics += "Total Player Death: " + playerDeathNumber.ToString() + "\n";
        metrics += "Total Hooks Fired: " + fireHookNumber.ToString() + "\n";

        for(int i = 0; i < deathDistribution.Length; i++)
        {
            metrics += "Scene " + i + " dealth: " + deathDistribution[i] + "\n";
        }

        for (int j = 0; j < hookDistribution.Length; j++)
        {
            metrics += "Scene " + j + " fire number: " + hookDistribution[j] + "\n";
        }

        return metrics;
    }

    // Uses the current date/time on this computer to create a uniquely named file,
    // preventing files from colliding and overwriting data.
    private string CreateUniqueFileName()
    {
        string dateTime = System.DateTime.Now.ToString();
        //string sceneNumber = SceneManager.GetActiveScene().buildIndex.ToString();
        dateTime = dateTime.Replace("/", "_");
        dateTime = dateTime.Replace(":", "_");
        dateTime = dateTime.Replace(" ", "___");
        return "TowerOfBabel_metrics_" + dateTime +  ".txt";
    }

    // Generate the report that will be saved out to a file.
    private void WriteMetricsToFile() 
    {
        string totalReport = "Report generated on " + System.DateTime.Now + "\n\n";
        totalReport += "Total Report:\n";
        totalReport += ConvertMetricsToStringRepresentation();
        float temp = Time.time;
        float duration = temp - starttime;
        totalReport += "Duration: \n" + duration;
        totalReport = totalReport.Replace("\n", System.Environment.NewLine);
        string reportFile = CreateUniqueFileName();

#if !UNITY_WEBPLAYER
        File.WriteAllText(reportFile, totalReport);
#endif
    }

    // The OnApplicationQuit function is a Unity-Specific function that gets
    // called right before your application actually exits. You can use this
    // to save information for the next time the game starts, or in our case
    // write the metrics out to a file.
    private void OnApplicationQuit()
    {
        
        WriteMetricsToFile();
    }
}