using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

public class HighscoreController : MonoBehaviour {

    private static int nameIndex = 1;

    public void PostRandomScore() {

        string playerName = "name" + nameIndex++;
        long score = Random.Range(0, 100000);

        StartCoroutine(PostScores(playerName, score));
    }

    public void ShowGlobalHighscores(System.Action<GlobalHighscoreTable.GlobalHighscoreEntry[]> OnSuccess) {
        StartCoroutine(FetchHighscores(OnSuccess));
    }

    private IEnumerator PostScores(string name, long score) {

        string hash = Helper.Md5Sum(name + score + Constants.secretKey);

        string url = Constants.addScoreURL + "name=" + UnityWebRequest.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

        using (UnityWebRequest www = UnityWebRequest.Get(url)) {

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log("ERROR: " + www.error);
            } else {
                string result = www.downloadHandler.text;
                Debug.Log(result);
            }
        }
    }

    private IEnumerator FetchHighscores(System.Action<GlobalHighscoreTable.GlobalHighscoreEntry[]> OnSuccess) {

        using (UnityWebRequest www = UnityWebRequest.Get(Constants.fetchScoresURL)) {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else {
                string result = www.downloadHandler.text;

                string[] entries = result.Split('_');

                GlobalHighscoreTable.GlobalHighscoreEntry[] globalEntries = new GlobalHighscoreTable.GlobalHighscoreEntry[entries.Length];

                for (int i = 0; i < entries.Length; i++) {
                    GlobalHighscoreTable.GlobalHighscoreEntry entry = new GlobalHighscoreTable.GlobalHighscoreEntry();
                    string[] entryTriple = entries[i].Split('-');

                    //Debug.Log(entryTriple[i]);

                    entry.playerName = entryTriple[0];
                    entry.score = int.Parse(entryTriple[1]);
                    entry.timestamp = entryTriple[2];

                    globalEntries[i] = entry;
                }

                OnSuccess?.Invoke(globalEntries);
            }
        }
    }
}