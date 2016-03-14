using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace ExportApp
{

    public class ExportCSV

        
    {


        private string GetPropertyValueAsString(object propertyValue)
        {
            string propertyValueString;

            if (propertyValue == null)
                propertyValueString = "";
            else if (propertyValue is DateTime)
                propertyValueString = ((DateTime)propertyValue).ToString("yyyy-MM-dd");
            else if (propertyValue is int)
                propertyValueString = propertyValue.ToString();
            else if (propertyValue is float)
                propertyValueString = ((float)propertyValue).ToString("#.####"); // format as you need it
            else if (propertyValue is double)
                propertyValueString = ((double)propertyValue).ToString("#.####"); // format as you need it
            else // treat as a string
                propertyValueString = @"""" + propertyValue.ToString().Replace(@"""", @"""""") + @""""; // quotes with 2 quotes

            return propertyValueString;
        }




        public string GetCSV<T>(List<T> list)
        {
            StringBuilder sb = new StringBuilder();

            //Get the properties for type T for the headers
            PropertyInfo[] propInfos = typeof(T).GetProperties();
            for (int i = 0; i <= propInfos.Length - 1; i++)
            {
                sb.Append(propInfos[i].Name);

                if (i < propInfos.Length - 1)
                {
                    sb.Append(",");
                }
            }

            sb.AppendLine();

            //Loop through the collection, then the properties and add the values
            for (int i = 0; i <= list.Count - 1; i++)
            {
                T item = list[i];
                for (int j = 0; j <= propInfos.Length - 1; j++)
                {
                    object o = item.GetType().GetProperty(propInfos[j].Name).GetValue(item, null);
                    if (o != null)
                    {
                        string value = GetPropertyValueAsString(o);

                        //Check if the value contans a comma and place it in quotes if so
                        if (value.Contains(","))
                        {
                            value.Replace(",", "-");
                            //value = string.Concat("\"", value, "\"");
                        }

                        //Replace any \r or \n special characters from a new line with a space
                        if (value.Contains("\r"))
                        {
                            value = value.Replace("\r", " ");
                        }
                        if (value.Contains("\n"))
                        {
                            value = value.Replace("\n", " ");
                        }

                        sb.Append(value);
                    }

                    if (j < propInfos.Length - 1)
                    {
                        sb.Append(",");
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }




        public bool CreatingCsvFiles<T>(List<T> listToExport, string fileName)
        {
            string filepath = @"X:\Work Desk\CICON\CSV\";
            string FullfileName = filepath + fileName + ".csv";
            if (!File.Exists(FullfileName))
            {
                File.Create(FullfileName).Close();
            }
            string _dateStr =  GetCSV(listToExport);

            File.WriteAllText(FullfileName, _dateStr, Encoding.UTF8);
            return true;
        }

    }

}
