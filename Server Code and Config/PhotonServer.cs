using Photon.SocketServer;
using System.IO;
using log4net.Config;
using ExitGames.Logging.Log4Net;
using ExitGames.Logging;

namespace PhotonIntro
{
    public class PhotonServer : ApplicationBase
    {
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            Log.Debug("PeerCreate");
            return new UnityClient(initRequest);
        }


        protected override void Setup()
        {
            
        }

        protected override void TearDown()
        {
        }
    }
}
