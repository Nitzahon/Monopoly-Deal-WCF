using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDWcfWFClient
{
    internal class ClientInfo
    {
        int id = -1;
        Guid guid;

        public ClientInfo(int idP)
        {
            //Player ID
            id = idP;
            MessageBox.Show("client started id:" + id);
        }

        public int getID() { return id; }
    }
}