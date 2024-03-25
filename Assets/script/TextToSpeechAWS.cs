using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class TextToSpeechAWS : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    //抓MainScene的AWSKey和AWSSecretKey
    [SerializeField] private MainScene mainScene;
    private BasicAWSCredentials credentials;

    //AWS_Speak()此方法，會交由ChatGPTManager的OnResponse通知，並且會傳進ChatGPT的回覆文字
    public async void AWS_Speak(string ChatMessage_content)
    {
        var client = new AmazonPollyClient(credentials, RegionEndpoint.APNortheast1); //設定AWS的區域，亞太區域東京

        var request = new SynthesizeSpeechRequest()
        {
            Text = ChatMessage_content, //ChatGPT的回覆文字
            Engine = Engine.Standard,   //引擎，標準比較便宜，Neural比較貴比較自然
            VoiceId = VoiceId.Zhiyu,    //語音，中文聲音只有子瑜能使用
            OutputFormat = OutputFormat.Mp3,   //輸出格式
        };

        var response = await client.SynthesizeSpeechAsync(request); //等待AWS輸出聲音

        //輸出音檔給Unity抓取，由於Unity的限制，無法直接轉換System.IO.Stream，所以要轉換成byte[]
        WriteINtoFile(response.AudioStream);

        //透過www讀取音檔
        using (var www = UnityWebRequestMultimedia.GetAudioClip(uri:$"{Application.persistentDataPath}/AWSaudio.mp3", AudioType.MPEG)) //www: UnityWebRequest
        {
            var op = www.SendWebRequest();   //發送請求 op:UnityWebRequestAsyncOperation

            while (!op.isDone) await Task.Yield(); //等待請求完成

            var clip = DownloadHandlerAudioClip.GetContent(www); //等待完成後，取得音檔

            audioSource.clip = clip; //將音檔放進audioSource
            audioSource.Play(); //播放音檔
        }
    }

    //寫入音檔進Unity
    private void WriteINtoFile(Stream stream)
    {
        using (var fileStream = new FileStream(path: $"{Application.persistentDataPath}/AWSaudio.mp3", FileMode.Create))
        {
            byte[] buffer = new byte[8 * 1024];//設置緩衝記憶體
            int bytesRead; //讀取的位元組數

            while((bytesRead = stream.Read(buffer,offset:0,count:buffer.Length)) > 0)   //當讀取的位元組數大於0時 進行寫入
            {
                fileStream.Write(buffer, offset: 0, bytesRead);
            }

        }
    }

    public void GetAWS_KeyManager()
    {
        credentials = new BasicAWSCredentials(accessKey: mainScene.getAWSKey(), secretKey: mainScene.getAWSSecretKey());
        //print(mainScene.getAWSKey());
        //print(mainScene.getAWSSecretKey());
    }
}
