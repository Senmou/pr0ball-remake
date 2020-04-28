using System.Text.RegularExpressions;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

public class HighscoreController : MonoBehaviour {

    public GameObject loadingIndicator;

    private static int nameIndex = 1;
    private UnityWebRequest fetchScoresWebRequest;

    private void Awake() {
        loadingIndicator.SetActive(false);
    }

    public void UploadHighscore(long score) {

        string playerName = PersistentData.instance.playerName;

        Regex rgx = new Regex("[^a-zA-Z0-9]");
        playerName = rgx.Replace(playerName, "");

        if (string.IsNullOrEmpty(playerName) || string.IsNullOrWhiteSpace(playerName))
            playerName = "anonymous";

        StartCoroutine(CheckIfTop20(playerName, score));
    }

    private IEnumerator CheckIfTop20(string name, long score) {
        string hash = Helper.Md5Sum(name + score + Constants.secretKey);

        string url = Constants.checkNewHighscore + "name=" + UnityWebRequest.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

        using (UnityWebRequest www = UnityWebRequest.Get(url)) {

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log("ERROR: " + www.error);
            } else {
                string result = www.downloadHandler.text;

                if (result.Equals("true")) {
                    StartCoroutine(PostScores(name, score));
                } else
                    Debug.Log(result);
            }
        }
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

        LeanTween.rotateAround(loadingIndicator, Vector3.forward, -360f, 0.5f)
          .setOnStart(() => loadingIndicator.SetActive(true))
          .setEase(LeanTweenType.linear)
          .setIgnoreTimeScale(true)
          .setLoopClamp();

        fetchScoresWebRequest = UnityWebRequest.Get(Constants.fetchScoresURL);

        using (fetchScoresWebRequest) {

            fetchScoresWebRequest.SendWebRequest();

            float timer = 0f;
            float retryTimer = 5f;

            while (!fetchScoresWebRequest.isDone || fetchScoresWebRequest.isNetworkError || fetchScoresWebRequest.isHttpError) {
                if (timer > retryTimer) {
                    timer = 0f;
                    fetchScoresWebRequest.Abort();
                    fetchScoresWebRequest.Dispose();
                    fetchScoresWebRequest = UnityWebRequest.Get(Constants.fetchScoresURL);
                    fetchScoresWebRequest.SendWebRequest();
                } else
                    timer += Time.unscaledDeltaTime;
                yield return null;
            }

            string result = fetchScoresWebRequest.downloadHandler.text;

            string[] entries = result.Split('_');

            GlobalHighscoreTable.GlobalHighscoreEntry[] globalEntries = new GlobalHighscoreTable.GlobalHighscoreEntry[entries.Length];

            for (int i = 0; i < entries.Length; i++) {
                GlobalHighscoreTable.GlobalHighscoreEntry entry = new GlobalHighscoreTable.GlobalHighscoreEntry();
                string[] entryTriple = entries[i].Split('-');

                if (entryTriple.Length < 3) {
                    Debug.LogWarning("No valid entries!");
                    break;
                }

                entry.playerName = entryTriple[0];
                entry.score = int.Parse(entryTriple[1]);
                entry.timestamp = entryTriple[2];

                globalEntries[i] = entry;
            }

            loadingIndicator.SetActive(false);
            OnSuccess?.Invoke(globalEntries);
        }
    }
}