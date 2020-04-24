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

        public IEnumerator GetHostIp(string url)
        {
            if (!requestFinished)
            {
                UnityWebRequest request = UnityWebRequest.Get(url);
                Debug.Log(request);
                yield return request.SendWebRequest();
                yield return new WaitForSeconds(.5f);
                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.Log(request.error);
                    HostIp = "0.0.0.0";
                }
                else
                {
                    HostIp = request.downloadHandler.text;
                }
                requestFinished = true;
                yield return null;
            }
        }
    }

}
