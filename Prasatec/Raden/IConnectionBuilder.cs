using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public interface IConnectionBuilder : IBuilder<IConnection>
    {
        IConnectionBuilder Server(String Server);
        IConnectionBuilder Port(Int32 Port);

        IConnectionBuilder Username(String Username);
        IConnectionBuilder Password(String Password);
        IConnectionBuilder Database(String Database);
        IConnectionBuilder Filename(String Filename);

        IConnectionBuilder Encryption(EncryptionOptions Encryption);


        String Server();
        Int32 Port();

        String Username();
        String Password();
        String Database();
        String Filename();

        EncryptionOptions Encryption();
    }
}
