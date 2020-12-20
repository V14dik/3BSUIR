using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Lr2WindowsService;
using Models;
using LoggerToDB;


namespace DataAccessLayer
{
    public class ConnectionToDataBase
    {
        internal SqlConnection sqlConnect;
        private ConfigurationProvider provider = new ConfigurationProvider();
        private List<OptionsForDeserealizing> list = new List<OptionsForDeserealizing>();
        private Person person = new Person();
        public List<Person> personList = new List<Person>();
        private ErrorLogger log = new ErrorLogger();

        internal string ConnectionString { get; }

        internal ConnectionState DataBaseInfo { get; set; }
        public ConnectionToDataBase()
        {
            
            list = provider.GetOption();

            ConnectionString  = list[0].connectionString;
            sqlConnect = new SqlConnection(ConnectionString);
        }
        public List<Person> ConnectAndInteract()
        {
            try
            {
                sqlConnect.Open();
                string procedureName = "dbo.TablesConnection";

                
                using (SqlCommand command = new SqlCommand(procedureName, sqlConnect))
                {
                    SqlConnection test = command.Connection;
                    using (SqlDataReader reader =  command.ExecuteReader())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                person.Name = (string)reader[0];
                                personList.Add(person);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                log.WriteErrorsToDataBase(e.Message);
                MessageBox.Show(e.Message, "Подключение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return personList;
        }
        public string GetDataBaseInfo()
        {
            DataBaseInfo = sqlConnect.State;
            string DBInfo = DataBaseInfo.ToString();
            return DBInfo;
        }
        public void Disconnect()
        {
            try
            {
                sqlConnect.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Подключение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
