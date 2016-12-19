using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Prasatec.Cu2Com
{
    public abstract class ServerBase
    {
        private ushort port;
        private ServiceHost host;
        private Uri baseAddress;

        private ServiceMetadataBehavior smb;
        public ServerBase(ushort Port)
        {
            this.port = Port;
            this.baseAddress = new Uri("localhost:" + Port + "/hello");
        }

        public void Start()
        {
            this.Stop();
            this.host = new ServiceHost(typeof(HelloWorldService), this.baseAddress);
            this.smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            host.Description.Behaviors.Add(smb);
            host.Open();
        }
        public void Stop()
        {
            if (this.host != null)
            {
                this.host.Close();
                this.host = null;
                this.smb = null;
            }
        }
    }
}
