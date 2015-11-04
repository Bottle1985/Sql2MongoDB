using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
// Insert Mongo
using System.Threading.Tasks;
// Mongo DB
using MongoDB.Bson;
using MongoDB.Driver;


namespace SQL_MongoDB
{
    public partial class Form1 : Form
    {
        private SqlConnection con;
        private DataTable dt = new DataTable("dtCustomer");
        private SqlDataAdapter da = new SqlDataAdapter();
        private void connect()
        {
            String cn = "Data Source=(local)\\SQLEXPRESS; Initial Catalog=Northwind; Persist Security Info=True; User ID=Boong;Password=77k1636";
          
            try
            {
                con = new SqlConnection(cn);
                con.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void disconnect()
        {
            con.Close();
            con.Dispose();
            con = null;
        }
        private void getData()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            //// Dùng câu lệnh
            //command.CommandType = CommandType.Text;
            //command.CommandText = @"Select * from Customers";
            //da.SelectCommand = command;
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Bottle";
            SqlDataReader daReader = command.ExecuteReader();
            dt.Load(daReader);
            dataGridView1.DataSource = dt;

        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connect();
            getData();
            binding();
            disconnect();
        }
        private void binding()
        {
            textBox1.DataBindings.Clear();
            textBox1.DataBindings.Add("Text", dataGridView1.DataSource,"LastName");
            textBox2.DataBindings.Clear();
            textBox2.DataBindings.Add("Text", dataGridView1.DataSource, "FirstName");
            textBox3.DataBindings.Clear();
            textBox3.DataBindings.Add("Text", dataGridView1.DataSource, "Title");
            textBox4.DataBindings.Clear();
            textBox4.DataBindings.Add("Text",dataGridView1.DataSource,"EmployeeID");
            textBox5.DataBindings.Clear();
            textBox5.DataBindings.Add("Text", dataGridView1.DataSource, "BirthDate");
            textBox6.DataBindings.Clear();
            textBox6.DataBindings.Add("Text", dataGridView1.DataSource, "HireDate");

        }
        // MongoDB
        //protected  static IMongoClient _client;
        //protected  static IMongoDatabase _database;

        //_client = new MongoClient();
        //_database = _client.GetDatabase("test");
        private void connect_MG()
        {
            MongoServer mongo = MongoServer.Create();
            //MongoServer mongo = MongoClient.GetServer();
         
            mongo.Connect();

            var db = mongo.GetDatabase("test");
        }
        private void insert_MongoDB()
        {
            MongoServer mongo = MongoServer.Create();
      
            mongo.Connect();

            var db = mongo.GetDatabase("test");
            var document = new BsonDocument
            {
                { "address" , new BsonDocument
                    {
                        { "street", "2 Avenue" },
                        { "zipcode", "10075" },
                        { "building", "1480" },
                        { "coord", new BsonArray { 73.9557413, 40.7720266 } }
                    }
                },
                { "borough", "Manhattan" },
                { "cuisine", "Italian" },
                { "grades", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "date", new DateTime(2014, 10, 1, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "A" },
                            { "score", 11 }
                        },
                        new BsonDocument
                        {
                            { "date", new DateTime(2014, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "B" },
                            { "score", 17 }
                        }
                    }
                },
                { "name", "Vella" },
                { "restaurant_id", "41704620" }
            };

            var collection = db.GetCollection<BsonDocument>("restaurants");
            collection.Insert(document);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insert_MongoDB();
        }
        private void find_mongoDB()
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

