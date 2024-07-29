namespace ContactTracker
{
    class Program
    {
        static SortedDictionary<string, User> userTree = new SortedDictionary<string, User>();

        static void Main()
        {
            LoadData("MOCK_DATA.csv");
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Options: 1. Add User 2. Delete User 3. Search User 4. Exit");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddUser();
                        break;
                    case "2":
                        DeleteUser();
                        break;
                    case "3":
                        SearchUser();
                        break;
                    case "4":
                        SaveData("MOCK_DATA.csv");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void LoadData(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    sr.ReadLine(); // Skip header
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var data = line.Split(',');
                        var user = new User(data[0], data[1], data[2], data[3]);
                        userTree.Add(data[3], user); // Key is phone number
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error loading data: {e.Message}");
            }
        }

        static void SaveData(string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("last_name,first_name,email,phone");
                    foreach (var user in userTree.Values)
                    {
                        sw.WriteLine($"{user.LastName},{user.FirstName},{user.Email},{user.Phone}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error saving data: {e.Message}");
            }
        }

        static void AddUser()
        {
            try
            {
                Console.WriteLine("Enter last name:");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter first name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter phone:");
                string phone = Console.ReadLine();

                if (!User.ValidateName(lastName) || !User.ValidateName(firstName))
                {
                    Console.WriteLine("Invalid name. Please enter a valid name.");
                    return;
                }

                if (!User.ValidateEmail(email))
                {
                    Console.WriteLine("Invalid email format. Please enter a valid email.");
                    return;
                }

                if (!User.ValidatePhone(phone))
                {
                    Console.WriteLine("Invalid phone number format. Please enter a valid phone number.");
                    return;
                }

                if (userTree.ContainsKey(phone))
                {
                    Console.WriteLine("A user with this phone number already exists.");
                    return;
                }

                var user = new User(lastName, firstName, email, phone);
                userTree.Add(phone, user);
                Console.WriteLine("User added.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding user: {e.Message}");
            }
        }

        static void DeleteUser()
        {
            Console.WriteLine("Enter phone number of user to delete:");
            string phone = Console.ReadLine();

            if (userTree.Remove(phone))
            {
                Console.WriteLine("User deleted.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        static void SearchUser()
        {
            Console.WriteLine("Search by 1. Last Name 2. First Name");
            string choice = Console.ReadLine();
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine().ToLower();

            List<User> results = new List<User>();

            foreach (var user in userTree.Values)
            {
                if ((choice == "1" && user.LastName.ToLower().Contains(name)) ||
                    (choice == "2" && user.FirstName.ToLower().Contains(name)))
                {
                    results.Add(user);
                }
            }

            if (results.Count > 0)
            {
                Console.WriteLine("Search Results:");
                foreach (var user in results)
                {
                    Console.WriteLine(user);
                }
            }
            else
            {
                Console.WriteLine("No users found.");
            }
        }
    }
}
