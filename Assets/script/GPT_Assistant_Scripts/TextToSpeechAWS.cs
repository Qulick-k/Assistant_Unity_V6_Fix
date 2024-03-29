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

    //��MainScene��AWSKey�MAWSSecretKey
    [SerializeField] private MainScene mainScene;
    private BasicAWSCredentials credentials;

    //AWS_Speak()����k�A�|���ChatGPTManager��OnResponse�q���A�åB�|�ǶiChatGPT���^�Ф�r
    public async void AWS_Speak(string ChatMessage_content)
    {
        var client = new AmazonPollyClient(credentials, RegionEndpoint.APNortheast1); //�]�wAWS���ϰ�A�ȤӰϰ�F��

        var request = new SynthesizeSpeechRequest()
        {
            Text = ChatMessage_content, //ChatGPT���^�Ф�r
            Engine = Engine.Standard,   //�����A�зǤ���K�y�ANeural����Q����۵M
            VoiceId = VoiceId.Zhiyu,    //�y���A�����n���u���l���ϥ�
            OutputFormat = OutputFormat.Mp3,   //��X�榡
        };

        var response = await client.SynthesizeSpeechAsync(request); //����AWS��X�n��

        //��X���ɵ�Unity����A�ѩ�Unity������A�L�k�����ഫSystem.IO.Stream�A�ҥH�n�ഫ��byte[]
        WriteINtoFile(response.AudioStream);

        //�z�LwwwŪ������
        using (var www = UnityWebRequestMultimedia.GetAudioClip(uri:$"{Application.persistentDataPath}/AWSaudio.mp3", AudioType.MPEG)) //www: UnityWebRequest
        {
            var op = www.SendWebRequest();   //�o�e�ШD op:UnityWebRequestAsyncOperation

            while (!op.isDone) await Task.Yield(); //���ݽШD����

            var clip = DownloadHandlerAudioClip.GetContent(www); //���ݧ�����A���o����

            audioSource.clip = clip; //�N���ɩ�iaudioSource
            audioSource.Play(); //������
        }
    }

    //�g�J���ɶiUnity
    private void WriteINtoFile(Stream stream)
    {
        using (var fileStream = new FileStream(path: $"{Application.persistentDataPath}/AWSaudio.mp3", FileMode.Create))
        {
            byte[] buffer = new byte[8 * 1024];//�]�m�w�İO����
            int bytesRead; //Ū�����줸�ռ�

            while((bytesRead = stream.Read(buffer,offset:0,count:buffer.Length)) > 0)   //��Ū�����줸�ռƤj��0�� �i��g�J
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
