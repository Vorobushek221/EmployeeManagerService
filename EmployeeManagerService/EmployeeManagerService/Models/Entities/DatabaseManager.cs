using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace EmployeeManagerService.Models.Entities
{
    public class DatabaseManager
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public readonly string databaseName = WebConfigurationManager.AppSettings["databaseName"];

        public DatabaseManager()
        {
            if (!CheckDatabaseExists(databaseName))
            {
                CreateDatabase(databaseName);
                CreateTables(databaseName);
            }
        }

        public List<Company> Companies
        {
            get
            {
                var list = new List<Company>();

                string sqlExpression = string.Format("SELECT * FROM {0}.dbo.Companies", databaseName);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new Company
                            {
                                Id = (int)reader["Id"],
                                Name = ((string)reader["Name"]).Trim(),
                                Size = (int)reader["Size"],
                                LegalForm = ((string)reader["LegalForm"]).Trim()
                            });
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
                return list;
            }
        }

        public List<Employee> Employees
        {
            get
            {
                var list = new List<Employee>();

                string sqlExpression = string.Format("SELECT * FROM {0}.dbo.Employees", databaseName);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new Employee
                            {
                                Id = (int)reader["Id"],
                                Surname = ((string)reader["Surname"]).Trim(),
                                Name = ((string)reader["Name"]).Trim(),
                                FatherName = ((string)reader["FatherName"]).Trim(),
                                RecruitDate = (DateTime)reader["RecruitDate"],
                                Rank = (int)reader["Rank"],
                                CompanyId = (int)reader["CompanyId"]
                            });
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
                return list;
            }
        }

        public void AddCompany(Company company)
        {
            string query = string.Format(@"INSERT INTO {0}.dbo.Companies (Name, Size, LegalForm) 
                                           VALUES (@Name, @Size, @LegalForm)", databaseName);

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = company.Name;
                cmd.Parameters.Add("@Size", SqlDbType.Int).Value = company.Size;
                cmd.Parameters.Add("@LegalForm", SqlDbType.VarChar, 20).Value = company.LegalForm;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void RemoveCompany(int companyId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();

                cmd.CommandText = string.Format(@"DELETE FROM {0}.dbo.Employees WHERE CompanyId = @Id", databaseName);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = companyId;
                cmd.ExecuteNonQuery();

                cmd.CommandText = string.Format(@"DELETE FROM {0}.dbo.Companies WHERE Id = @Id", databaseName);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateCompany(Company company)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = string.Format(@"UPDATE {0}.dbo.Companies
                                    SET Name = @Name, Size = @Size, LegalForm = @LegalForm 
                                    WHERE Id = @Id", databaseName);
                cmd.Parameters.AddWithValue("@Id", company.Id);
                cmd.Parameters.AddWithValue("@Name", company.Name);
                cmd.Parameters.AddWithValue("@Size", company.Size);
                cmd.Parameters.AddWithValue("@LegalForm", company.LegalForm);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void AddEmployee(Employee employee)
        {
            string query = string.Format(@"INSERT INTO {0}.dbo.Employees (Surname, Name, FatherName, RecruitDate, Rank, CompanyId) 
                             VALUES (@Surname, @Name, @FatherName, @RecruitDate, @Rank, @CompanyId)",databaseName);

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 50).Value = employee.Surname;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = employee.Name;
                cmd.Parameters.Add("@FatherName", SqlDbType.VarChar, 50).Value = employee.FatherName;
                cmd.Parameters.Add("@RecruitDate", SqlDbType.DateTime).Value = employee.RecruitDate;
                cmd.Parameters.Add("@Rank", SqlDbType.Int).Value = employee.Rank;
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = employee.CompanyId;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = string.Format(@"UPDATE {0}.dbo.Employees
                                    SET Surname = @Surname, Name = @Name, FatherName = @FatherName, 
                                    RecruitDate = @RecruitDate, Rank = @Rank, CompanyId = @CompanyId 
                                    WHERE Id = @Id", databaseName);

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = employee.Id;
                cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 50).Value = employee.Surname;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = employee.Name;
                cmd.Parameters.Add("@FatherName", SqlDbType.VarChar, 50).Value = employee.FatherName;
                cmd.Parameters.Add("@RecruitDate", SqlDbType.DateTime).Value = employee.RecruitDate;
                cmd.Parameters.Add("@Rank", SqlDbType.Int).Value = employee.Rank;
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = employee.CompanyId;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void RemoveEmployee(int emloyeeId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = string.Format(@"DELETE FROM {0}.dbo.Employees WHERE Id = @Id", databaseName);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = emloyeeId;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        private bool CheckDatabaseExists(string databaseName)
        {
            string sqlCreateDBQuery;
            bool result = false;

            try
            {
                sqlCreateDBQuery = string.Format(@"SELECT database_id FROM sys.databases WHERE Name = '{0}'", databaseName);

                using (var connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, connection))
                    {
                        connection.Open();

                        var resultObj = sqlCmd.ExecuteScalar();

                        int databaseID = 0;

                        if (resultObj != null)
                        {
                            int.TryParse(resultObj.ToString(), out databaseID);
                        }
                        connection.Close();
                        result = (databaseID > 0);
                    }
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        private void CreateDatabase(string databaseName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = string.Format("CREATE DATABASE {0}", databaseName);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void CreateTables(string databaseName)
        {
            string CompaniesCreateTableQuery = string.Format(@"CREATE TABLE {0}.dbo.Companies(
                                                 Id INTEGER PRIMARY KEY IDENTITY,
                                                 Name CHAR(50) NOT NULL, 
                                                 Size INTEGER NOT NULL, 
                                                 LegalForm CHAR(20) NOT NULL)", databaseName);

            string EmployeesCreateTableQuery = string.Format(@"CREATE TABLE {0}.dbo.Employees(
                                                 Id INTEGER PRIMARY KEY IDENTITY,
                                                 Surname CHAR(50) NOT NULL, 
                                                 Name CHAR(50) NOT NULL, 
                                                 FatherName CHAR(50) NOT NULL,
                                                 RecruitDate DATETIME NOT NULL,
                                                 Rank INTEGER NOT NULL,
                                                 CompanyId INTEGER,
                                                 FOREIGN KEY (CompanyId) REFERENCES {0}.dbo.Companies(Id))", databaseName);

            ExecuteSql(CompaniesCreateTableQuery);
            ExecuteSql(EmployeesCreateTableQuery);
        }

        private void ExecuteSql(string sql)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}