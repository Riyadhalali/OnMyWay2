using OnMyWay2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnMyWay2.Controllers
{
    public partial class start : System.Web.UI.Page
    {
		Admin ca = new Admin();
		ReturnData rt = new ReturnData();
		public string SHA512(string pass)
		{
			SHA512Managed SHA512 = new SHA512Managed();
			byte[] Hash = System.Text.Encoding.UTF8.GetBytes(pass);
			Hash = SHA512.ComputeHash(Hash);
			StringBuilder sb = new StringBuilder();
			foreach (byte b in Hash)
			{
				sb.Append(b.ToString("x2").ToLower());
			}
			return sb.ToString();
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			string time = DateTime.Now.ToString("h:mm:ss tt");
			timelable.Text = time;
		  SqlConnection conn =
			new SqlConnection
			("Data Source=SQL5050.site4now.net;Initial Catalog=DB_A6BF71_onmyway2;User Id=DB_A6BF71_onmyway2_admin;Password=They_1234@A;"
);
			try
			{
				conn.Open();
				status.Text = "Successfull connected to the DataBase";
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message);
				status.Text = ex.Message;

			}

		}

		protected void btnLogin_Click(object sender, EventArgs e)
		{

			try
			{


				string time = DateTime.Now.ToString("h:mm:ss tt");
				timelable.Text = time;

				string username = txtusername.Text;
				//string pass = SHA512(txtpassword.Text);
				string pass = txtpassword.Text;


				if (DropDownList1.SelectedItem.Value == "Admin")
				{
					rt = ca.loginadmin(username, pass);//اذا هناك تعديل بالحقول 
					int x = rt.id;
					string y = rt.unid;
					if (rt.id > 0)
					{
						Session["t"] = rt.id;

						//Session["id"] = table.Rows[0]["EmployeeID"].ToString();
						//Session["fn"] = table.Rows[0]["EmployeeName"].ToString();

						Response.Redirect("users_page.aspx?unid=1234*784abcd$%&^@!$#" + y + "");
						// Response.Redirect("users.aspx");
					}
					else
					{
						Label1.Text = "Wrong username or password";
						//Response.Redirect("users.aspx");


					}
				}
			}

			catch (Exception ex)
			{
				Response.Write(ex.Message);

			}
		}

		protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
