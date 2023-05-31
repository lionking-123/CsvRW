using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Text;

#nullable disable

public class Program
{
    public static void Main(string[] args)
    {
        // initialize DataTable
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("FirstName");
        dataTable.Columns.Add("LastName");
        dataTable.Columns.Add("JoinedDate");
        dataTable.Columns.Add("Salary");
        dataTable.Columns.Add("Active");

        // Get filenames for all files in target directory/folder
        var files = Directory.EnumerateFiles("./", "*.csv");

        // read data from all files and save it on dataTable
        foreach (string file in files)
        {
            DataTable tmpTb = new DataTable();
            using (TextFieldParser parser = new TextFieldParser(file))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                string[] headers = parser.ReadLine().Split(';');
                foreach (string header in headers)
                {
                    tmpTb.Columns.Add(header, typeof(string));
                }

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    tmpTb.Rows.Add(fields);
                }

                foreach(DataRow row in tmpTb.Rows)
                {
                    dataTable.Rows.Add(row["FirstName"], row["LastName"], row["JoinedDate"], row["Salary"], row["Active"]);
                }
            }
        }

        // write dataTable to CSV file
        StringBuilder sb = new StringBuilder();
        DateTime dateTime = DateTime.Now;

        IEnumerable<string> columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
        sb.AppendLine(string.Join(";", columnNames));

        foreach (DataRow row in dataTable.Rows)
        {
            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
            sb.AppendLine(string.Join(";", fields));
        }


        File.WriteAllText($"Tracking{dateTime.Year}{dateTime.Month}{dateTime.Day}_{dateTime.Hour}{dateTime.Minute}{dateTime.Second}.csv", sb.ToString());
    }
}