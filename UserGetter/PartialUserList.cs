using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGetter
{
    public class PartialUserList
    {
        /// <summary>
        /// A partial list of integer IDs of people in the data set
        /// </summary>
        public int[] result { get; set; }
        /// <summary>
        /// The optional token pointing towards the next partial list
        /// </summary>
        public string token { get; set; }
    }
}
