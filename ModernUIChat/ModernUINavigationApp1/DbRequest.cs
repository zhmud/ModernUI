using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModernUINavigationApp1.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace ModernUINavigationApp1
{
    class DbRequest
    {
        string serverName = "db4free.net"; // Адрес сервера (для локальной базы пишите "localhost")
        string userName = "adminzhmud"; // Имя пользователя
        string dbName = "dbchat_corporate"; //Имя базы данных
        string port = "3306"; // Порт для подключения
        string password = "admin10"; // Пароль для подключения
        MySqlConnection connection;

        public DbRequest()
        {
            string connStr = "server=" + serverName +
                 ";user=" + userName +
                 ";database=" + dbName +
                 ";port=" + port +
                 ";";
            connStr = password != null ? connStr += "password=" + password + ";" : connStr;
            connection = new MySqlConnection(connStr);
        }

        public User CreateNewAccount(User user)
        { 
            connection.Open();
            string sql = "SELECT COUNT(*) FROM Users WHERE login = '" + user.login + "'"; // Строка запроса
            MySqlCommand sqlCom = new MySqlCommand(sql, connection);
            int count = Convert.ToInt32(sqlCom.ExecuteScalar());
            if (count != 0)
            {
                connection.Close();
                return null;
            }
            sql = "INSERT INTO Users (login,email,first_name,last_name,password,role_id) VALUES('" +
                user.login + "', '" +
                user.email + "', '" +
                user.first_name + "', '" +
                user.last_name + "', '" +
                user.password + "', " +
                user.role_id + ")";
            sqlCom = new MySqlCommand(sql, connection);
            sqlCom.ExecuteNonQuery();

            sql = "SELECT * FROM Users WHERE login = '" + user.login + "'"; // Строка запроса
            sqlCom = new MySqlCommand(sql, connection);
            sqlCom.ExecuteNonQuery();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCom);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            var myData = dt.Select();
            User userResult = new User();
            userResult.id = (int)myData[0].ItemArray[0];
            userResult.login = (string)myData[0].ItemArray[1];
            userResult.email = (string)(myData[0].ItemArray[2] != null ? myData[0].ItemArray[2] : "");
            userResult.first_name = (string)(myData[0].ItemArray[3] != null ? myData[0].ItemArray[3] : "");
            userResult.last_name = (string)(myData[0].ItemArray[4] != null ? myData[0].ItemArray[4] : "");
            userResult.password = (string)myData[0].ItemArray[5];
            userResult.role_id = (int)(myData[0].ItemArray[6] != null ? myData[0].ItemArray[6] : "");

            connection.Close();
            return userResult;
        }

        public List<Role> GetRoles()
        {
            connection.Open();

            string sql = "SELECT * FROM Roles"; // Строка запроса
            MySqlCommand sqlCom = new MySqlCommand(sql, connection);
            sqlCom.ExecuteNonQuery();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCom);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            var myData = dt.Select();
            List<Role> roles = new List<Role>();
            for (int i = 0; i < myData.Length; i++)
            {
                roles.Add(new Role() { id = (int)myData[i].ItemArray[0], name = (string)myData[i].ItemArray[1] });
            }
            connection.Close();
            return roles;
        }

        public User LogIn(string login, string password)
        {
            connection.Open();
            string sql = "SELECT * FROM Users WHERE login = '" + login + "' AND password = '" + password + "'"; // Строка запроса
            var sqlCom = new MySqlCommand(sql, connection);
            sqlCom.ExecuteNonQuery();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCom);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            var myData = dt.Select();
            if (myData.Length == 0)
            {
                connection.Close();
                return null;
            }
            User userResult = new User();
            userResult.id = (int)myData[0].ItemArray[0];
            userResult.login = (string)myData[0].ItemArray[1];
            userResult.email = (string)(myData[0].ItemArray[2] != null? myData[0].ItemArray[2]: "");
            userResult.first_name = (string)(myData[0].ItemArray[3] != null ? myData[0].ItemArray[3] : "");
            userResult.last_name = (string)(myData[0].ItemArray[4] != null ? myData[0].ItemArray[4] : "");
            userResult.password = (string)myData[0].ItemArray[5];
            userResult.role_id = (int)(myData[0].ItemArray[6] != null ? myData[0].ItemArray[6] : "");

            connection.Close();
            return userResult;
        }

      
    }
}
