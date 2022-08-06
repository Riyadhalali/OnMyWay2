using OnMyWay2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using WMS.Models.VM;

namespace OnMyWay2.Controllers
{
    /// <summary>
    /// Summary description for api
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class api : System.Web.Services.WebService
	{//C5kyyDbzsu4=

		[WebMethod]
        public string HelloWorld()
        {
            return "Hello Eng. Riyad Al-Ali to your world";
        }

        [WebMethod(MessageName = "Register", Description = "Register New User")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Register_Players(String username, String phone, String gender, String password,  String photo)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
			string en_user_id = "";
            int UserID = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Users where user_phone = '" + phone  + "' or user_name= '" + username + "'" , db.conn);

                adapter.Fill(rt1);

                if (rt1 != null && rt1.Rows.Count > 0)
                {
                    mess = "Username or Phone number already exists ";
                    result = 0;
                }



                else

                {
                    ToBase64 convert = new ToBase64();
                   // convert.Base64ToImage(photo).Save(Server.MapPath("~/users/" + username + ".jpg"));

                    sql = "insert into Users (user_name,user_phone,user_gender,user_password,user_photo) values" +
                           " ('" + username + "','" + phone + "','" + gender + "','" + password + "','" + photo + ".jpg"+"')";

                    // Image1.ImageUrl = "~/Images/Hello.jpg";

                    SqlCommand cmd = new SqlCommand(sql, db.conn);
                    db.conn.Open();
                    result = cmd.ExecuteNonQuery();
                    db.conn.Close();
                    if (result != 0)
                    {
                        mess = "Registered succussfully";
                        SqlDataReader reader;

                        sql = "select user_id from Users where user_phone='" + phone + "'and user_password='" + password + "' ";
                        SqlCommand cmd2 = new SqlCommand(sql, db.conn);
                        cmd.CommandType = CommandType.Text;

                        db.conn.Open();

                        reader = cmd2.ExecuteReader();
                        while (reader.Read())
                        {
                            UserID = reader.GetInt32(0);

                        }
                        if (UserID == 0)
                        {
                            mess = "  phone or password is incorrect";
                        }
                        else
						{
							mess = "login succussfully";
							en_user_id = encript(UserID);

                                 
						}
						reader.Close();

                        db.conn.Close();

                    }


                }



            }
            catch (Exception ex)
            {
                db.conn.Close();
                mess = " Data Incorrect please check connection ";
            }
            var jsonData = new
            {
                message = mess,
                user_id = en_user_id
			};
            Context.Response.Write(sr.Serialize(jsonData));
        }


        [WebMethod(MessageName = "AddOrSeek_Service", Description = "this method to add or seek service 1 for Add 2 for seek in Type")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void AddOrSeek_Service(string user_id, int type, string phone , string space , string date 
            ,string gender,string from , string to,string username)
        {

           
            JavaScriptSerializer sr = new JavaScriptSerializer();
      
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
			int dec_user_id = 0;
			DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
				dec_user_id = decript(user_id);
				sql = "insert into Services" +
                    " (user_id,service_status,service_pickup,service_destination,service_gender,user_name,user_phone," +
                    "service_type,service_date,service_space)" +
                    " values(" + dec_user_id + "," + 0 + ",'" + from + "','" + to + "','" + gender + "','" + username + "'" +
                    ",'" + phone + "'," + type + ",'" + date + "','" + space + "')";
                    SqlCommand cmd = new SqlCommand(sql, db.conn);
                    db.conn.Open();
                    result = cmd.ExecuteNonQuery();
                    db.conn.Close();
                    if (result != 0)
                    {
                        mess = "Service added succussfully ";

                    }

                
            }
            catch (Exception ex)
            {
                db.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Get_Provided_services", Description = "this method to get services  provided or seeked according to the" +
            "type 1 for provided 2 for seeked ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Get_Provided_services(int type)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Services> service_list = new List<Services>();
            string sql = null;

            try
            {
                SqlDataReader reader;

                sql = "select * from Services where service_status="+ 0 +" and  service_type =  " + type;
                SqlCommand cmd = new SqlCommand(sql, db.conn);
                cmd.CommandType = CommandType.Text;

                db.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

					Services service = new Services();
					service.service_date = reader["service_date"].ToString();
					service.service_gender = reader["service_gender"].ToString();
					service.service_space = reader["service_space"].ToString();
					service.service_pickup = reader["service_pickup"].ToString();
					service.service_destination = reader["service_destination"].ToString();
					service.user_name = reader["user_name"].ToString();
					service.user_phone = reader["user_phone"].ToString();
					int user_id_int = Convert.ToInt32(reader["user_id"]);
					service.user_id = encript(user_id_int);
					service.service_id = Convert.ToInt32(reader["service_id"]);
					service.service_type = Convert.ToInt32(reader["service_type"]);
					// service.service_status = Convert.ToInt32(reader["service_status"]);
					service_list.Add(service);
                }

                reader.Close();
                db.conn.Close();
            }
            catch (Exception ex)
            {

                db.conn.Close();
            }

            /*  var jsonData = new
              {
                  name_hotoffer = name,
                  picture = agent_id
              };*/
            Context.Response.Write(sr.Serialize(service_list));
        }

