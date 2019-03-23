using Lab04_CSharp.Exceptions;
using Lab04_CSharp.Properties;
using Lab04_CSharp.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace Lab04_CSharp.Models
{
    [Serializable]
    class Person
    {

        public Person(string name, string surname, string email, DateTime birthday)
        {
            
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                throw new InvalidFormatEmailException("Format of your email is wrong", email);
            }
            
            /**
            if (Age < 0)
            {
                throw new BirthdateInFutureException("There was a mistake. You have not been born yet", Age);
            }
            else if (Age > 135)
            {
                throw new BirthdateInPastException("There was a mistake. Your age is above 135", Age);
            }
    */
            Name = name;
            Surname = surname;
            Email = email;
            Birthday = birthday;
        }

        public Person(string name, string surname, string email) : this(name, surname, email, DateTime.Today)
        {
        }

        public Person(string name, string surname, DateTime birthday) : this(name, surname, "default", birthday)
        {
        }

        public string Name { get; private set; }
        
        public string Surname { get; private set; }
       
        public string Email { get; private set; }
        
        public DateTime Birthday { get; private set; }

        public Boolean IsAdult
        {
            get
            {
                if (Age >= 18)
                    return true;
                return false;
            }
        }

        public string SunSign
        {
            get
            {
                int moth = Birthday.Month;
                int day = Birthday.Day;
                switch (moth)
                {
                    case 1:
                        if (day <= 19)
                            return "Capricorn";
                        else
                            return "Aquarius";

                    case 2:
                        if (day <= 18)
                            return "Aquarius";
                        else
                            return "Pisces";
                    case 3:
                        if (day <= 20)
                            return "Pisces";
                        else
                            return "Aries";
                    case 4:
                        if (day <= 19)
                            return "Aries";
                        else
                            return "Taurus";
                    case 5:
                        if (day <= 20)
                            return "Taurus";
                        else
                            return "Gemini";
                    case 6:
                        if (day <= 20)
                            return "Gemini";
                        else
                            return "Cancer";
                    case 7:
                        if (day <= 22)
                            return "Cancer";
                        else
                            return "Leo";
                    case 8:
                        if (day <= 22)
                            return "Leo";
                        else
                            return "Virgo";
                    case 9:
                        if (day <= 22)
                            return "Virgo";
                        else
                            return "Libra";
                    case 10:
                        if (day <= 22)
                            return "Libra";
                        else
                            return "Scorpio";
                    case 11:
                        if (day <= 21)
                            return "Scorpio";
                        else
                            return "Sagittarius";
                    case 12:
                        if (day <= 21)
                            return "Sagittarius";
                        else
                            return "Capricorn";
                }
                return "";
            }
        }

        public string ChineseSign
        {
            get
            {
                System.Globalization.EastAsianLunisolarCalendar cc = new System.Globalization.ChineseLunisolarCalendar();
                int sexagenaryYear = cc.GetSexagenaryYear(Birthday);
                int terrestrialBranch = cc.GetTerrestrialBranch(sexagenaryYear);

                string[] years = new string[] { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };

                return years[terrestrialBranch - 1];
            }
        }

        public Boolean IsBirthday
        {
            get
            {
                if (Birthday.Day == DateTime.Today.Day)
                    return true;
                return false;
            }
        }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - Birthday.Year;
                if (Birthday > today.AddYears(-age)) age--;

                return age;
            }
        }
    }
}
