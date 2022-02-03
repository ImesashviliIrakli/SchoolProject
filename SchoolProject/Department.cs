using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolProject
{
    public partial class Department : Form
    {
        public Department()
        {
            InitializeComponent();
        }

        private void Department_Load(object sender, EventArgs e)
        {
            this.departmentTableAdapter.Fill(this.schoolDataSet.Department);
        }

        #region AddDepartment
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO dbo.Department ( DepartmentID, Name, Budget, StartDate, Administrator )" +
                    "VALUES ( @departmentId, @name, @budget, @startDate, @administrator )";

                sqlCommand.Parameters.Add("@departmentId", SqlDbType.Int);
                sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar, 50);
                sqlCommand.Parameters.Add("@budget", SqlDbType.Money);
                sqlCommand.Parameters.Add("@startDate", SqlDbType.DateTime);
                sqlCommand.Parameters.Add("@administrator", SqlDbType.Int);

                var checkDepId = int.TryParse(textBox1.Text, out var DepId);
                var checkDepBudget = decimal.TryParse(textBox3.Text, out var DepBudget);
                var checkAdministrator = int.TryParse(textBox4.Text, out var administrator);

                if (!checkDepId || !checkDepBudget || !checkAdministrator)
                {
                    label15.Text = "Must be a number";
                    return;
                }

                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    label15.Text = "Name was not valid";
                    return;
                }

                sqlCommand.Parameters["@departmentId"].Value = DepId;
                sqlCommand.Parameters["@name"].Value = textBox2.Text;
                sqlCommand.Parameters["@budget"].Value = DepBudget;
                sqlCommand.Parameters["@startDate"].Value = dateTimePicker1.Value;
                sqlCommand.Parameters["@administrator"].Value = administrator;

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Department", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Department");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "Department";

                label15.Text = "Success";

                connection.Close();
            }
            catch (Exception ex)
            {
                label15.Text = "Something went wrong";
            }
        }
        #endregion

        #region DeleteDepartment
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "DELETE FROM dbo.Department WHERE DepartmentID = @departmentId";

                sqlCommand.Parameters.Add("@departmentId", SqlDbType.Int);


                bool checkPersonId = int.TryParse(textBox5.Text, out var depId);

                if (checkPersonId)
                {
                    sqlCommand.Parameters["@departmentId"].Value = depId;
                }

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Department", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Department");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "Department";

                connection.Close();

                label18.Text = "Success";

            }
            catch (Exception ex)
            {
                label18.Text = "Something went wrong";
            }
        }
        #endregion

        #region Update Department
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("GetDepartment", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@depId", SqlDbType.Int);
            sqlCommand.Parameters["@depId"].Value = int.Parse(textBox6.Text);

            var name = sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar, 50);
            name.Direction = ParameterDirection.Output;

            var budget = sqlCommand.Parameters.Add("@budget", SqlDbType.Money);
            budget.Direction = ParameterDirection.Output;

            var startDate = sqlCommand.Parameters.Add("@startDate", SqlDbType.DateTime);
            startDate.Direction = ParameterDirection.Output;

            var administrator = sqlCommand.Parameters.Add("@administrator", SqlDbType.Int);
            administrator.Direction = ParameterDirection.Output;

            sqlCommand.ExecuteNonQuery();

            textBox7.Text = name.Value.ToString();
            textBox8.Text = budget.Value.ToString();
            dateTimePicker2.Value = (DateTime)startDate.Value;
            textBox10.Text = administrator.Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "UPDATE dbo.Department" +
                    " SET Name = @name, Budget = @budget, StartDate = @startDate, Administrator = @administrator" +
                    " WHERE DepartmentID = @departmentId";

                sqlCommand.Parameters.Add("@departmentId", SqlDbType.Int);
                sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar, 50);
                sqlCommand.Parameters.Add("@budget", SqlDbType.Money);
                sqlCommand.Parameters.Add("@startDate", SqlDbType.DateTime);
                sqlCommand.Parameters.Add("@administrator", SqlDbType.Int);

                var checkDepId = int.TryParse(textBox6.Text, out var DepId);
                var checkDepBudget = decimal.TryParse(textBox8.Text, out var DepBudget);
                var checkAdministrator = int.TryParse(textBox10.Text, out var administrator);

                if (!checkDepId || !checkDepBudget || !checkAdministrator)
                {
                    label20.Text = "Must be a number";
                    return;
                }

                if (string.IsNullOrEmpty(textBox7.Text))
                {
                    label20.Text = "Name was not valid";
                    return;
                }

                sqlCommand.Parameters["@departmentId"].Value = DepId;
                sqlCommand.Parameters["@name"].Value = textBox7.Text;
                sqlCommand.Parameters["@budget"].Value = DepBudget;
                sqlCommand.Parameters["@startDate"].Value = dateTimePicker2.Value;
                sqlCommand.Parameters["@administrator"].Value = administrator;

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Department", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Department");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "Department";

                label15.Text = "Success";

                connection.Close();
            }
            catch (Exception)
            {
                label20.Text = "Something went wrong";
            }
        }

        #endregion

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
