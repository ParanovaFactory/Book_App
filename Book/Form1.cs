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

namespace Book
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection sql = new SqlConnection(@"Data Source=predator;Initial Catalog=Db_Book;Integrated Security=True;TrustServerCertificate=True");

        void listBook()
        {
            sql.Open();
            SqlCommand cmd = new SqlCommand("select * from TblBook", sql);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sql.Close();
        }

        void listDeletedBook()
        {
            sql.Open();
            SqlCommand cmd = new SqlCommand("select * from Tbl_DeletedBook", sql);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
            sql.Close();
        }

        void listBookCount()
        {
            sql.Open();
            SqlCommand cmd = new SqlCommand("select * from TblNumOfBook", sql);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblbook.Text = reader[0].ToString();
            }
            sql.Close();
        }

        void clear()
        {
            txtName.Clear();
            txtAuthor.Clear();
            mskPage.Clear();
            txtPublisher.Clear();
            txtType.Clear();
        }

        string id, name, author, page, publisher, type;

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("insert into TblBook (bookName,bookAuthor,bookPage,publishingHouse,bookType) values(@p1,@p2,@p3,@p4,@p5)", sql);
                cmd.Parameters.AddWithValue("@p1", txtName.Text);
                cmd.Parameters.AddWithValue("@p2", txtAuthor.Text);
                cmd.Parameters.AddWithValue("@p3", mskPage.Text);
                cmd.Parameters.AddWithValue("@p4", txtPublisher.Text);
                cmd.Parameters.AddWithValue("@p5", txtType.Text);
                cmd.ExecuteNonQuery();
                sql.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Chech the values");
            }


            listBook();
            listDeletedBook();
            listBookCount();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                sql.Open();
                SqlCommand command = new SqlCommand("delete from TblBook where bookId = @p1", sql);
                command.Parameters.AddWithValue("@p1", int.Parse(id));
                command.ExecuteNonQuery();
                sql.Close();

                sql.Open();
                SqlCommand command1 = new SqlCommand("insert into Tbl_DeletedBook (bookDeleteName,bookDeleteAuthor,bookDeletePage,bookDeletePublishingHouse,bookDeleteType) values(@p2,@p3,@p4,@p5,@p6)", sql);
                command1.Parameters.AddWithValue("@p2", name);
                command1.Parameters.AddWithValue("@p3", author);
                command1.Parameters.AddWithValue("@p4", page);
                command1.Parameters.AddWithValue("@p5", publisher);
                command1.Parameters.AddWithValue("@p6", type);
                command1.ExecuteNonQuery();
                sql.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Cilich a value form book list");
            }
            listBook();
            listDeletedBook();
            listBookCount();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBook();
            listDeletedBook();
            listBookCount();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            author = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            page = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            publisher = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            type = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }
    }
}
