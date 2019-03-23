using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_CSharp.Models
{
    class PersonGenerator
    {
        public static List<Person> getGeneratedPersons(int count)
        {
            var persons = new List<Person>();
            var random = new Random();

            for (var i = 0; i < count; ++i)
            {
                var name = Names[random.Next(Names.Length)];
                var surname = Surnames[random.Next(Surnames.Length)];

               
                persons.Add(new Person(name, surname,
                    $"{name}.{surname}@gmail.com",
                   new DateTime(
                            random.Next(DateTime.Today.Year - 50, DateTime.Today.Year - 1),
                            random.Next(1, 13), random.Next(1, 30))));
            }
            
            return persons;
        }



        private static readonly string[] Names =
        {
                "Oliver",
                "Harry",
                "George",
                "Jack",
                "Jacob",
                "Noah",
                "William",
                "James",
                "Leo",
                "Theo",
                "Theodore",
                "David",
                "Olha",
                "Sasha",
                "Emily",
                "Carla",
                "Julia",
                "Margaret"
            };



        private static readonly string[] Surnames =
        {
                "Smith",
                "Johnson",
                "Williams",
                "Jones",
                "Brown",
                "Davis",
                "Miller",
                "Wilson",
                "Moore",
                "Taylor",
                "Anderson",
                "Thompson",
                "Collins",
                "Horan"

            };


    private static readonly string[] WesternZodiaсs =
    {
            "Ram",
            "Bull",
            "Twins",
            "Crab",
            "Lion",
            "Virgin",
            "Scales",
            "Scorpion",
            "Archer",
            "Mountain Sea-Goat",
            "Water Bearer",
            "Fish"
        };



    private static readonly string[] ChineseZodiaсs = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };

    }
}
