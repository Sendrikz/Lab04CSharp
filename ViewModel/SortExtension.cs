using Lab04_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab04_CSharp
{
    public static class SortExtension
    {
        public static readonly string[] SortFiltertOptions =
            Array.ConvertAll(typeof(Person).GetProperties(), (property) => property.Name);

        public static List<Person> SortByProperty(this List<Person> persons, string property, bool ascending)
        {
            return Array.IndexOf(SortFiltertOptions, property) >= 0
                ?
                    (ascending
                        ?
                        persons.OrderBy(p => p.GetType().GetProperty(property)?.GetValue(p, null)).ToList()
                        :
                        persons.OrderByDescending(p => p.GetType().GetProperty(property)?.GetValue(p, null)).ToList()
                    )
                : persons;
        }
        
        public static List<Person> FilterByProperty(this List<Person> persons, string property, string query)
        {
            if (Array.IndexOf(SortFiltertOptions, property) < 0) return new List<Person>();

            query = query.ToLower();
            return (from p in persons
                    where (p.GetType().GetProperty(property)?.GetValue(p, null)).ToString().ToLower().Contains(query)
                    select p).ToList();
        }
    }
}

