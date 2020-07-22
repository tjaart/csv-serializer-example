using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ReflectionSerializerTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+++ Employee Import +++");

            var emps = Deserialize<ImportedEmployee>("EmployeeImport.csv");
        }

        static void InvokeSomething(Action? doSomething)
        {
            doSomething?.Invoke();
        }

        private static List<TImportType> Deserialize<TImportType>(string fileName)
        {
            var lines = File.ReadLines(fileName);
            var deserializedRecords = new List<TImportType>();

            foreach (var line in lines)
            {
                var splitLine = line.Split(",");

                var properties = typeof(TImportType).GetProperties();
                var emp = Activator.CreateInstance<TImportType>();

                var m = typeof(TImportType).GetMethods().First(c => c.Name == "SingASong");
                m.Invoke(emp, null);

                for (var index = 0; index < properties.Length; index++)
                {
                    var propertyInfo = properties[index];

                    var ignoredAttribute =
                        (CsvIgnoreAttribute) propertyInfo.GetCustomAttribute(typeof(CsvIgnoreAttribute));

                    if (ignoredAttribute != null)
                    {
                        continue;
                    }

                    Console.WriteLine(propertyInfo.Name);

                    var value = splitLine[index];

                    if (propertyInfo.PropertyType == typeof(decimal))
                    {
                        var decimalValue = decimal.Parse(value);
                        propertyInfo.SetValue(emp, decimalValue);
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        var dateTimeValue = DateTime.Parse(value);
                        propertyInfo.SetValue(emp, dateTimeValue);
                    }
                    else
                    {
                        propertyInfo.SetValue(emp, value);
                    }
                }

                deserializedRecords.Add(emp);
            }

            return deserializedRecords;
        }
    }
}