using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsoleTheTeaStory
{
    public class Helper
    {
        private static string LogsPath = new DirectoryInfo(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\ConsoleTheTeaStory\Logs"))).ToString();

        public static string RandomString(int size, bool lowerCase = false)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();

            return random.Next(min, max);
        }

        public static IWebElement WaitUntilElementExists(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementExists(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        public static bool InsertToClients(string name, string lastname, string email, string programm)
        {
            bool result = false;

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ivan.vasquez\source\repos\TheTeaStory\ConsoleTheTeaStory\Resources\Database1.mdf;Integrated Security=True");
            string query = $"INSERT INTO [Clients] values('{name}', '{lastname}', '{email}', '{programm}')";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Connection.Open();
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
                result = true;

            cmd.Connection.Close();

            return result;
        }

        public static IWebElement WaitUntilElementVisible(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
                throw;
            }
        }

        public static IWebElement WaitUntilElementClickable(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        public static void WaitForElementLoad(IWebDriver driver, By by, int timeoutInSeconds)
        {
            if(timeoutInSeconds > 0)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
                {
                    return d.FindElement(by);
                });
            }
        }

        public static void ClickAndWaitForPageToLoad(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeout));
                var element = Driver.Instance.FindElement(elementLocator);
                element.Click();
                wait.Until(ExpectedConditions.StalenessOf(element));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        public static void LogErrors(string customMessage)
        {
            var logPathName = LogsPath + @"\testLogs.txt";
            try
            {
                if(File.Exists(logPathName))
                {
                    using (var writer = new StreamWriter(logPathName, true))
                    {
                        writer.Write($"{DateTime.Now} \n {customMessage} \n");
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(logPathName))
                    {
                        writer.Write($"{DateTime.Now} \n {customMessage} \n");
                    }
                }
                
            }catch(Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} / {ex.Message} \n Error at moment to write in file");
            }
        }

        public static void TakeErrorScreenshot()
        {
            var logPathName = LogsPath + @"\ErrorScreenshot_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            ((ITakesScreenshot)Driver.Instance).GetScreenshot().SaveAsFile(logPathName, ScreenshotImageFormat.Png);
        }

        public static void WaitForElement(IWebElement element, double time)
        {
            DefaultWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromMinutes(time);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            Func<IWebElement, bool> waiter = new Func<IWebElement, bool>((IWebElement ele) =>
            {
                if(element.Displayed)
                {
                    return true;
                }
                return false;
            });

            wait.Until(waiter);
        }

        public static void GetData()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ivan.vasquez\source\repos\TheTeaStory\ConsoleTheTeaStory\Resources\Database1.mdf;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM CLIENTS", cnn);

            SqlDataReader dataReader = command.ExecuteReader();


            /*var dataTable = new DataTable();
            dataTable.Load(dataReader);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dataTable);*/


            var r = Serialize(dataReader);
            string json = JsonConvert.SerializeObject(r, Formatting.Indented);


            /*DataTable dt = new DataTable();
            dt.Load(dataReader);
            output = DataTableToJSONWithStringBuilder(dt);
            Console.WriteLine(DataTableToJSONWithStringBuilder(dt));*/


            cnn.Close();
        }

        public static string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }

        public static IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                cols.Add(reader.GetName(i));
            }

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }

        private static Dictionary<string, object> SerializeRow(IEnumerable<string> cols, SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach(var col in cols)
            {
                result.Add(col, reader[col]);
            }

            return result;
        }

    }
}