        [WebMethod(MessageName = "Delete_Service", Description = "this method to Delete service ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Delete_Service(int service_id)
        {


            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                 sql = "delete from Services where service_id= " + service_id + "";
                SqlCommand cmd = new SqlCommand(sql, db.conn);
                db.conn.Open();
                result = cmd.ExecuteNonQuery();
                db.conn.Close();
                if (result != 0)
                    mess = "removed successfully";
                else mess = "try again";
            }

            catch (Exception ex)
            {

                db.conn.Close();
                mess = "Error with database";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Add_appointemnt", Description = "this method to Add appointemnt")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Add_appointemnt(string customer_id, string provider_id,
             string customer_name, string provider_name, string customer_phone
           , string provider_phone, string customer_gender, string provider_gender,
             string pickup_location,string destination,string date ,string space ,
             double latitude,double longitude,int service_id)
        { 

             JavaScriptSerializer sr = new JavaScriptSerializer();
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
			int dec_customer_id = 0;
			int dec_provider_id = 0; 
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
			try
			{
				dec_customer_id = decript(customer_id);
				dec_provider_id = decript(provider_id);


				sql = "INSERT INTO [Appointments]" +
					" ([customer_name] ,[customer_phone] ," +
					"[customer_gender],[provider_name],[provider_phone]," +
					"[provider_gender] ,[pickup_location] ,[destination]," +
					"[date] ,[space] ,[latitude],[longitude],[customer_id],[provider_id],[service_id])"
					+
						" values('" + customer_name + "','" + customer_phone + "','" + customer_gender +
						"','" + provider_name + "','" + provider_phone + "','" + provider_gender + "'" +
						",'" + pickup_location + "','" + destination + "'" +
						",'" + date + "','" + space + "'," + latitude + "," + longitude +
						"," + dec_customer_id + "," + dec_provider_id + "," + service_id + ")";
				SqlCommand cmd = new SqlCommand(sql, db.conn);
				db.conn.Open();
				result = cmd.ExecuteNonQuery();
				db.conn.Close();
				if (result != 0)
				{
					sql = "update Services  set service_status = 1 where service_id=" + service_id;

					SqlCommand cmd1 = new SqlCommand(sql, db.conn);
					db.conn.Open();
					result = cmd1.ExecuteNonQuery();
					db.conn.Close();
					if (result != 0)
					{
						mess = "Appointmet added succussfully ";


					}
					else
					{
						mess = "UnExpected Error on stage 2";
					}


				}
				else
				{
					mess = "UnExpected Error on stage 1";

				}
			}
			catch (Exception ex)
			{
				db.conn.Close();
				mess = "Data Incorrect";
			}
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Delete_Appointment", Description = "this method to cancel appointment ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Delete_Appointment(int appointment_id,int service_id)
        {


            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                sql = "delete from Appointments where appointment_id= " + appointment_id + "";
                SqlCommand cmd = new SqlCommand(sql, db.conn);
                db.conn.Open();
                result = cmd.ExecuteNonQuery();
                db.conn.Close();
                if (result != 0)
				{
					sql = "update Services  set service_status = 0 where service_id=" + service_id;

					SqlCommand cmd1 = new SqlCommand(sql, db.conn);
					db.conn.Open();
					result = cmd1.ExecuteNonQuery();
					db.conn.Close();
					if (result != 0)
					{
						mess = "removed successfully";


					}
				}
                else mess = "try again";
            }

            catch (Exception ex)
            {

                db.conn.Close();
                mess = "Error connection to database";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Rate_Service", Description = "this method to Rate Serivce")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Rate_Service(string customer_id, string provider_id,
            int rate)
        {

            JavaScriptSerializer sr = new JavaScriptSerializer();
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
			int dec_customer_id = 0;
			int dec_provider_id = 0;
			try
            {
				dec_customer_id = decript(customer_id);
				dec_provider_id = decript(provider_id);
			

				sql = "INSERT INTO [dbo].[UserRate]([user_id],[user_rate],[provider_id])"
                                 +
                       " values("+ dec_customer_id + " ," + rate + "," + dec_provider_id + ")";
                SqlCommand cmd = new SqlCommand(sql, db.conn);
                db.conn.Open();
                result = cmd.ExecuteNonQuery();
                db.conn.Close();
                if (result != 0)
                {
                    mess = "rate added succussfully ";

                }


            }
            catch (Exception ex)
            {
                db.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Get_My_services", Description = "this method to get customer services  provided or seeked according to the" +
           "type 1 for provided 2 for seeked ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Get_My_services(string user_id,int type)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Services> service_list = new List<Services>();
            string sql = null;
			int dec_user_id = 0;


			try
			{
                SqlDataReader reader;
				dec_user_id = decript(user_id);

				sql = "select * from Services where service_type =  " + type +"and user_id="+ dec_user_id;
                SqlCommand cmd = new SqlCommand(sql, db.conn);
                cmd.CommandType = CommandType.Text;

                db.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Services service = new Services();
                    service.service_date = reader["service_date"].ToString();
                    service.service_gender = reader["service_gender"].ToString();
                    service.service_space = reader["service_space"].ToString();
                    service.service_pickup = reader["service_pickup"].ToString();
                    service.service_destination = reader["service_destination"].ToString();
                    service.user_name = reader["user_name"].ToString();
                    service.user_phone = reader["user_phone"].ToString();
					int user_id_int = Convert.ToInt32(reader["user_id"]);
					service.user_id = encript(user_id_int);
					service.service_id = Convert.ToInt32(reader["service_id"]);
                    service.service_type = Convert.ToInt32(reader["service_type"]);
                    service.service_status = Convert.ToInt32(reader["service_status"]);
                    service_list.Add(service);
                }

                reader.Close();
                db.conn.Close();
            }
            catch (Exception ex)
            {

                db.conn.Close();
            }

            /*  var jsonData = new
              {
                  name_hotoffer = name,
                  picture = agent_id
              };*/
            Context.Response.Write(sr.Serialize(service_list));
        }

        [WebMethod(MessageName = "Get_My_Appointments", Description = "this method to get customer_appointments ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Get_My_Appointments(string user_id)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Appointments> appointment_list = new List<Appointments>();
            string sql = null;
			int dec_user_id = 0;


			try
			{
				dec_user_id = decript(user_id);
				SqlDataReader reader;

                sql = "select * from Appointments where customer_id =  " + dec_user_id + "or provider_id=" + dec_user_id;
                SqlCommand cmd = new SqlCommand(sql, db.conn);
                cmd.CommandType = CommandType.Text;

                db.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Appointments appointment = new Appointments();
                    appointment.customer_name = reader["customer_name"].ToString();
                    appointment.customer_phone = reader["customer_phone"].ToString();
                    appointment.customer_gender = reader["customer_gender"].ToString();
                    appointment.provider_gender = reader["provider_gender"].ToString();
                    appointment.provider_name = reader["provider_name"].ToString();
                    appointment.provider_phone = reader["provider_phone"].ToString();
                    appointment.pickup_location = reader["pickup_location"].ToString();
                    appointment.destination = reader["destination"].ToString();
                    appointment.date = reader["date"].ToString();
                    appointment.space = reader["space"].ToString();
                    appointment.customer_id = Convert.ToInt32(reader["customer_id"]);
                    appointment.provider_id = Convert.ToInt32(reader["provider_id"]);
                    appointment.appointment_id = Convert.ToInt32(reader["appointment_id"]);
					appointment.service_id = Convert.ToInt32(reader["service_id"]);
					appointment.latitude = Convert.ToDouble(reader["latitude"]);
                    appointment.longitude = Convert.ToDouble(reader["longitude"]);
                    appointment_list.Add(appointment);
                }

                reader.Close();
                db.conn.Close();
            }
            catch (Exception ex)
            {

                db.conn.Close();
            }

            /*  var jsonData = new
              {
                  name_hotoffer = name,
                  picture = agent_id
              };*/
            Context.Response.Write(sr.Serialize(appointment_list));
        }

        [WebMethod(MessageName = "Get_Service_location", Description = "this method to get_Service_location ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Get_Service_location(int appointment_id)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Appointments> appointment_list = new List<Appointments>();
            string sql = null;

            double appointment_lat = 0;
            double appointment_lon = 0;
            try
            {
                SqlDataReader reader;
                

                sql = "select * from Appointments where appointment_id =  " + appointment_id;
                SqlCommand cmd = new SqlCommand(sql, db.conn);
                cmd.CommandType = CommandType.Text;

                db.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    appointment_lat = Convert.ToDouble(reader["latitude"]);
                    appointment_lon = Convert.ToDouble(reader["longitude"]);
                }

                reader.Close();
                db.conn.Close();
            }
            catch (Exception ex)
            {

                db.conn.Close();
            }

             var jsonData = new
              {
                  lat = appointment_lat,
                  lon = appointment_lon
             };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Get_Version", Description = "this method To Get_Version of the app")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Version()
        {
            Version h = new Version();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Version> offer = new List<Version>();
            string sql = null;
            string sql1 = null;
            int version_number=0;
            string version_link="";
            try
            {
                //string validate_token = TokenManager.ValidateToken(token);
               
                SqlDataReader reader;

                sql = "select *  from Version";

                SqlCommand cmd = new SqlCommand(sql, db.conn);
                cmd.CommandType = CommandType.Text;

                db.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    version_number = Convert.ToInt32(reader["version_number"]);
                    version_link = reader["version_link"].ToString();
                }

                reader.Close();
                db.conn.Close();
            }
            catch (Exception ex)
            {

                db.conn.Close();
            }

            var jsonData = new
            {
                android_ver = version_number,
                link = version_link,


            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Login_Player", Description = "Login_Player  ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Login_Player(string phone, string password)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            int UserID = 0;
            string Message = "";
            string sql = "";
			string en_user_id = "";
            SHA512Managed SHA512 = new SHA512Managed();
            byte[] Hash = System.Text.Encoding.UTF8.GetBytes(password);
            Hash = SHA512.ComputeHash(Hash);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in Hash)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            string pass = sb.ToString();

            try
            {
                SqlDataReader reader;

                sql = "select user_id from Users where user_phone='" + phone + "'and user_password='" + password + "' ";
                SqlCommand cmd = new SqlCommand(sql, db.conn);
                cmd.CommandType = CommandType.Text;

                db.conn.Open();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserID = reader.GetInt32(0);

                }
                if (UserID == 0)
                {
                    Message = " user name or password is in correct";
                }
                else
                {
					// Message = TokenManager.GenerateToken(phone);
					Message = "login success";
					en_user_id = encript(UserID);

				}
                // Message = "login success";
                reader.Close();

                db.conn.Close();


            }
            catch (Exception ex)
            {
                Message = " cannot access to the data";


            }

            var jsonData = new
            {
                id = en_user_id,
                message = Message
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

		[WebMethod(MessageName = "Get_User_Info", Description = "this method to get User_Info" +
	"type 1 for provided 2 for seeked ")]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]

		public void Get_User_Info(string user_id)
		{
			JavaScriptSerializer sr = new JavaScriptSerializer();
			List<Users> user_info = new List<Users>();
			string sql = null;
			int dec_user_id = 0;
			Users user = new Users();

			try
			{
				SqlDataReader reader;
				dec_user_id = decript(user_id);

				sql = "select * from Users where user_id=" + dec_user_id;
				SqlCommand cmd = new SqlCommand(sql, db.conn);
				cmd.CommandType = CommandType.Text;
				db.conn.Open();
				reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					
					user.user_name = reader["user_name"].ToString();
					user.user_gender = reader["user_gender"].ToString();
					user.user_photo = reader["user_photo"].ToString(); 
					user.user_phone = reader["user_phone"].ToString();
					user_info.Add(user);
				
				}

				reader.Close();
				db.conn.Close();
			}
			catch (Exception ex)
			{

				db.conn.Close();
			}

		  var jsonData = new
              {
                  username = user.user_name,
                  usergender = user.user_gender,
			      userphoto = user.user_photo,
			      usephone = user.user_phone,
		  };
			Context.Response.Write(sr.Serialize(jsonData));
		}
		[WebMethod(MessageName = "Search_services", Description = "this method to get services  provided or seeked according to the" +
			"type 1 for provided 2 for seeked ")]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]

		public void Search_services(int type,string from , string to , string gender , string date)
		{
			JavaScriptSerializer sr = new JavaScriptSerializer();
			List<Services> service_list = new List<Services>();
			string sql = null;

			try
			{
				SqlDataReader reader;
				if(gender.Equals("0") && date.Equals("0"))
				{
					sql = "select * from Services where service_status=" + 0 + " and  service_type =  " + type
						+ " and service_pickup LIKE '%" + from + "%' and service_destination like'%" + to + "%' ";

				}
				else if (!gender.Equals("0") && date.Equals("0"))
				{
					sql = "select * from Services where service_status=" + 0 + " and  service_type =  " + type
										+ " and service_pickup LIKE '%" + from + "%' and service_destination like '%" + to + "%' and service_gender='" + gender + "' ";
				}
				else if (gender.Equals("0") && !date.Equals("0"))
				{
					sql = "select * from Services where service_status=" + 0 + " and  service_type =  " + type
										+ " and service_pickup LIKE '%" + from + "%' and service_destination like '%" + to + "%' and service_date like '%" + date + "%' "; ;
				}
				else if (!gender.Equals("0") && !date.Equals("0"))
				{
					sql = "select * from Services where service_status=" + 0 + " and  service_type =  " + type
										+ " and service_pickup LIKE '%" + from + "%' and service_destination like '%" + to + "%' and service_date like '%" + date + "%'   and service_gender = '" + gender + "' ";
				}

				SqlCommand cmd = new SqlCommand(sql, db.conn);
				cmd.CommandType = CommandType.Text;

				db.conn.Open();
				reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Services service = new Services();
					service.service_date = reader["service_date"].ToString();
					service.service_gender = reader["service_gender"].ToString();
					service.service_space = reader["service_space"].ToString();
					service.service_pickup = reader["service_pickup"].ToString();
					service.service_destination = reader["service_destination"].ToString();
					service.user_name = reader["user_name"].ToString();
					service.user_phone = reader["user_phone"].ToString();
					int user_id_int = Convert.ToInt32(reader["user_id"]);
					service.user_id = encript(user_id_int);
					service.service_id = Convert.ToInt32(reader["service_id"]);
					service.service_type = Convert.ToInt32(reader["service_type"]);
					// service.service_status = Convert.ToInt32(reader["service_status"]);
					service_list.Add(service);
				}

				reader.Close();
				db.conn.Close();
			}
			catch (Exception ex)
			{

				db.conn.Close();
			}

			/*  var jsonData = new
              {
                  name_hotoffer = name,
                  picture = agent_id
              };*/
			Context.Response.Write(sr.Serialize(service_list));
		}
		public string encript(int user_id)
		{
			string hash = @"foxle@rn_@!";
			string res = "";
			byte[] data = UTF8Encoding.UTF8.GetBytes(user_id.ToString());
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
			{
				byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
				using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
				{
					ICryptoTransform transform = tripleDes.CreateEncryptor();
					byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
					res = Convert.ToBase64String(results);
				}
			}


			return res;
		}
		public int decript(string en_user_id)
		{
			string hash = @"foxle@rn_@!";
			string org = "";
			int user_id = -1;
		
			byte[] data1 = Convert.FromBase64String(en_user_id);
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
			{
				byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
				using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
				{
					ICryptoTransform transform = tripleDes.CreateDecryptor();
					byte[] results = transform.TransformFinalBlock(data1, 0, data1.Length);
					org = UTF8Encoding.UTF8.GetString(results);
					user_id= Convert.ToInt32(org);
				}
			}

			return user_id;
		}

    }

}
