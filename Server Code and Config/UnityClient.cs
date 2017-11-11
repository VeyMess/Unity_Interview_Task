using Photon.SocketServer;
using ExitGames.Logging;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;
using System.Threading;

namespace PhotonIntro
{
    public class UnityClient : ClientPeer
    {
        Thread tim;
        float playerHealth;
        float enemyHealth;

        float playerDMG = 15f;
        float enemyDMG = 15f;

        private readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public UnityClient(InitRequest initRequest):base(initRequest)
        {
            tim = new Thread(()=>
            {
                while (true)
                {
                    Thread.Sleep(120000);
                    SendEvent(new EventData(0), new SendParameters());
                }
            });

            tim.IsBackground = true;
            tim.Start();
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            tim.Abort();
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            //Начало боя инициализация жизни обьектов = operation 1
            if (operationRequest.OperationCode == (byte) 1)
            {
                playerHealth = 100;
                enemyHealth = 100;

                OperationResponse tempResp = new OperationResponse(operationRequest.OperationCode);
                Dictionary<byte, object> dic = new Dictionary<byte, object>();

                dic.Add(0, playerHealth);
                dic.Add(1, enemyHealth);
                tempResp.Parameters = dic;

                SendOperationResponse(tempResp, sendParameters);
            }

            //пользователь хочет атаковать = operation 2
            else if (operationRequest.OperationCode == (byte) 2)
            {
                OperationResponse tempResponse = new OperationResponse(operationRequest.OperationCode);
                Dictionary<byte, object> respDic = new Dictionary<byte, object>();
                // враг мертв
                if (enemyHealth == 0)
                {
                    respDic.Add(0, false);
                }
                else
                {
                    // у врага жизни больше чем сила атаки игрока
                    if (enemyHealth > playerDMG)
                    {
                        enemyHealth -= playerDMG;
                    }
                    // у врага жизни меньше чем сила атаки игрока
                    else
                    {
                        enemyHealth = 0;
                    }
                    respDic.Add(0, true);
                }
                respDic.Add(1, enemyHealth);
                tempResponse.Parameters = respDic;

                SendOperationResponse(tempResponse, sendParameters);
            }
            else
            {
                Log.Debug("Unknow operation");
            }
        }
    }
}
