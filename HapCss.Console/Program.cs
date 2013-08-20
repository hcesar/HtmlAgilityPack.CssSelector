using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HapCss
{
    static class Program
    {
        static void Main(string[] args)
        {
            HtmlNode node;
            IList<HtmlAgilityPack.HtmlNode> nodes;
            //var tokens = Tokenizer.GetTokens(".class #id1 div.class2").ToList();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load("linkedin.html");

            nodes = doc.QuerySelectorAll("*");
            node = nodes.First(i => i.GetIndexOnParent() == nodes.Max(n => n.GetIndexOnParent()));

            for (int it = 0; it < 10; it++)
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                int max = nodes.Max(n => n.GetIndexOnParent());
                node = nodes.First(i => i.GetIndexOnParent() == max);
                Console.WriteLine(sw.ElapsedMilliseconds);
            }


            System.Environment.Exit(0);

            var profile = new Profile();
            profile.Name = doc.QuerySelectorAll(".full-name").text();
            profile.LinkedInUrl = profile.Url = string.Concat("http://", doc.QuerySelectorAll(".public-profile dd span").text());
            profile.Picture = doc.QuerySelectorAll(".profile-picture img").src();
            profile.Email = (doc.QuerySelectorAll("#email li a").attr("href") ?? "").Replace("mailto:", "");
            profile.Phones = doc.QuerySelectorAll("#phone li").mapArray();
            profile.BirthDate = doc.QuerySelectorAll("#personal-info-view tr:nth-child(1) td").text();
            profile.MaritalStatus = doc.QuerySelectorAll("#personal-info-view tr:nth-child(2) td").text();
            profile.Location = doc.QuerySelectorAll("a[name=location]").text();
            profile.Skills = doc.QuerySelectorAll(".skill-pill .endorse-item-name-text").mapArray();
            profile.Summary = doc.QuerySelectorAll(".background-summary .description").text();
            profile.Experience = doc.QuerySelectorAll("#background-experience .editable-item").mapArray(getExperience);
            //profile.Education = doc.QuerySelectorAll("#background-education > div").mapArray(getEducation);
            //profile.Languages = doc.QuerySelectorAll("#languages .section-item").mapArray(getLanguage);

        }


        static object getExperience(HtmlNode element)
        {
            string Title = element.QuerySelectorAll("div h4 a").text();
            string Company = element.QuerySelectorAll("div h5:last-child a").text();
            string Description = element.QuerySelectorAll(".description").text();
            string StartDate = element.QuerySelectorAll(".experience-date-locale time:nth-child(1)").text();
            string EndDate = element.QuerySelectorAll(".experience-date-locale time:nth-child(2)").text();
            return new { Title, Company, Description, StartDate, EndDate };
        }

        static string text(this IList<HtmlNode> nodes)
        {
            return nodes.select(i => i.InnerText);
        }

        static string src(this IList<HtmlNode> nodes)
        {
            return nodes.select(i => i.Attributes.Contains("src") ? i.Attributes["src"].Value : null);
        }
        static string attr(this IList<HtmlNode> nodes, string attribute)
        {
            return nodes.select(i => i.Attributes.Contains(attribute) ? i.Attributes[attribute].Value : null);
        }

        static string select(this IList<HtmlNode> nodes, Func<HtmlNode, object> selector)
        {
            if (nodes.Count == 0)
                return string.Empty;
            if (nodes.Count == 1)
                return (selector(nodes[0]) ?? string.Empty).ToString();

            StringBuilder sb = new StringBuilder();
            foreach (var n in nodes)
                sb.AppendLine((selector(n) ?? string.Empty).ToString());
            return sb.ToString();
        }

        static string[] mapArray(this IList<HtmlNode> nodes)
        {
            return nodes.mapArray(i => i.InnerText);
        }

        static T[] mapArray<T>(this IList<HtmlNode> nodes, Func<HtmlNode, T> selector)
        {
            return nodes.Select(i => selector(i)).ToArray();
        }
    }

    class Profile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string LinkedInUrl { get; set; }
        public string Picture { get; set; }
        public string BirthDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }

        public string[] Skills { get; set; }
        public string[] Phones { get; set; }

        public object[] Experience { get; set; }
    }
}

