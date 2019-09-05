using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CSharp2SqlLibrary {
    public class Users {

        public static Connection Connection { get; set; }   //use connection to make calls to db. static means 1 no matter how many instances

        public static Users Login(string username, string password) {
            var sql = "SELECT * from Users Where @username = @username AND Password = @Password";
            var sqlcmd = new SqlCommand(sql, Connection._Connection);
            sqlcmd.Parameters.AddWithValue("@Username", username);
            sqlcmd.Parameters.AddWithValue("@Password", password);
            var reader = sqlcmd.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                return null;  //if no rows come back return null
            }
            reader.Read();
            var user = new Users();  //create instance of a new user    
            LoadUsersFromSql(user, reader);


            reader.Close();
            return user;

        }

        public static Users GetByPk(int id) {
            var sql = "SELECT * from Users Where Id = @Id";
            var sqlcmd = new SqlCommand(sql, Connection._Connection);
            sqlcmd.Parameters.AddWithValue("@Id", id);
            var reader = sqlcmd.ExecuteReader();
            if(!reader.HasRows) {
                reader.Close();
                return null;  //if no rows come back return null
            }
            reader.Read();
            var user = new Users();  //create instance of a new user    
            LoadUsersFromSql(user, reader);


            reader.Close();
            return user;
        }

        public static List<Users> GetAll() {
            var sql = "SELECT * from Users";
            var sqlcmd = new SqlCommand(sql, Connection._Connection);  //defines command
            var reader = sqlcmd.ExecuteReader();    //executes select statement, reader is returned
            var users = new List<Users>();  //generic collection of users instances created
            while(reader.Read()) {
                var user = new Users();  //create instance of a new user
                users.Add(user);     //add empty instance to a collection

            LoadUsersFromSql(user, reader);

                
            }
            reader.Close();
            return users;   // exits while loop

        }

        private static void LoadUsersFromSql(Users user, SqlDataReader reader) {

            user.Id = (int)reader["Id"];        //changing type from object to int, Id is an int
            user.Username = reader["Username"].ToString();   //turns object into string
            user.Password = (string)reader["Password"];   //also turns object into string
            user.Firstname = (string)reader["Firstname"];
            user.Lastname = (string)reader["Lastname"];
            user.Phone = reader["Phone"]?.ToString();   //return null if null instead of using tostring method
            user.Email = reader["Email"]?.ToString();   //see above
            user.IsAdmin = (bool)reader["IsAdmin"];
            user.IsReviewer = (bool)reader["IsReviewer"];
        }

        public int Id { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }   
        public bool IsReviewer { get; set; }

        public Users() {
        }

        public override string ToString() {
            return $"Id={ Id}, Username={Username}, Password={Password}, " +
                $"Name={Firstname} {Lastname}, Admin?={IsAdmin}, Reviewer?={IsReviewer}";
        }
    }
}
