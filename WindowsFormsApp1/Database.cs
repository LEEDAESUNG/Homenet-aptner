using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Odbc;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WindowsFormsApp1
{
    class Database
    {
        public OdbcConnection oConnection;
        public OdbcCommand oCommand;
        //public string strConn = "DRIVER={MySQL ODBC 3.51 Driver}; Server=192.168.100.110; Database=single; Uid=admin; Pwd=jawootek; CharSet=utf8;";
        public string strConn = "DRIVER={MySQL ODBC 3.51 Driver};        Server=192.168.100.110; Database=jwt_anps; Uid=admin; Pwd=jawootek; CharSet=utf8;";
        //public string strConn = "DRIVER={MySQL ODBC 8.0 Unicode Driver}; Server=192.168.100.110; Database=jwt_anps; Uid=admin; Pwd=jawootek;";
    }
}
