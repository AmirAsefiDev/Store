using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.HomePages
{
    public class HomePageImage:BaseEntity
    {
        public string Src { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }

    public enum ImageLocation
    {
        L1=0,
        L2=1,
        R3=2,
        CenterFullScreen=4,
        G1=5,
        G2=6,
    }
}
