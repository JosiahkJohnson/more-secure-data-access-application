using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace data_access_application
{
    public partial class Form1 : Form
    {

        Controller database;

        public Form1()
        {
            InitializeComponent();
        }

        //connect to the server
        private void button1_Click(object sender, EventArgs e)
        {
            //change to false if authentication fails
            bool validated = true;
            
            //gather information from the user
            string user = textBox1.Text;
            string password = textBox2.Text;
            
            //must have entered information
            if(user.Length == 0 || password.Length == 0)
            {
                validated = false;
                MessageBox.Show("Enter Username and Password");
            } 
            //cleans the input of user so that only known user values are used
            else if(!user.Equals("user_ceo", StringComparison.OrdinalIgnoreCase) && !user.Equals("user_sales", StringComparison.OrdinalIgnoreCase) && !user.Equals("user_hr", StringComparison.OrdinalIgnoreCase))
            {
                validated = false;
                MessageBox.Show(user + " not found in the database.");
            }
            //detect if the password is not long enough to be considered
            else if (password.Length < 6)
            {
                validated = false;
                MessageBox.Show("Password entered is invalid.");
            }
            else
            {
                //final if the password is not all numbers like are required for this program
                long num1 = 0;
                if(!long.TryParse(password, out num1))
                {
                    validated = false;
                    MessageBox.Show("Password must be all numbers.");
                }
            }

            //finally, if the validation is still valid we can try to connect
            if(validated)
            {
                //insert the information gathered from the user to login
                MessageBox.Show("Connection info sent");
                database = new Controller("Server = LAPTOP-1HMCDI96\\SQLEXPRESS;" +
                                      "Database = Northwind;" +
                                      "User Id = " + user +
                                      "; Password = " + password + ";");
            }
            
        }

        //gets the total number of customers
        private void button2_Click(object sender, EventArgs e)
        {
            string count = database.getCustomerNumber();
            if(count != null)
            {
                MessageBox.Show(count + " Customers");
            }

        }

        //gets all the customer names
        private void button3_Click(object sender, EventArgs e)
        {
            string result = database.getCustomerNames();
            if(result != null)
            {
                MessageBox.Show(result);
            }

        }
        //oops
        private void label6_Click(object sender, EventArgs e)
        {

        }

        //gets the total number of employees
        private void button4_Click(object sender, EventArgs e)
        {
            string count = database.getEmployeeNumber();
            if(count != null)
            {
                MessageBox.Show(count + " Employees");
            }
  
        }

        //oops again
        private void label8_Click(object sender, EventArgs e)
        {
            
        }

        //gets the first names of all the employees
        private void button5_Click(object sender, EventArgs e)
        {
            string result = database.getEmployeeNames();
            if (result != null)
            {
                MessageBox.Show(result);
            }
  
        }

        //gets the total number of orders in the system
        private void button6_Click(object sender, EventArgs e)
        {
            string count = database.getOrderNumber();
            if (count != null)
            {
                MessageBox.Show(count + " Orders");
            }
  
        }

        //gets the addresses of the orders in the system
        private void button7_Click(object sender, EventArgs e)
        {
            string result = database.getOrderDetails();
            if (result != null)
            {
                MessageBox.Show("Addresses: \n" + result);
            }
   
        }
    }
}
