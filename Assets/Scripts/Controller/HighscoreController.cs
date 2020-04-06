using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

public class HighscoreController : MonoBehaviour {

    private string secretKey = "secretKey";
    private string addScoreURL = "https://senmou.bplaced.net/addscore.php?";
    private string fetchScoresURL = "https://senmou.bplaced.net/fetchScores.php";

    private static int nameIndex = 1;

    public void PostRandomScore() {

        string playerName = "name" + nameIndex++;
        long score = Random.Range(0, 100000);

        StartCoroutine(PostScores(playerName, score));
    }

    public void GetHighscores() {
        StartCoroutine(FetchHighscores());
    }

    private IEnumerator PostScores(string name, long score) {

        string hash = Helper.Md5Sum(name + score + secretKey);

        string url = addScoreURL + "name=" + UnityWebRequest.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

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

    private IEnumerator FetchHighscores(System.Action OnSuccess) {

        using (UnityWebRequest www = UnityWebRequest.Get(fetchScoresURL)) {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else {
                string result = www.downloadHandler.text;

                string[] entries = result.Split(' ');

                for (int i = 0; i < entries.Length; i++) {
                    string[] entryTouple = entries[i].Split('-');

                    string playerName = entryTouple[0];
                    int playerScore = int.Parse(entryTouple[1]);

                    OnSuccess?.Invoke();
                }
            }
        }
    }
}