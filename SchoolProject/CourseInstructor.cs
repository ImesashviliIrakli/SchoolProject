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
    public partial class CourseInstructor : Form
    {
        public CourseInstructor()
        {
            InitializeComponent();
        }

        private void CourseInstructor_Load(object sender, EventArgs e)
        {
            this.courseInstructorTableAdapter.Fill(this.schoolDataSet.CourseInstructor);
        }

        #region AddCourseInstructor
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO dbo.CourseInstructor ( CourseID, PersonID ) VALUES ( @courseId, @personId );";

                sqlCommand.Parameters.Add("@courseId", SqlDbType.Int);
                sqlCommand.Parameters.Add("@personId", SqlDbType.Int);

                var checkCourseId = int.TryParse(textBox1.Text, out var courseId);
                var checkPersonId = int.TryParse(textBox2.Text, out var personId);

                if (!checkCourseId || !checkPersonId)
                {
                    label6.Text = "Must be number";
                    return;
                }

                sqlCommand.Parameters["@courseId"].Value = courseId;
                sqlCommand.Parameters["@personId"].Value = personId;

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.CourseInstructor", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "CourseInstructor");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "CourseInstructor";

                label6.Text = "Success";

                connection.Close();
            }
            catch (Exception )
            {
                label6.Text = "Something went wrong";
            }
        }
        #endregion

        #region DeleteCourseInstructor
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "DELETE FROM dbo.CourseInstructor WHERE CourseID = @courseId";

                sqlCommand.Parameters.Add("@courseId", SqlDbType.Int);


                bool checkCourseId = int.TryParse(textBox5.Text, out var courseId);

                if (checkCourseId)
                {
                    sqlCommand.Parameters["@courseId"].Value = courseId;
                }

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.CourseInstructor", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "CourseInstructor");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "CourseInstructor";

                connection.Close();

                label10.Text = "Success";

            }
            catch (Exception)
            {
                label10.Text = "Something went wrong";
            }
        }

        #endregion

        #region UpdateCourseInstructor
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("GetCourseInstructor", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@courseId", SqlDbType.Int);
            sqlCommand.Parameters["@courseId"].Value = int.Parse(textBox6.Text);

            var personId = sqlCommand.Parameters.Add("@personId", SqlDbType.NVarChar, 50);
            personId.Direction = ParameterDirection.Output;

            sqlCommand.ExecuteNonQuery();

            textBox7.Text = personId.Value.ToString();

            label14.Text = "Found person";

            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "UPDATE dbo.CourseInstructor SET PersonID = @personId WHERE CourseID = @courseId";

                sqlCommand.Parameters.Add("@courseId", SqlDbType.Int);
                sqlCommand.Parameters.Add("@personId", SqlDbType.Int);

                var checkCourseId = int.TryParse(textBox6.Text, out var courseId);
                var checkPersonId = int.TryParse(textBox7.Text, out var personId);

                if (!checkCourseId || !checkPersonId)
                {
                    label14.Text = "Must be number";
                    return;
                }

                sqlCommand.Parameters["@courseId"].Value = courseId;
                sqlCommand.Parameters["@personId"].Value = personId;

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.CourseInstructor", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "CourseInstructor");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "CourseInstructor";

                label14.Text = "Success";

                connection.Close();
            }
            catch (Exception)
            {
                label14.Text = "Something went wrong";
            }
        }
        #endregion


    }
}