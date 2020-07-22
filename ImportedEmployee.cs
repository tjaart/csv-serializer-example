using System;

namespace ReflectionSerializerTutorial
{
    public class ImportedEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        
        [CsvIgnore(Why = "just because")]
        public string Placeholder { get; set; }

        public void SingASong()
        {
            Console.WriteLine("sing");
        }
    }
}