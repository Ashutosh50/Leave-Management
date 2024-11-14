using LeaveManagement.Models;
using System.Data;
using System.Data.SqlClient;

namespace LeaveManagement.Data
{
    public class EmployeeDataAccessLayer
    {
        string cs = ConnnectionString.dbcs;
        public List<Employees> GetAllEmp()
        {
            List<Employees> Emplist = new List<Employees>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetAll", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employees emp = new Employees();
                    emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    emp.Name = reader["Name"].ToString()??"";
                    emp.Email = reader["Email"].ToString()??"";
                    emp.Manager = reader["Manager"] != DBNull.Value && Convert.ToBoolean(reader["Manager"]);
                    Emplist.Add(emp);
                }
            }
            return Emplist;
        }

        public Employees GetById(int Id) 
        {
            Employees emp = new Employees();
            using (SqlConnection con = new SqlConnection(cs))
            {
               
                SqlCommand cmd = new SqlCommand("spGetbyID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    emp.Name = reader["Name"].ToString() ?? "";
                    emp.Email = reader["Email"].ToString() ?? "";
                    emp.Manager = reader["Manager"] != DBNull.Value && Convert.ToBoolean(reader["Manager"]);
                }
            }
            
                return emp;
        
        }
        public void AddEmployee(Employees emp) 
        {
            using (SqlConnection con= new SqlConnection(cs)) 
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name",emp.Name);
                cmd.Parameters.AddWithValue("@Email",emp.Email);
                cmd.Parameters.AddWithValue("@Manager", emp.Manager);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(Employees emp)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", emp.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Manager", emp.Manager);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(int id) 
        {
            using (SqlConnection con = new SqlConnection(cs)) 
            {
                SqlCommand cmd = new SqlCommand("spDelete", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id",id);
                con.Open() ;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
