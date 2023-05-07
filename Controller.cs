using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace data_access_application
{
    class Controller
    {
        string connection;
        SqlConnection connect = null;

        public Controller()
        {
            connection = "";
        }
        public Controller(string connect)
        {
            connection = connect;
        }

        public string getCustomerNumber()
        {
            //catching any errors due to authentication so the program keeps running
            try
            {
                int count = 0;
                connect = new SqlConnection(connection);
                connect.Open();

                //Old, less secure method
                //string Query = "SELECT COUNT(*) FROM customers;";

                //new, more secure method
                string Query = "SELECT [dbo].[GetCustomerCount] ();";
                SqlCommand command = new SqlCommand(Query, connect);
                try
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                connect.Close();

                //if the count was 0, then the user was likely not authorized this information
                if(count > 0)
                {
                    return count.ToString();
                } else
                {
                    //return null so the logic in the rest of the code doesn't throw an extra box at the user
                    MessageBox.Show("Unable to access this data.");
                    return null;
                }
                
            } catch
            {
                //same as before, return null since there is no data
                MessageBox.Show("Unable to access this data.");
                return null;
            }
        }

        //get number of employees
        public string getEmployeeNumber()
        {
            int count = 0;
            connect = new SqlConnection(connection);
            connect.Open();

            string Query = "SELECT COUNT(*) FROM employees;";
            SqlCommand command = new SqlCommand(Query, connect);

            
            try
            {
                count = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            //if the result is 0 the user is likely unauthorized
            connect.Close();
            if (count > 0)
            {
                return count.ToString();
            } else
            {
                //return null since there is no data to return
                MessageBox.Show("Unable to access this data.");
                return null;
            }
            
        }

        //get number of orders
        public string getOrderNumber()
        {
            int count = 0;
            connect = new SqlConnection(connection);
            connect.Open();

            string Query = "SELECT COUNT(*) FROM orders;";
            SqlCommand command = new SqlCommand(Query, connect);

            try
            {
                count = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            //0 means the user likely does not have access to this information
            connect.Close();
            if (count > 0)
            {
                return count.ToString();
            } else
            {
                //return null since there is no useful data at this point
                MessageBox.Show("Unable to access this data.");
                return null;
            }
            
        }

        public string getCustomerNames()
        {
            string names = "";
            SqlDataReader reader;
            connect = new SqlConnection(connection);
            connect.Open();
            //keep the script running, un authorized access will throw an exception
            try
            {
                string Query = "SELECT companyname FROM customers;";
                SqlCommand command = new SqlCommand(Query, connect);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        names = names + reader.GetValue(0) + "\n";
                        
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
                connect.Close();
                return names.ToString();
            } catch
            //exception likely is the user is unauthorzied to this data
            {
                MessageBox.Show("Unable to access this data.");
            }

            //return null is the default assuming the user is unauthorized
            connect.Close();
            return null;

        }

        //get the names of all the employees
        public string getEmployeeNames()
        {
            string names = "";
            SqlDataReader reader;
            connect = new SqlConnection(connection);
            connect.Open();
            //main exception catcher for unauthorized users
            try
            {
                string Query = "SELECT firstname FROM employees;";
                SqlCommand command = new SqlCommand(Query, connect);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        names = names + reader.GetValue(0) + "\n";
                        
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
                connect.Close();
                return names.ToString();
            } catch
            {
                //default message is likely to happen if user is unauthorzed to this data
                MessageBox.Show("Unable to access this data.");
            }

            //this will not execute granted a user is authrized
            connect.Close();
            return null;
        }

        //get all order details
        public string getOrderDetails()
        {
            string names = "";
            SqlDataReader reader;
            connect = new SqlConnection(connection);
            connect.Open();

            //catch unauthorized users
            try
            {
                string Query = "SELECT shipaddress FROM orders;";
                SqlCommand command = new SqlCommand(Query, connect);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        names = names + reader.GetValue(0) + "\n";
                        
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
                connect.Close();
                return names.ToString();
            } catch
            {
                //default message for unathorized users that break this script
                MessageBox.Show("Unable to access this data.");
            }
            
            //default return value if authentication fails
            connect.Close();
            return null;
        }
    }
}
