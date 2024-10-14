using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using TMPro;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    private static readonly HttpClient client = new HttpClient();
    private string baseUrl = "http://10.20.0.92:8000/";
    public AnimationManager AnimationManager;
    public GameObject ResultValueObj;
    string ResultValue;

    void Update()
    {
        ResultValueObj.GetComponent<TextMeshPro>().text = ResultValue;
    }
    // Asenkron POST fonksiyonu
    //El animasyonlarý için
    //Play tuþuna basýldýðýnda çalýþacak fonksiyon
    public async Task<string> PostToAPI(string sessionId, int decibel, string earSide, int frequency, string message, int patientId, string testType)
    {
        string url = baseUrl + "pretend/" + sessionId;

        ApiRequest postData = new ApiRequest
        {
            patient_id = patientId,
            decibel = decibel,
            frequency = frequency,
            ear_side = earSide,
            test_type = testType,
            message = message
        };

        var json = JsonConvert.SerializeObject(postData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");


        try
        {
            HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                UnityEngine.Debug.Log("API response: " + responseBody);
                return responseBody;
            }
            else
            {
                UnityEngine.Debug.LogError("API POST request failed with status code: " + response.StatusCode);
                return null;
            }
        }
        catch (HttpRequestException e)
        {
            UnityEngine.Debug.LogError("Request exception: " + e.Message);
            return null;
        }
    }

    // Asenkron PUT fonksiyonu
    // API ye gönderilecek data iþlemlerinde kullanýlýyor
    //Odyogram üzerinde bir frekans ve desibel deðerine iþaret konulduðu vakit API ye o noktanýn deðerlerini gönderiyor
    public async Task<bool> PutAudiogram(string sessionId, int decibel, string earSide, int frequency, string testType)
    {
        string url = baseUrl + "audiogram/" + sessionId;

        AudiogramRequest putData = new AudiogramRequest
        {
            decibel = decibel,
            ear_side = earSide,
            frequency = frequency,
            test_type = testType
        };

        var json = JsonConvert.SerializeObject(putData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                UnityEngine.Debug.Log("Audiogram data updated successfully.");
                return true;
            }
            else
            {
                UnityEngine.Debug.LogError("API PUT request failed with status code: " + response.StatusCode);
                return false;
            }
        }
        catch (HttpRequestException e)
        {
            UnityEngine.Debug.LogError("Request exception: " + e.Message);
            return false;
        }
    }

    // Asenkron GET fonksiyonu
    //API üzerinden iþaret konulan noktalarýn deðerlerini döndür.
    public async Task<string> GetAudiogram(string sessionId)
    {
        string url = baseUrl + "audiogram/" + sessionId;

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                UnityEngine.Debug.Log("Audiogram data: " + responseBody);
                return responseBody;
            }
            else
            {
                UnityEngine.Debug.LogError("API GET request failed with status code: " + response.StatusCode);
                return null;
            }
        }
        catch (HttpRequestException e)
        {
            UnityEngine.Debug.LogError("Request exception: " + e.Message);
            return null;
        }
    }

    // API ye gönderilen odyogram tablosu sonucu LLM çýktýsýný döndür
    public async Task<string> PostTeach(int sessionId, int patientId)
    {
        string url = baseUrl + "teach/" + sessionId;

        var postData = new
        {
            patient_id = patientId
        };

        // JSON formatýna serileþtirme
        var json = JsonConvert.SerializeObject(postData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // POST isteði gönderme
        HttpResponseMessage response = await client.PostAsync(url, content);

        // Baþarý durumunu kontrol et
        response.EnsureSuccessStatusCode();

        // Yanýtý okuma
        string responseBody = await response.Content.ReadAsStringAsync();
        return responseBody;
    }


    // API isteði modeli
    public class ApiRequest
    {
        public int decibel { get; set; }
        public string ear_side { get; set; }
        public int frequency { get; set; }
        public string message { get; set; }
        public int patient_id { get; set; }
        public string test_type { get; set; }
    }

    // Audiogram PUT isteði modeli
    public class AudiogramRequest
    {
        public int decibel { get; set; }
        public string ear_side { get; set; }
        public int frequency { get; set; }
        public string test_type { get; set; }
    }

    //Bu fonksiyon Play tuþu içine atýlacak
    // API'ye POST isteði gönder ve animasyonu tetikle

    public async void PostAndTriggerAnimationAsync(string sessionId, int decibel, string earSide, int frequency, string testType)
    {
        // Asenkron API çaðrýsýný baþlat
        var apiResponse = await PostToAPI(sessionId, decibel, earSide, frequency, "", 1, testType);

        // Gelen cevaba göre animasyonu tetikle
        if (apiResponse.Contains("\"did_fake\":true"))
        {
            AnimationManager.HandSlow();
        }
        else if (apiResponse.Contains("\"did_hear\":true"))
        {
            AnimationManager.HandUp();
        }
    }
    public IEnumerator HandlePutRequest(string sessionId, int decibel, string earSide, int frequency, string testType)
    {
        Task<bool> putTask = PutAudiogram(sessionId, decibel, earSide, frequency, testType);
        yield return new WaitUntil(() => putTask.IsCompleted);

        if (putTask.Result)
        {
            UnityEngine.Debug.Log("PUT request completed successfully.");
        }
        else
        {
            UnityEngine.Debug.Log("PUT request failed.");
        }
    }

    // GET isteði için Coroutine
    public IEnumerator HandleGetRequest(string sessionId)
    {
        Task<string> getTask = GetAudiogram(sessionId);
        yield return new WaitUntil(() => getTask.IsCompleted);

        UnityEngine.Debug.Log("GET response: " + getTask.Result);
    }

    public IEnumerator CallPostTeach()
    {

        // PostTeach metodunu çaðýrma
        Task<string> responseTask = PostTeach(1, 1);
        yield return new WaitUntil(() => responseTask.IsCompleted);

        print(responseTask.Result);
        // Sonucu logla
        UnityEngine.Debug.Log("API Response: " + responseTask.Result);
        ResultValue = responseTask.Result;
        //string soundText = ResultValue;
        string soundText = "Bu bir test mesajýdýr. Her ne kadar bu mesaj hazýr olsa da anlýk olarak yapay zeka sistemine gönderilip tekrardan seslendirilmektedir. Projenin ilerleyen süreçlerinde bu kýsýmda önünüzde görmüþ olduðunuz sonuç yazýsýný duyacaksýnýz.";
        //string soundText ="As your teacher, I'd like to guide you on how to interpret the patient's data.Firstly, it seems like you're trying to report a blank or unknown audiogram for both ears. However, in an actual clinical setting, we always collect data from both ears.Let's assume the patient is Seda Karan, and she told you that she has difficulty hearing high-pitched sounds clearly. This information might give us a hint about her possible hearing issues.Before we proceed, I just want to confirm: did you conduct the audiometry test with air conduction and bone conduction separately? Or were they both missing in your report?Also, based on Seda's statement that she has difficulty hearing high-pitched sounds, do you think there might be a specific frequency range where her thresholds could be higher than normal?Let's discuss further to improve your understanding of audiometry testing.";
        FetchAndPlayAudio(soundText);
    }


    public AudioSource audioSource; // Sesin oynatýlacaðý AudioSource

    // Bilgisayardaki API endpoint'i (örneðin, bilgisayarýn IP adresi)
    private string apiUrl = "http://10.20.0.93:5000/synthesize_file"; // IP'yi güncelleyin

    public void FetchAndPlayAudio(string textToSynthesize)
    {
        StartCoroutine(GetAudioFromAPI(textToSynthesize));
    }

    private IEnumerator GetAudioFromAPI(string text)
    {
        string jsonPayload = $"{{\"text\": \"{text}\", \"language\": \"tr\", \"speaker\": \"Dionisio Schuyler\"}}";
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonPayload);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            UnityEngine.Debug.LogError("API Error: " + request.error);
        }
        else
        {
            // Ses dosyasýný temp olarak kaydet
            string filePath = Application.persistentDataPath + "/temp_audio.wav";
            System.IO.File.WriteAllBytes(filePath, request.downloadHandler.data);

            // Temp dosyayý AudioSource ile oynat
            StartCoroutine(PlayAudioFromFile(filePath));
        }
    }

    private IEnumerator PlayAudioFromFile(string filePath)
    {
        using (WWW www = new WWW("file:///" + filePath))
        {
            yield return www;

            if (www.error == null)
            {
                audioSource.clip = www.GetAudioClip();
                audioSource.Play();
            }
            else
            {
                UnityEngine.Debug.LogError("Failed to load audio: " + www.error);
            }
        }
    }

}
