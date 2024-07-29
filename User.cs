using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactTracker
{
    public class User
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public User(string lastName, string firstName, string email, string phone)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Phone = phone;
        }

        public override string ToString()
        {
            return $"{LastName}, {FirstName}, {Email}, {Phone}";
        }
        public static bool ValidateEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool ValidatePhone(string phone)
        {
            string phonePattern = @"^[\d\-\s]+$";
            return Regex.IsMatch(phone, phonePattern);
        }

        public static bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
    }
}
