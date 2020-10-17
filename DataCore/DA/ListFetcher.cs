using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;


namespace DataCore.DA
{
    public class ListFetcher
    {
        string connectionString = ConnectionString.MyConnection();

        public DataTable Select(string storedProcedureorCommandText, bool isStoredProcedure = true , string parameter = null , string value = null)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        if(!string.IsNullOrEmpty(value))
                        {
                            command.Parameters.AddWithValue(parameter, value);
                        }
                        if (!isStoredProcedure)
                        {
                            command.CommandType = CommandType.Text;
                        }
                        command.CommandText = storedProcedureorCommandText;
                        connection.Open();

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        dataAdapter.Fill(dataTable);

                        
                    }
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return dataTable;
        }
        public IEnumerable<T> ExcuteObject<T>(string storedProcedureorCommandText, bool isStoredProcedure = true, string parameter = null, string value = null)
        {
            List<T> items = new List<T>();
            var dataTable = Select(storedProcedureorCommandText, isStoredProcedure, parameter,value); //this will use the DataTable Select function
            
            foreach (DataRow row in dataTable.Rows)
            {
                T item = GetItem<T>(row);
                items.Add(item);
            }
            return items;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                            try
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);
                            }
                            catch (Exception e)
                            {
                                
                            }
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }

    
}

