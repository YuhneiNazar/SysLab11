using System.Data;
using System.Data.SQLite;

namespace SysLab11
{
    public partial class Form1 : Form
    {
        private string nametab;
        public Form1()
        {
            InitializeComponent();
            ViewEnterprise();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewEnterprise();

        }

        public void ViewEnterprise()
        {
            SQLiteConnection sqlite_con = new SQLiteConnection("Data Source=OblikTovariv.db");
            const string sql = "select * from Enterprise;";
            try
            {
                sqlite_con.Open();
                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(sql, sqlite_con);
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                nametab = "Enterprise";
                ViewTextBox();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlite_con.Close();
            }
        }

        public void ViewProducts()
        {
            SQLiteConnection sqlite_con = new SQLiteConnection("Data Source=OblikTovariv.db");
            const string sql = "select product_id, product_name, product_type, production_year, production_volume, enterprise_id from Products;";
            try
            {
                sqlite_con.Open();
                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(sql, sqlite_con);
                da.Fill(ds);
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                nametab = "Products";
                ViewTextBox();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlite_con.Close();
            }
        }

        public void ViewOrdering ()
        {
            SQLiteConnection sqlite_con = new SQLiteConnection("Data Source=OblikTovariv.db");
            const string sql = "select * from Orders;";
            try
            {
                sqlite_con.Open();
                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(sql, sqlite_con);
                da.Fill(ds);
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                nametab = "Orders";
                ViewTextBox();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlite_con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewProducts();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewOrdering();
        }

        public void ViewTextBox()
        {
            if (nametab == "Products")
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = true;
            }
            if (nametab == "Orders")
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = false;
                textBox6.Visible = false;
            }
            if (nametab == "Enterprise")
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SQLiteConnection sqlite_con = new SQLiteConnection("Data Source=OblikTovariv.db");
            sqlite_con.Open();
            if (nametab == "Enterprise")
            {
                int id, emplCount;
                try
                {
                    id = Convert.ToInt32(textBox1.Text);
                    emplCount = Convert.ToInt32(textBox5.Text);

                    string sql = "INSERT INTO Enterprise (enterprise_id, name, address, phone, employee_count) VALUES(" + id + ",'" + textBox2.Text + "','" + textBox3.Text + "', '" + textBox4.Text + "'," + emplCount + " );";
                    try
                    {
                        DataSet ds = new DataSet();
                        var da = new SQLiteDataAdapter(sql, sqlite_con);
                        da.Fill(ds);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        ViewEnterprise();
                    }
                    catch (Exception es)
                    {
                        sqlite_con.Close();
                        MessageBox.Show("Помилка додавання: " + es.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Помилка конвертації: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                sqlite_con.Close();
            }

            if (nametab == "Products")
            {
                int id, prodyear, prodvol, enterprid;
                try
                {
                    id = Convert.ToInt32(textBox1.Text);
                    prodyear = Convert.ToInt32(textBox4.Text);
                    prodvol = Convert.ToInt32(textBox5.Text);
                    enterprid = Convert.ToInt32(textBox6.Text);
                    string sql = "INSERT INTO Products (product_id, product_name, product_type, production_year, production_volume, enterprise_id) VALUES(" + id + ",'" + textBox2.Text + "','" + textBox3.Text + "', " + prodyear + "," + prodvol + ", " + enterprid + " );";
                    try
                    {
                        DataSet ds = new DataSet();
                        var da = new SQLiteDataAdapter(sql, sqlite_con);
                        da.Fill(ds);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        ViewProducts();
                    }
                    catch (Exception es)
                    {
                        sqlite_con.Close();
                        MessageBox.Show("Помилка додавання: " + es.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Помилка конвертації: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                sqlite_con.Close();
            }

            if (nametab == "Orders")
            {
                int id, prodid, quanti;
                try
                {
                    id = Convert.ToInt32(textBox1.Text);
                    prodid = Convert.ToInt32(textBox3.Text);
                    quanti = Convert.ToInt32(textBox4.Text);

                    string sql = "INSERT INTO Orders (order_id, client, product_id, quantity) VALUES(" + id + ",'" + textBox2.Text + "'," + prodid + "," + quanti + " );";
                    try
                    {
                        DataSet ds = new DataSet();
                        var da = new SQLiteDataAdapter(sql, sqlite_con);
                        da.Fill(ds);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        ViewOrdering();
                    }
                    catch (Exception es)
                    {
                        sqlite_con.Close();
                        MessageBox.Show("Помилка додавання: " + es.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Помилка конвертації: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    sqlite_con.Close();
                }
            }
        }

        private void DeleteRecord(int id)
        {
            SQLiteConnection sqlite_con = new SQLiteConnection("Data Source=OblikTovariv.db");
            try
            {
                sqlite_con.Open();

                string sql;
                if (nametab == "Enterprise")
                {
                    sql = $"DELETE FROM {nametab} WHERE enterprise_id = {id};";
                }
                else if (nametab == "Products")
                {
                    sql = $"DELETE FROM {nametab} WHERE product_id = {id};";
                }
                else
                {
                    sql = $"DELETE FROM {nametab} WHERE order_id = {id};";
                }
                var cmd = new SQLiteCommand(sql, sqlite_con);
                cmd.ExecuteNonQuery();
                if (nametab == "Enterprise")
                {
                    ViewEnterprise();
                }
                else if (nametab == "Products")
                {
                    ViewProducts();
                }
                else
                {
                    ViewOrdering();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlite_con.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                DeleteRecord(selectedId);
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SearchRecord(string searchKeyword)
        {
            string sql;
            SQLiteConnection sqlite_con = new SQLiteConnection("Data Source=OblikTovariv.db");
            try
            {
                sqlite_con.Open();
                if (nametab == "Enterprise")
                {
                    sql = $"SELECT * FROM {nametab} WHERE name LIKE @searchKeyword;";
                }
                else if (nametab == "Products")
                {
                    sql = $"SELECT * FROM {nametab} WHERE product_name LIKE @searchKeyword;";
                }
                else
                {
                    sql = $"SELECT * FROM {nametab} WHERE client LIKE @searchKeyword;";
                }
                var cmd = new SQLiteCommand(sql, sqlite_con);
                cmd.Parameters.AddWithValue("@searchKeyword", $"%{searchKeyword}%");
                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(cmd);
                da.Fill(ds);
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlite_con.Close();
            }
        }

        private void UpdateRecord(int id)
        {
            SQLiteConnection sqlite_con = new SQLiteConnection("Data Source=OblikTovariv.db");
            try
            {
                sqlite_con.Open();
                string sql;
                if (nametab == "Enterprise")
                {
                     sql = $"UPDATE {nametab} SET name = @name, address = @address, phone = @phone, employee_count = @emploecount WHERE enterprise_id = {id};";
                     var cmd = new SQLiteCommand(sql, sqlite_con);
                    cmd.Parameters.AddWithValue("@name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@address", textBox3.Text);
                    cmd.Parameters.AddWithValue("@phone", textBox4.Text);
                    cmd.Parameters.AddWithValue("@emploecount", textBox5.Text);
                    cmd.ExecuteNonQuery();
                    ViewEnterprise();
                }
                else if (nametab == "Products")
                {
                    sql = $"UPDATE {nametab} SET product_name = @productName, product_type = @productType, production_year = @productionYear, production_volume = @productionVolume, enterprise_id = @enterpriseId WHERE product_id = {id};";
                    var cmd = new SQLiteCommand(sql, sqlite_con);
                    cmd.Parameters.AddWithValue("@productName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@productType", textBox3.Text);
                    cmd.Parameters.AddWithValue("@productionYear", Convert.ToInt32(textBox4.Text));
                    cmd.Parameters.AddWithValue("@productionVolume", Convert.ToInt32(textBox5.Text));
                    cmd.Parameters.AddWithValue("@enterpriseId", Convert.ToInt32(textBox6.Text));
                    cmd.ExecuteNonQuery();
                    ViewProducts();
                } else if (nametab == "Orders")
                {
                    sql = $"UPDATE {nametab} SET client = @client, product_id = @productId, quantity = @quantity WHERE order_id = {id};";
                    var cmd = new SQLiteCommand(sql, sqlite_con);
                    cmd.Parameters.AddWithValue("@client", textBox2.Text); 
                    cmd.Parameters.AddWithValue("@productId", Convert.ToInt32(textBox3.Text)); 
                    cmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(textBox4.Text)); 
                    cmd.ExecuteNonQuery();
                    ViewOrdering();
                }
                sqlite_con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlite_con.Close();
            }
        }




        private void button6_Click(object sender, EventArgs e)
        {
            string searchKeyword = textBox7.Text;
            SearchRecord(searchKeyword);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                UpdateRecord(selectedId);
            }
            else
            {
                MessageBox.Show("Please select a row to update.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}