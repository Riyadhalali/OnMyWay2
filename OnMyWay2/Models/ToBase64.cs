using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OnMyWay2.Models
{
    public class ToBase64
    {
        public System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
    }
}