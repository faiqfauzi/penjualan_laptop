using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace penjualan_laptop
{
    public partial class Form1 : Form
    {
        MySqlConnection conn = connection.getConnection();
        DataTable dataTable = new DataTable();

        public DataTable getDataLaptop(bool search)
        {
            dataTable.Reset();
            dataTable = new DataTable();
            string query = "";
            if (search)
            {
                query = "SELECT * FROM data_laptop where id LIKE '%" + txtSearch.Text + "%' or nama LIKE '%" + txtSearch.Text + "%' or seri LIKE '%" + txtSearch.Text + "%' or stok LIKE '%" + txtSearch.Text + "%'";
            }
            else
            {
                query = "SELECT * FROM data_laptop";
            }
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);
            }
            return dataTable;

        }

        public void filldataTable(bool search)
        {
            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = getDataLaptop(search);

            DataGridViewButtonColumn colEditLaptop = new DataGridViewButtonColumn();
            colEditLaptop.UseColumnTextForButtonValue = true;
            colEditLaptop.Text = "Edit";
            colEditLaptop.Name = "";
            dataGridView2.Columns.Add(colEditLaptop);

            DataGridViewButtonColumn colDeleteLaptop = new DataGridViewButtonColumn();
            colDeleteLaptop.UseColumnTextForButtonValue = true;
            colDeleteLaptop.Text = "Delete";
            colDeleteLaptop.Name = "";
            dataGridView2.Columns.Add(colDeleteLaptop);
            
            conn.Close();
        }

        public void resetIncrement()
        {
            MySqlScript script = new MySqlScript(conn, "SET @id := 0; UPDATE data_laptop SET id = @id := (@id+1); " +
                "ALTER TABLE data_laptop AUTO_INCREMENT = 1;");
            script.Execute();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filldataTable(false);
            resetIncrement();
        }

        private void btn_simpan_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd;

            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO data_laptop (nama, seri, stok) VALUE(@nama, @seri, @stok)";
                cmd.Parameters.AddWithValue("@nama", nama.Text);
                cmd.Parameters.AddWithValue("@seri", seri.Text);
                cmd.Parameters.AddWithValue("@stok", stok.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                dataGridView2.Columns.Clear();
                dataTable.Clear();
                filldataTable(false);
            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                int id = Convert.ToInt32(dataGridView2.CurrentCell.RowIndex.ToString());
                id_laptop.Text = dataGridView2.Rows[id].Cells[0].Value.ToString();
                nama.Text = dataGridView2.Rows[id].Cells[1].Value.ToString();
                seri.Text = dataGridView2.Rows[id].Cells[2].Value.ToString();
                stok.Text = dataGridView2.Rows[id].Cells[3].Value.ToString();

                dataGridView2.Enabled = true;
            }

            if (e.ColumnIndex == 5)
            {
                int id = Convert.ToInt32(dataGridView2.CurrentCell.RowIndex.ToString());

                MySqlCommand cmd;
                conn.Open();

                try
                {
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM data_laptop WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", dataGridView2.Rows[id].Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    dataGridView2.Columns.Clear();
                    dataTable.Clear();
                    filldataTable(false);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE data_laptop SET nama=@nama, seri=@seri, stok=@stok where id=@id";
                cmd.Parameters.AddWithValue("@id", id_laptop.Text);
                cmd.Parameters.AddWithValue("@nama", nama.Text);
                cmd.Parameters.AddWithValue("@seri", seri.Text);
                cmd.Parameters.AddWithValue("@stok", stok.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                dataGridView2.Columns.Clear();
                dataTable.Clear();
                filldataTable(false);
            }
            catch (Exception ex)
            {

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            filldataTable(true);
            
            //dataGridView2.DataSource = getDataLaptop(true);
            /*
            DataGridViewButtonColumn colEditLaptop = new DataGridViewButtonColumn();
            colEditLaptop.UseColumnTextForButtonValue = true;
            colEditLaptop.Text = "Edit";
            colEditLaptop.Name = "";
            dataGridView2.Columns.Add(colEditLaptop);

            DataGridViewButtonColumn colDeleteLaptop = new DataGridViewButtonColumn();
            colDeleteLaptop.UseColumnTextForButtonValue = true;
            colDeleteLaptop.Text = "Delete";
            colDeleteLaptop.Name = "";
            dataGridView2.Columns.Add(colDeleteLaptop);*/

            //conn.Close();
            
        }
    }
}