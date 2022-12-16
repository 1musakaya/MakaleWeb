using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BusinessLayer
{
    public class BusinessLayerSonuc<T> where T:class    // t classtır demek istedik yani her kayıt olduğunda bu her şeyi içinde tutabilir kategoriyide notuda gibi
    {
        public List<string> Hatalar { get; set; }

        public T nesne { get; set; }
        public BusinessLayerSonuc()
        {
            Hatalar = new List<string>();
        }
    }
}
