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
	public partial class users_page : System.Web.UI.Page
	{
		DataTable Dt = new DataTable();

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




				SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Users ", db.conn);



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

		protected void OnRowEditing(object sender, GridViewEditEventArgs e)
		{

			GridView1.EditIndex = e.NewEditIndex;
			BindGridView();
		}

		protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GridView1.Visible = true;

			GridView1.EditIndex = -1;

			BindGridView();
		}


		protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
		{
			GridView1.Visible = true;

			GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];


		   db.conn.Open();

			SqlCommand cmd = new SqlCommand("delete FROM Users where user_id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", db.conn);

			cmd.ExecuteNonQuery();

			db.conn.Close();

			BindGridView();
		}

		protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
		{

			GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
			string id = ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text;

			// string username = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
			string username = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

			string phone = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

			string pass = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

			string gender = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;


		
			GridView1.EditIndex = -1;
			db.conn.Open();

			SqlCommand cmd;

			cmd = new SqlCommand("update Users set user_name='" + username + "' , user_phone='" + phone + "' , user_password='" + pass + "',user_gender='" + gender +  "'where user_id=" + id, db.conn);

			cmd.ExecuteNonQuery();

			db.conn.Close();

			BindGridView();
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{

			string username = UserNameTxt.Text;
			string gender = GenderTxt.Text;
			string phone = PhoneTxt.Text;
			string password = PasswordTxt.Text;
			// string credit = CreditTxt.Text;
			//  string city = CityTxt.Text;

			string sql = "insert into Users (user_name,user_phone,user_gender,user_password,user_photo) values" +
				   " ('" + username + "','" + phone + "','" + gender + "','" + password + "','" + username + ".jpg" + "')";

			db.conn.Open();
			SqlCommand cmd = new SqlCommand(sql, db.conn);
			cmd.ExecuteNonQuery();
			db.conn.Close();
			BindGridView();
		}
	}
}
