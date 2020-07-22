using System;

namespace ReflectionSerializerTutorial
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CsvIgnoreAttribute : System.Attribute
    {
        public string Why { get; set; }
    }
}