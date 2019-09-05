using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CSharp2SqlLibrary {

    public class Connection {
        public SqlConnection _Connection { get; set; } = null;

        public void Open() {  //method to open
            this._Connection.Open();  //open connection
            if (this._Connection.State != System.Data.ConnectionState.Open) {  //must check if connection is open
                throw new Exception("Connection did not open!");  //if no exception then it opened successfully
            }

        }

        public void Close() {
            if(this._Connection.State != System.Data.ConnectionState.Open) { //if not open, no action needed
                return;
            }
            this._Connection.Close();
        }

        public Connection(string server, string database) {
            var connStr = $"server={server};database={database};trusted_connection=true;";
            this._Connection = new SqlConnection(connStr);  //connection object created
        
        }
    }
}
