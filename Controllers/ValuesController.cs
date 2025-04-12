using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPIApp.Models;
using System.Data.SqlClient;

namespace MyAPIApp.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {

        string constr = "Server=LAPTOP-CTB3RD1I;Database=VardaanTrial;Integrated Security=True;";



        [HttpGet]
        [Route("Test")]

        public String Test()
        {
            return "Success in message for api";
        }





        [HttpGet]
        [Route("ReadALL")]


        public ActionResult<List<Contacts>> ReadALL()
        {

            SqlConnection conn = new SqlConnection(constr);


            try
            {
                string qry = "SELECT * FROM Contacts";

                SqlCommand cmd = new SqlCommand(qry, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();


                List<Contacts> contactsList = new List<Contacts>();


                while (reader.Read())
                {

                    Contacts contact = new Contacts();

                    contact.Id = reader["ID"].ToString();
                    contact.Name = reader["Name"].ToString();
                    contact.ContactNumber = reader["ContactNumber"].ToString();



                    contactsList.Add(contact);
                }

                conn.Close();

                return contactsList;

            }

            catch (Exception ex)
            {
                return StatusCode(500, ex);

            }

        }




            [HttpGet]
            [Route("ReadSingleData")]


            public ActionResult<Contacts> ReadSingleData()

            {

                SqlConnection conn = new SqlConnection(constr);


                try
                {
                    string qry = "SELECT * FROM Contacts where ID = 1";

                    SqlCommand cmd = new SqlCommand(qry, conn);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();


                    Contacts contact = new Contacts();


                    while (reader.Read())
                    {


                        contact.Id = reader["ID"].ToString();
                        contact.Name = reader["Name"].ToString();
                        contact.ContactNumber = reader["ContactNumber"].ToString();

                    }

                    conn.Close();

                    return contact;

                }

                catch (Exception ex)
                {
                    return StatusCode(500, ex);

                }
            }







        [HttpGet]
        [Route("ReadDatabyID")]


        public ActionResult<Contacts> ReadDatabyID(int id)

        {

            SqlConnection conn = new SqlConnection(constr);


            try
            {
                string qry = "SELECT * FROM Contacts where ID = @ID ";

                SqlCommand cmd = new SqlCommand(qry, conn);


                cmd.Parameters.AddWithValue("ID", id);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();


                Contacts contact = new Contacts();


                while (reader.Read())
                {


                    contact.Id = reader["ID"].ToString();
                    contact.Name = reader["Name"].ToString();
                    contact.ContactNumber = reader["ContactNumber"].ToString();

                }

                conn.Close();

                return contact;

            }

            catch (Exception ex)
            {
                return StatusCode(500, ex);

            }
        }



        //############################################################  Post Request  #########################################################################################################


        [HttpPost]
        [Route("SaveContact")]

        public ActionResult<String> SaveContact(SaveContacts contact)
        {


            SqlConnection conn = new SqlConnection(constr);


            try
            {
                string qry = "INSERT INTO Contacts (Name, ContactNumber) VALUES (@Name, @ContactNumber)";

                SqlCommand cmd = new SqlCommand(qry, conn);

                cmd.Parameters.AddWithValue("@Name", contact.Name);
                cmd.Parameters.AddWithValue("@ContactNumber", contact.ContactNumber);
                conn.Open();

                int roweffected = cmd.ExecuteNonQuery();

                if (roweffected > 0)
                {
                    return "Data Saved";
                }
                else
                {
                    return "Failed to Save";
                }


            }

            catch
            {
                return "failed";
            }


        }






        [HttpPost]
        [Route("UpdateContact")]

        public ActionResult<String> UpdateContact(Contacts contact)
        {


            SqlConnection conn = new SqlConnection(constr);


            try
            {
                string qry = "UPDATE Contacts SET Name = @Name, ContactNumber = @ContactNumber WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(qry, conn);

                cmd.Parameters.AddWithValue("@ID", contact.Id);


                cmd.Parameters.AddWithValue("@Name", contact.Name);
                cmd.Parameters.AddWithValue("@ContactNumber", contact.ContactNumber);
                conn.Open();

                int roweffected = cmd.ExecuteNonQuery();

                if (roweffected > 0)
                {
                    return "Data Updated";
                }
                else
                {
                    return "Failed to Update";
                }


            }

            catch
            {
                return "failed";
            }


        }



    }
}
