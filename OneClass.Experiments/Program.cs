using Microsoft.Data.SqlClient;

string connectionString = string.Empty;
connectionString += "Server=tcp:oneclass.database.windows.net,1433;";
connectionString += "Initial Catalog=oneclassdb;";
connectionString += "Persist Security Info=False;";
connectionString += "User ID=oneclass_admin;";
connectionString += "Password=8Ap!HGW2V!xy8uC;";
connectionString += "MultipleActiveResultSets=False;";
connectionString += "Encrypt=True;";
connectionString += "TrustServerCertificate=False;";
connectionString += "Connection Timeout=30;";

try
{
	using (SqlConnection connection = new SqlConnection(connectionString))
	{
		connection.Open();
	}
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
}
