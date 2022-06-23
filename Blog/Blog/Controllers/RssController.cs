using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blog.Controllers
{
    public class RssController : Controller
    {
        private readonly FeedContext _context;

        public RssController(FeedContext context)
        {
            _context = context;
        }

        /// <summary>
        /// A6 - 
        [HttpPost]
        public async Task<IActionResult> CreateFeed()
        {
            string content = string.Empty;
            using (Stream receiveStream = HttpContext.Request.Body)
            {
                using (StreamReader reader = new StreamReader(receiveStream))
                {
                    content = reader.ReadToEnd();
                }
            }

            var entry = JsonConvert.DeserializeObject<Feed>(content, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto 
            });

            _context.Add(entry);
            await _context.SaveChangesAsync();

            return Ok();

        }


        /// A4 -
        [HttpGet]
        public IActionResult LoadFeed()
        {
  
            string xml = "<?xml version=\"1.0\" encoding=\"ISO - 8859 - 1\"?>" +
                "<!DOCTYPE foo [" +
                "<!ELEMENT foo ANY >" +
                "<!ENTITY xxe SYSTEM \"file:///C:/users/bdinger/documents/test.txt\" >]><foo>&xxe;</foo>";

            XmlTextReader xmlReader = new XmlTextReader(new StringReader(xml));
            xmlReader.DtdProcessing = DtdProcessing.Parse;
            var result = string.Empty;
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    result = xmlReader.ReadElementContentAsString();
                }
            }

            return Ok(result);
        }

        /// <summary>
        /// A4 - 
        public ActionResult Feed()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"ISO - 8859 - 1\"?>" +
                         "<!DOCTYPE foo [" +
                         "<!ELEMENT foo ANY >" +
                         "<!ENTITY xxe SYSTEM \"file:///C:/users/bdinger/documents/test.txt\" >]><foo>&xxe;</foo>";

            XmlTextReader xmlReader = new XmlTextReader(new StringReader(xml));
            xmlReader.DtdProcessing = DtdProcessing.Prohibit;
            var result = string.Empty;
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    result = xmlReader.ReadElementContentAsString();
                }
            }

            return Ok(result);
        }
    }
}
