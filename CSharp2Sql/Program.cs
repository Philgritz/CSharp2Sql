﻿using System;
using CSharp2SqlLibrary;
using System.Diagnostics;

namespace CSharp2Sql {
    class Program {

        void Run() {
            var conn = new Connection(@"localhost\sqlexpress", "PRS");  //@ makes \ not a special char
            conn.Open();
            Users.Connection = conn;  //puts open connection in Connection property
            var userLogin = Users.Login("Philgritz", "Gaetano");
            Console.WriteLine(userLogin);
            var userFailedLogin = Users.Login("Xx", "Xx");
            Console.WriteLine(userFailedLogin?.ToString() ?? "Not found");
            var users = Users.GetAll();  //should bring back all users in db table
            foreach(var u in users) {
                Console.WriteLine(u);

            }
            var user = Users.GetByPk(1);
            Debug.WriteLine(user);
            var usernf = Users.GetByPk(222);  //no user found, should be null
            //var success = Users.Delete(3);
            var user3 = Users.GetByPk(3);  //check if delete on pk3 worked
            Debug.WriteLine(user3);

            var newuser = new Users();
            newuser.Username = "FFFF1";
            newuser.Password = "XYZ";
            newuser.Firstname = "Normal";
            newuser.Lastname = "newuser";
            newuser.Phone = "5555555";
            newuser.Email = "Abcd@gmail";
            newuser.IsAdmin = false;
            newuser.IsReviewer = true;
            var success = Users.Insert(newuser);

            var userabc = Users.GetByPk(5);
            userabc.Username = "CCCC1";
            userabc.Firstname = "A";
            userabc.Lastname = "BC";
            success = Users.Update(userabc);


            conn.Close();
        }
        static void Main(string[] args) {
            var pgm = new Program();
            pgm.Run();
            
        }
    }
}
