using OnMyWay2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnMyWay2.Controllers
{
	public partial class Appointments_page : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			if (Session["t"] != null)
			{
				if (!IsPostBack)
				{
					BindGridView();


				}
			}

			else
				Response.Redirect("start.aspx");

		}
		private void BindGridView()

		{

			DataTable dt = new DataTable();



			try

			{




				SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Appointments ", db.conn);



				sqlDa.Fill(dt);

				if (dt.Rows.Count > 0)

				{

					GridView1.DataSource = dt;

					GridView1.DataBind();

				}

			}

			catch (System.Data.SqlClient.SqlException ex)

			{

				string msg = "Fetch Error:";

				msg += ex.Message;

				throw new Exception(msg);

			}

			finally

			{

				//connection.Close();

			}

		}

	}
}
