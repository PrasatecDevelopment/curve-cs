using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;

namespace Prasatec.Raden
{
    public sealed class ConnectionBuilder<E> : IConnectionBuilder
        where E : IConnection
    {
        private string s_Server, s_Username, S_Password, s_Database, s_Filename;
        private Int32 i_Port;
        private EncryptionOptions e_Encryption;

        public bool IsLocked { get; private set; }

        public string Database()
        {
            return this.s_Database;
        }

        public IConnectionBuilder Database(string Database)
        {
            if (this.IsLocked == true) { throw new BuilderLockedException(this.GetType().FullName); }
            this.s_Database = Database;
            return this;
        }

        public EncryptionOptions Encryption()
        {
            return this.e_Encryption;
        }

        public IConnectionBuilder Encryption(EncryptionOptions Encryption)
        {
            if (this.IsLocked == true) { throw new BuilderLockedException(this.GetType().FullName); }
            this.e_Encryption = Encryption;
            return this;
        }

        public string Filename()
        {
            return this.s_Filename;
        }

        public IConnectionBuilder Filename(string Filename)
        {
            if (this.IsLocked == true) { throw new BuilderLockedException(this.GetType().FullName); }
            this.s_Filename = Filename;
            return this;
        }

        public string Password()
        {
            return this.S_Password;
        }

        public IConnectionBuilder Password(string Password)
        {
            if (this.IsLocked == true) { throw new BuilderLockedException(this.GetType().FullName); }
            this.S_Password = Password;
            return this;
        }

        public int Port()
        {
            return this.i_Port;
        }

        public IConnectionBuilder Port(int Port)
        {
            if (this.IsLocked == true) { throw new BuilderLockedException(this.GetType().FullName); }
            this.i_Port = Port;
            return this;
        }

        public string Server()
        {
            return this.s_Server;
        }

        public IConnectionBuilder Server(string Server)
        {
            if (this.IsLocked == true) { throw new BuilderLockedException(this.GetType().FullName); }
            this.s_Server = Server;
            return this;
        }

        public string Username()
        {
            return this.s_Username;
        }

        public IConnectionBuilder Username(string Username)
        {
            if (this.IsLocked == true) { throw new BuilderLockedException(this.GetType().FullName);  }
            this.s_Username = Username;
            return this;
        }

        public void Lock()
        {
            this.IsLocked = true;
        }

        public IConnection Build()
        {
            IConnection result;
            try
            {
                result = (E)Activator.CreateInstance(typeof(E), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new object[] { this }, null);
            }
            catch (Exception ex)
            {
                if (ex is TargetInvocationException && ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                throw new CodeException(ex.Message);
            }
            return result;
        }

        public IBuilder<IConnection> Save()
        {
            throw new NotImplementedException();
        }

        public IBuilder<IConnection> Load()
        {
            throw new NotImplementedException();
        }
    }
}