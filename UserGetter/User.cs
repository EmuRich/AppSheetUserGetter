using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGetter
{
    public class User
    {
        /// <summary>
        /// The numerical ID corresponding to this person
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// The name of the person
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The numerical age of the person
        /// </summary>
        public int age { get; set; }
        /// <summary>
        /// The string format of the phone number of the person
        /// </summary>
        public string number { get; set; }

        public string ToString()
        {
            return "{ ID: " + id + ", Name: " + name + ", Age: " + age + ", Number: " + number + " }";
        }
    }
}
