using OnMyWay2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
namespace OnMyWay2.Controllers
{
	/// <summary>
	/// Summary description for Admin
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class Admin : System.Web.Services.WebService
	{

		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}
		[WebMethod(MessageName = "Login admin", Description = "Login new admin")]

		[System.Xml.Serialization.XmlInclude(typeof(ReturnData))]
		public ReturnData loginadmin(string name, string password)  /// get list of notes
		{

			int UserID = 0;
			string Message = "";
			string Unid = null;
			string admin_password = "P@Ass_19920";
			string admin_user = "Admin_2020_xx";

			try
			{
				if (name.Equals(admin_user) & password.Equals(admin_password))
					{
					Message = "login succ";
					UserID = 110;
				}

				
				else
				{
					Message = " user name or password is in correct";
					UserID = 0;
				}
				
				
		


			}
			catch (Exception ex)
			{
				Message = " cannot access to the data";

			}
			ReturnData rt = new ReturnData();
			rt.id = UserID;
			rt.message = Message;
			rt.unid = Unid;

			return rt;
		}
	}
}
