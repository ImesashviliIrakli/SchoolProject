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
    public partial class StudentGrade : Form
    {
        public StudentGrade()
        {
            InitializeComponent();
        }

        private void StudentGrade_Load(object sender, EventArgs e)
        {
            this.studentGradeTableAdapter.Fill(this.schoolDataSet.StudentGrade);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        #region AddStudentGrade
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO dbo.StudentGrade ( CourseID, StudentID, Grade ) VALUES ( @courseId, @studentId, @grade)";

                sqlCommand.Parameters.Add("@courseId", SqlDbType.Int);
                sqlCommand.Parameters.Add("@studentId", SqlDbType.Int);
                sqlCommand.Parameters.Add("@grade", SqlDbType.Decimal);

                var checkCourseId = int.TryParse(textBox1.Text, out var courseId);
                var checkStudentId = int.TryParse(textBox2.Text, out var studentId);
                var checkGrade = decimal.TryParse(textBox3.Text, out var grade);

                if (!checkCourseId || !checkStudentId || !checkGrade)
                {
                    label6.Text = "Must be a number";
                    return;
                }

                sqlCommand.Parameters["@courseId"].Value = courseId;
                sqlCommand.Parameters["@studentId"].Value = studentId;
                sqlCommand.Parameters["@grade"].Value = grade;

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.StudentGrade", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "StudentGrade");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "StudentGrade";

                label6.Text = "Success";

                connection.Close();
            }
            catch (Exception)
            {
                label6.Text = "Something went wrong";
            }
        }
        #endregion

        #region DeleteStudentGrade
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "DELETE FROM dbo.StudentGrade WHERE EnrollmentID = @Id";

                sqlCommand.Parameters.Add("@Id", SqlDbType.Int);

                bool checkId = int.TryParse(textBox5.Text, out var Id);

                if (checkId)
                {
                    sqlCommand.Parameters["@Id"].Value = Id;
                }

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.StudentGrade", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "StudentGrade");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "StudentGrade";

                connection.Close();

                label10.Text = "Success";

            }
            catch (Exception)
            {
                label10.Text = "Something went wrong";
            }
        }
        #endregion

        #region UpdateStudentGrade
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=(LocalDb)\\LocalDbDemo; database=School; Integrated Security = True");

            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand("GetStudentGrade", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                sqlCommand.Parameters["@id"].Value = int.Parse(textBox6.Text);

                var courseId = sqlCommand.Parameters.Add("@courseId", SqlDbType.Int);
                courseId.Direction = ParameterDirection.Output;

                var studentId = sqlCommand.Parameters.Add("@studentId", SqlDbType.Int);
                studentId.Direction = ParameterDirection.Output;

                var grade = sqlCommand.Parameters.Add("@grade", SqlDbType.Int);
                grade.Direction = ParameterDirection.Output;

                sqlCommand.ExecuteNonQuery();

                textBox7.Text = courseId.Value.ToString();
                textBox8.Text = studentId.Value.ToString();
                textBox9.Text = grade.Value.ToString();

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
                sqlCommand.CommandText = "UPDATE dbo.StudentGrade SET CourseID = @courseId, StudentID = @studentId, Grade = @grade WHERE EnrollmentID = @id";

                sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                sqlCommand.Parameters.Add("@courseId", SqlDbType.Int);
                sqlCommand.Parameters.Add("@studentId", SqlDbType.Int);
                sqlCommand.Parameters.Add("@grade", SqlDbType.Decimal);

                var checkId = int.TryParse(textBox6.Text, out var id);
                var checkCourseId = int.TryParse(textBox7.Text, out var courseId);
                var checkStudentId = int.TryParse(textBox8.Text, out var studentId);
                var checkGrade = decimal.TryParse(textBox9.Text, out var grade);

                if (!checkId ||!checkCourseId || !checkStudentId || !checkGrade)
                {
                    label14.Text = "Must be a number";
                    return;
                }
                sqlCommand.Parameters["@id"].Value = courseId;
                sqlCommand.Parameters["@courseId"].Value = courseId;
                sqlCommand.Parameters["@studentId"].Value = studentId;
                sqlCommand.Parameters["@grade"].Value = grade;

                sqlCommand.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.StudentGrade", connection);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "StudentGrade");
                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "StudentGrade";

                label14.Text = "Success";

                connection.Close();
            }
            catch (Exception)
            {
                label6.Text = "Something went wrong";
            }
        }
        #endregion
    }
}
