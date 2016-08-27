using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Net;

namespace UserGetter
{

    //https://numverify.com/: possible DLL for actual phone verification???
    public class Program
    {

        public static string ServiceEndpoint = "https://appsheettest1.azurewebsites.net/sample/";

        public static void Main(string[] args)
        {

            List<User> ValidUserMasterList = GetValidUserList();

            ValidUserMasterList = ValidUserMasterList.OrderBy(x => x.age).ToList<User>();

            Console.WriteLine("-------------\nList of all users: \n");

            foreach (User u in ValidUserMasterList)
            {
                Console.WriteLine(u.ToString());
            }

            List<User> YoungestList = new List<User>();

            if (ValidUserMasterList.Count > 5)
            {
                YoungestList = ValidUserMasterList.Take(5).ToList<User>();
            } else
            {
                YoungestList = ValidUserMasterList;
            }
            

            YoungestList = YoungestList.OrderBy(x => x.name).ToList<User>();

            Console.WriteLine("-------------\nYoungest users with valid telephone numbers, in alphabetical order: \n");

            foreach (User u in YoungestList)
            {
                Console.WriteLine("* " + u.ToString());
            }




        }
        /// <summary>
        /// Returns a list of User objects that have valid telephone numbers
        /// </summary>
        /// <returns>The list of Users returned is in an arbitrary order</returns>
        public static List<User> GetValidUserList()
        {
            List<User> UserList = new List<User>();

            string token = null;

            //Get the next user list until there are no additional partial user lists available
            using (WebClient Wc = new WebClient())
            {
                do
                {

                    //Construct the correct URL depending on the token value
                    string appendedLink = "list";

                    if (token == null) //To get the process started for the first run through
                    {
                        appendedLink += "/";
                    }
                    else //When an additional token was offered
                    {
                        appendedLink += "?token=" + token;
                    }

                    //Can be async later
                    string Result = Wc.DownloadString(ServiceEndpoint + appendedLink);

                    PartialUserList temp = JsonConvert.DeserializeObject<PartialUserList>(Result);

                    //Record the new token, if there is one
                    token = temp.token;

                    //Record the people objects
                    foreach (int id in temp.result)
                    {
                        appendedLink = "detail/" + id;

                        Result = Wc.DownloadString(ServiceEndpoint + appendedLink);

                        User tempUser = JsonConvert.DeserializeObject<User>(Result);

                        if (HasValidTelephoneNumber(tempUser))
                        {
                            UserList.Add(tempUser);
                        }

                    }

                }
                while (token != null);
            }
            return UserList;
        }
        /// <summary>
        /// Returns whether the user's telephone number is valid or invalid.
        /// 
        /// Note: we will assume that a phone number is valid if and only if we have 10 digits somewhere within the phone number.
        /// </summary>
        /// <param name="u">The user we inquire about</param>
        /// <returns>Returns true if phone number is valid; false if phone number is not valid</returns>
        public static bool HasValidTelephoneNumber(User u)
        {
            string unformattedPhoneNumber = u.number;

            var sb = new StringBuilder();

            int digitCount = 0;

            foreach (char c in unformattedPhoneNumber)
            {
                if (char.IsDigit(c))
                {
                    sb.Append(c);
                    digitCount++;
                }
            }

            u.number = sb.ToString();

            return (digitCount == 10);
        }
    }
}
