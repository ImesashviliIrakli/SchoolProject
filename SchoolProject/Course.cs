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
    public partial class Course : Form
    {
        public Course()
        {
            InitializeComponent();
        }

        private void Course_Load(object sender, EventArgs e)
        {
            this.courseTableAdapter.Fill(this.schoolDataSet.Course);
        }

        #region AddCourse
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO dbo.Course ( CourseID, Title, Credits, DepartmentID ) VALUES ( @courseID, @title, @credits, @depId )";

                sqlCommand.Parameters.Add("@courseId", SqlDbType.Int);
                sqlCommand.Parameters.Add("@title", SqlDbType.NVarChar, 100);
                sqlCommand.Parameters.Add("@credits", SqlDbType.Int);
                sqlCommand.Parameters.Add("@depId", SqlDbType.Int);


                var checkCourseId = int.TryParse(textBox1.Text, out var courseId);
                var checkCredits = int.TryParse(textBox3.Text, out var credits);
                var checkDepId = int.TryParse(textBox4.Text, out var depId);

                if (!checkCourseId || !checkCredits || !checkDepId)
                {
                    label6.Text = "Must be a number";
                    return;
                }

                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    label6.Text = "TiTle tust not be empty";
                    return;
                }

                sqlCommand.Parameters["@courseId"].Value = courseId;
                sqlCommand.Parameters["@title"].Value = textBox2.Text;
                sqlCommand.Parameters["@credits"].Value = credits;
                sqlCommand.Parameters["@depId"].Value = depId;

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Course", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Course");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "Course";

                label6.Text = "Success";

                connection.Close();
            }
            catch (Exception)
            {
                label6.Text = "Something went wrong";
            }
        }
        #endregion

        #region DeleteCourse
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "DELETE FROM dbo.Course WHERE CourseID = @courseId";

                sqlCommand.Parameters.Add("@courseId", SqlDbType.Int);

                bool checkCourseId = int.TryParse(textBox5.Text, out var courseId);

                if (checkCourseId)
                {
                    sqlCommand.Parameters["@courseId"].Value = courseId;
                }

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Course", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Course");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "Course";

                connection.Close();

                label10.Text = "Success";

            }
            catch (Exception)
            {
                label10.Text = "Something went wrong";
            }
        }
        #endregion

        #region UpdateCourse
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand("GetCourse", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@courseId", SqlDbType.Int);
                sqlCommand.Parameters["@courseId"].Value = int.Parse(textBox6.Text);

                var title = sqlCommand.Parameters.Add("@title", SqlDbType.NVarChar, 100);
                title.Direction = ParameterDirection.Output;

                var credits = sqlCommand.Parameters.Add("@credits", SqlDbType.Int);
                credits.Direction = ParameterDirection.Output;

                var depId = sqlCommand.Parameters.Add("@depId", SqlDbType.Int);
                depId.Direction = ParameterDirection.Output;

                sqlCommand.ExecuteNonQuery();

                textBox7.Text = title.Value.ToString();
                textBox8.Text = credits.Value.ToString();
                textBox9.Text = depId.Value.ToString();

                label14.Text = "Found the course";

                connection.Close();
            }
            catch (Exception)
            {
                label14.Text = "Could not find the course";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "UPDATE dbo.Course SET Title = @title, Credits = @credits, DepartmentID = @depId WHERE CourseID = @courseId";

                sqlCommand.Parameters.Add("@courseId", SqlDbType.Int);
                sqlCommand.Parameters.Add("@title", SqlDbType.NVarChar, 100);
                sqlCommand.Parameters.Add("@credits", SqlDbType.Int);
                sqlCommand.Parameters.Add("@depId", SqlDbType.Int);


                var checkCourseId = int.TryParse(textBox6.Text, out var courseId);
                var checkCredits = int.TryParse(textBox8.Text, out var credits);
                var checkDepId = int.TryParse(textBox9.Text, out var depId);

                if (!checkCourseId || !checkCredits || !checkDepId)
                {
                    label14.Text = "Must be a number";
                    return;
                }

                if (string.IsNullOrEmpty(textBox7.Text))
                {
                    label14.Text = "TiTle tust not be empty";
                    return;
                }

                sqlCommand.Parameters["@courseId"].Value = courseId;
                sqlCommand.Parameters["@title"].Value = textBox7.Text;
                sqlCommand.Parameters["@credits"].Value = credits;
                sqlCommand.Parameters["@depId"].Value = depId;

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Course", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Course");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "Course";

                label14.Text = "Success";


                connection.Close();
            }
            catch (Exception)
            {
                label14.Text = "Something went wrong";
            }
        }
        #endregion

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
