using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnMyWay2.Models
{
    public class db
    {
		//Riyad Computer
		 /* public static SqlConnection conn =
			new SqlConnection
			("Server = Riyad; Database=OnMyway;User Id = sa; Password=61158;");*/
		

		// new database set on server done

		//WIN-HQ8EO8P8GBF 
		public static SqlConnection conn =
		new SqlConnection
		("Server = WIN-HQ8EO8P8GBF ; Database=OnMyway;User Id = sa; Password=61158;");



		

	}
}
