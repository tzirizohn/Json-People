using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Homework_4_08_19.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int id { get; set; }
    }

    public class PersonManager
    {
        private string _connectionString;

        public PersonManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddPerson(Person p)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "insert into people values (@firstname, @lastname, @age)";
            cmd.Parameters.AddWithValue("@firstname", p.FirstName);
            cmd.Parameters.AddWithValue("@lastname", p.LastName);
            cmd.Parameters.AddWithValue("@age", p.Age);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Person>GetPeople()
        {

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from people";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Person> ppl = new List<Person>();
            while (reader.Read())
            {
                ppl.Add(new Person
                {
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"],
                    id = (int)reader["id"]
                });
            }
            return ppl;
        }

        public void Edit(Person p)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();                                               
            cmd.CommandText = "UPDATE people SET FirstName = @firstName, LastName = @lastName, Age=@age WHERE Id = @id";
            cmd.Parameters.AddWithValue("@firstName", p.FirstName);
            cmd.Parameters.AddWithValue("@lastName", p.LastName);
            cmd.Parameters.AddWithValue("@age", p.Age);
            cmd.Parameters.AddWithValue("@id", p.id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "delete from people where id=@id";        
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}