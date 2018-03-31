using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiLibreriaSegurola.DL.Entities;

namespace WebApiLibreriaSegurola.BL
{
    public static class WebScraper
    {
        public static Item GetBook(string URI)
        {
            try
            {
                var webGet = new HtmlWeb();
                var document = webGet.Load(URI);
                Item foundItem = new Item();

                HtmlNode rootNode = document.DocumentNode.SelectNodes("//div[contains(@class, 'md-datos')]").FirstOrDefault();

                // ISBN
                foundItem.Isbn = rootNode.Descendants("ul").FirstOrDefault().Descendants("li").ToList()[1].ChildNodes[1].InnerText;

                //TITLE
                foundItem.Title = rootNode.Descendants("h4").FirstOrDefault().Descendants("a").FirstOrDefault().Attributes["title"].Value;

                // PUBLISHER
                foundItem.Publisher = rootNode.Descendants("ul").FirstOrDefault().Descendants("li").ToList()[0].ChildNodes[1].InnerText;

                // PRICE
                string price = document.DocumentNode.SelectNodes("//a[contains(@class, 'comprar papel')]").FirstOrDefault().InnerText;
                if (price != "")
                {
                    foundItem.Price = float.Parse(price.Trim().Split()[1], new CultureInfo("es-ES"));
                }

                return foundItem;
            }
            catch
            {
                throw;
            }
        }
    }
}
