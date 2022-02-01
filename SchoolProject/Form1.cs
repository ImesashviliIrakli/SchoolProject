using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region AddPerson
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=DESKTOP-1S0L8CE; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO dbo.Person ( LastName, FirstName, HireDate, EnrollmentDate, Discriminator )" +
                    "VALUES (@lastName, @firsName, @hireDate, @enrollmentDate, @discriminator)";

                sqlCommand.Parameters.Add("@lastName", SqlDbType.NVarChar, 50);
                sqlCommand.Parameters.Add("@firsName", SqlDbType.NVarChar, 50);
                sqlCommand.Parameters.Add("@hireDate", SqlDbType.DateTime);
                sqlCommand.Parameters.Add("@enrollmentDate", SqlDbType.DateTime);
                sqlCommand.Parameters.Add("@discriminator", SqlDbType.NVarChar, 50);


                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    label13.Text = "Last name was not valid";
                    return;
                }
                else if (textBox1.Text.ToCharArray().Any(char.IsDigit))
                {
                    label13.Text = "Last name must not contain digit";
                    return;
                }

                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    label13.Text = "First name was not valid";
                    return;
                }
                else if (textBox2.Text.ToCharArray().Any(char.IsDigit))
                {
                    label13.Text = "Last name must not contain digit";
                    return;
                }

                if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    label13.Text = "Discriminator was not valid";
                    return;
                }

                sqlCommand.Parameters["@lastName"].Value = textBox1.Text;
                sqlCommand.Parameters["@firsName"].Value = textBox2.Text;
                sqlCommand.Parameters["@hireDate"].Value = dateTimePicker1.Value;
                sqlCommand.Parameters["@enrollmentDate"].Value = dateTimePicker2.Value;
                sqlCommand.Parameters["@discriminator"].Value = comboBox1.Text;

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Person", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Person");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "Person";

                connection.Close();
            }
            catch (Exception ex)
            {
                label13.Text = "Something went wrong";
            }
        }
        #endregion

        #region DeletePerson
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=DESKTOP-1S0L8CE; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "DELETE FROM dbo.Person WHERE PersonID = @personId";

                sqlCommand.Parameters.Add("@personId", SqlDbType.Int);

                int personId = 0;

                bool checkPersonId = int.TryParse(textBox3.Text, out personId);

                if (checkPersonId)
                {
                    sqlCommand.Parameters["@personId"].Value = personId;
                }

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Person", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Person");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "Person";

                connection.Close();

                label10.Text = "Success";

            }
            catch (Exception ex)
            {
                label10.Text = "You must delete this person from other tables first";
            }
        }

        #endregion

        #region UpdatePerson
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=DESKTOP-1S0L8CE; database=School; Integrated Security = True");
            connection.Open();


            SqlCommand sqlCommand = new SqlCommand("getPerson", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@personId", SqlDbType.Int);
            sqlCommand.Parameters["@personId"].Value = int.Parse(textBox4.Text);

            var lastName = sqlCommand.Parameters.Add("@lastName", SqlDbType.NVarChar, 50);
            lastName.Direction = ParameterDirection.Output;

            var firstName = sqlCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 50);
            firstName.Direction = ParameterDirection.Output;

            var hireDate = sqlCommand.Parameters.Add("@hireDate", SqlDbType.DateTime);
            hireDate.Direction = ParameterDirection.Output;

            var enrollmentDate = sqlCommand.Parameters.Add("@enrollmentDate", SqlDbType.DateTime);
            enrollmentDate.Direction = ParameterDirection.Output;

            var discriminator = sqlCommand.Parameters.Add("@discriminator", SqlDbType.NVarChar, 50);
            discriminator.Direction = ParameterDirection.Output;

            sqlCommand.ExecuteNonQuery();

            textBox5.Text = lastName.Value.ToString();

            textBox6.Text = firstName.Value.ToString();

            dateTimePicker3.Value = (DateTime)hireDate.Value;

            dateTimePicker4.Value = (DateTime)enrollmentDate.Value;

            comboBox2.Text = discriminator.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=DESKTOP-1S0L8CE; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "UPDATE dbo.Person" +
                    " SET LastName = @lastName, FirstName = @firstName, HireDate = @hireDate, EnrollmentDate = @enrollmentDate, Discriminator = @discriminator" +
                    " WHERE PersonID = @personId;";

                sqlCommand.Parameters.Add("@personId", SqlDbType.Int);
                sqlCommand.Parameters.Add("@lastName", SqlDbType.NVarChar, 50);
                sqlCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 50);
                sqlCommand.Parameters.Add("@hireDate", SqlDbType.DateTime);
                sqlCommand.Parameters.Add("@enrollmentDate", SqlDbType.DateTime);
                sqlCommand.Parameters.Add("@discriminator", SqlDbType.NVarChar, 50);


                if (string.IsNullOrEmpty(textBox5.Text))
                {
                    label13.Text = "Last name was not valid";
                    return;
                }
                else if (textBox5.Text.ToCharArray().Any(char.IsDigit))
                {
                    label13.Text = "Last name must not contain digit";
                    return;
                }

                if (string.IsNullOrEmpty(textBox6.Text))
                {
                    label13.Text = "First name was not valid";
                    return;
                }
                else if (textBox6.Text.ToCharArray().Any(char.IsDigit))
                {
                    label13.Text = "Last name must not contain digit";
                    return;
                }

                if (string.IsNullOrEmpty(comboBox2.Text))
                {
                    label13.Text = "Discriminator was not valid";
                    return;
                }

                sqlCommand.Parameters["@personId"].Value = int.Parse(textBox4.Text);
                sqlCommand.Parameters["@lastName"].Value = textBox5.Text;
                sqlCommand.Parameters["@firstName"].Value = textBox6.Text;
                sqlCommand.Parameters["@hireDate"].Value = dateTimePicker3.Value;
                sqlCommand.Parameters["@enrollmentDate"].Value = dateTimePicker4.Value;
                sqlCommand.Parameters["@discriminator"].Value = comboBox2.Text;

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Person", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Person");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "Person";

                connection.Close();
            }
            catch (Exception ex)
            {
                label13.Text = "Something went wrong";
            }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            this.personTableAdapter.Fill(this.schoolDataSet.Person);
            

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
