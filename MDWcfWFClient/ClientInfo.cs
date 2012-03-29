using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDWcfWFClient
{
    internal class ClientInfo
    {
        public int id = -1;
        Guid guid;

        public ClientInfo(Guid idP)
        {
            //Player ID
            guid = idP;
            MessageBox.Show("client started id:" + id);
        }

        public Guid getGuidID() { return guid; }
    }
}