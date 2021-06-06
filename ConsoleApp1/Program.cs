using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    /// <summary>
    /// This program takes a string of json data and creates insert statements for sql server 
    /// i created this to easily insert data into table when leetcode give you data in json format.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            List<Table> tables = new List<Table>();
            using (StreamReader reader = new StreamReader(@"C:\Users\psjob\OneDrive\Desktop\json.json"))
            {
                string json = reader.ReadToEnd();
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                foreach (var kv in dict)
                {
                    if (kv.Key.ToString().ToLower().Trim() == "headers")
                    {
                        var x = JsonConvert.DeserializeObject<Dictionary<object, object>>(kv.Value.ToString());
                        foreach (var a in x)
                        {
                            Table table = new Table() { Name = a.Key.ToString(), Columns = new List<string>() };
                            var b = a.Value.ToString().Remove(0, 1).Remove(a.Value.ToString().Length - 2, 1).ToString().Trim();
                            b = Regex.Replace(b, @"\t|\n|\r|\""", "");
                            var bb = b.Split(',').Select(xx => xx.Trim()).ToArray();
                            foreach (var cc in bb)
                            {
                                table.Columns.Add(cc);
                            }
                            tables.Add(table);
                        }
                    }
                    if (kv.Key.ToString().ToLower().Trim() == "rows")
                    {
                        var x = JsonConvert.DeserializeObject<Dictionary<object, object>>(kv.Value.ToString());
                        foreach (var y in x)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(y.Value.ToString());
                            sb.Remove(0, 1);
                            sb.Remove(sb.Length - 1, 1);
                            sb.Replace('[', '(');
                            sb.Replace(']', ')');
                            sb.Replace('"', '\'');
                            tables.Find(t => t.Name == y.Key.ToString()).Rows = Regex.Replace(sb.ToString(), @"\t|\n|\r|\""", "");
                        }
                    }
                }
            }


            StringBuilder commandText = new StringBuilder();
            foreach(var table in tables)
            {
                StringBuilder sb = new StringBuilder();
                foreach(var col in table.Columns)
                {
                    sb.Append($@"{col},");
                }
                sb.Remove(sb.Length - 1, 1);
                commandText.Append($@" insert into {table.Name} ({sb}) values {table.Rows} ; ");
            }

            Console.WriteLine(commandText.ToString());

            Console.Read();
        }
    }

    class Table {
        public string Name { get; set; }
        public List<string> Columns { get; set; }
        public string Rows { get; set; }
    }

}
