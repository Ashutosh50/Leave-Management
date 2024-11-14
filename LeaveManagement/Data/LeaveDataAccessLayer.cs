using LeaveManagement.Models;
using System.Data;
using System.Data.SqlClient;

namespace LeaveManagement.Data
{
    public class LeaveDataAccessLayer
    {
        string cs = ConnnectionString.dbcs;
        public List<LeaveReq> GetLeaveReqs() 
        {
            List<LeaveReq> Leaves = new List<LeaveReq>();
            using (SqlConnection con = new SqlConnection(cs)) 
            {
                SqlCommand cmd = new SqlCommand("GetAllReq", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LeaveReq lev = new LeaveReq();
                    lev.LeaveId= Convert.ToInt32(reader["LeaveId"]);
                    lev.EmployeeId= Convert.ToInt32(reader["EmployeeId"]);
                    lev.StartDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["StartDate"]));
                    lev.EndDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["EndDate"]));
                    lev.Reason = reader["Reason"].ToString() ?? "";
                    lev.Staus = reader["Staus"].ToString() ?? "";
                    Leaves.Add(lev);
                }
            }
            return Leaves;
        }
        public void AddLeaveReq(LeaveReq req)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("LeaveReq", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", req.EmployeeId);
                cmd.Parameters.AddWithValue("@Start", req.StartDate.ToDateTime(TimeOnly.MinValue));
                cmd.Parameters.AddWithValue("@End", req.EndDate.ToDateTime(TimeOnly.MinValue));
                cmd.Parameters.AddWithValue("@Reason", req.Reason);
                cmd.Parameters.AddWithValue("@status", "Pending");
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateStatus(int id, string status)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_UpdateReq", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LeaveId",id);
                cmd.Parameters.AddWithValue("@status", status);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public LeaveReq GetReqById(int id) 
        {
            LeaveReq lev = new LeaveReq();
            using (SqlConnection con = new SqlConnection(cs)) 
            {
                SqlCommand cmd = new SqlCommand("GetReqById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);
                con.Open();
               SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    lev.LeaveId = Convert.ToInt32(reader["LeaveId"]);
                    lev.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    lev.StartDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["StartDate"]));
                    lev.EndDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["EndDate"]));
                    lev.Reason = reader["Reason"].ToString() ?? "";
                    lev.Staus = reader["Staus"].ToString() ?? "";
                }
            }
            return lev;
        }

    }
}
