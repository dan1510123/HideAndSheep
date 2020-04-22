using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.HUD
{
    public class IpRequestManager : MonoBehaviour
    {
        public string HostIp;
        public bool requestFinished;

        private IEnumerator SendRequest(string url)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                HostIp = "0.0.0.0";
                requestFinished = true;
            }
            else
            {
                HostIp = request.downloadHandler.text;
                requestFinished = true;
            }
            yield return null;
        }

        public IEnumerator GetHostIP(string url)
        {
            if (!requestFinished)
            {
                StartCoroutine(SendRequest(url));

            }
            yield return null;

        }
    }

}
