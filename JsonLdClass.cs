using System;
using System.Collections.Generic;
using System.Text;

public static class ClsJson
{
    public static string SearchAction(string text, string searchUrl, string websiteUrl)
    {
        return "<script type=\"application/ld+json\">{"
         + "\"@context\":\"http://schema.org\"," + "\"@type\":\"WebSite\"," +
            "\"url\" : " + "\"" + websiteUrl + "\"," +
            "\"potentialAction\" : {" + "\"@type\":\"SearchAction\"," +
             "\"query\" : " + "\"" + text + "\"," +
             "\"target\" : " + "\"" + websiteUrl + "/" + searchUrl + "{query}" + "\"," +
             "\"query-input\" : " + "\"required name=query\"}}</script>";
    }
    public static string ProductJson(string productName, string image, string brand, string productUrl,
         string owner, string category, string keywords, string description, string priceRials, Dictionary<string, string> additionalProperties)
    {
        StringBuilder str = new StringBuilder();
        str.Append("<script type=\"application/ld+json\">");
        str.Append("{\"@context\": \"https://schema.org\",");
        str.Append("\"@type\": \"Product\",");
        str.Append("\"url\" : \"" + productUrl + "\",");
        str.Append("\"owns\" : \"" + owner + "\",");
        str.Append("\"brand\" : \"" + brand + "\",");
        str.Append("\"category\" : \"" + category + "\",");
        str.Append("\"keywords\" : \"" + keywords + "\",");
        str.Append("\"description\" : \"" + description + "\",");
        str.Append("\"name\" : \"" + productName + "\",");
        str.Append("\"image\" : \"" + image + "\",");
        str.Append("\"offers\": {\"@type\": \"Offer\",");
        str.Append("\"availability\": \"https://schema.org/InStock\",");
        str.Append("\"price\": \"" + priceRials + "\",");
        str.Append("\"priceCurrency\": \"IRR\"},");
        str.Append("\"additionalProperty\": [");
        foreach (var property in additionalProperties)
        {
            str.Append("{");
            str.Append($"\"@type\": \"PropertyValue\",");
            str.Append($"\"name\": \"{property.Key}\",");
            str.Append($"\"value\": \"{property.Value}\"");
            str.Append("},");
        }
        if (additionalProperties.Count > 0)
        {
            str.Length -= 1;
        }
        str.Append("]");
        str.Append("}</script>");

        return str.ToString();
    }

     public static string ProductAggrigateJson(string productName, string description, string image,
            string[] offerUrl, string lowPrice, string highPrice, string logo, string keywords,
            string manufacturer, string releaseDate, string slogan, string alternateName, string disambiguatingDescription)
    {
        if (offerUrl.Length > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"application/ld+json\">{");
            sb.Append("\"@context\": \"https://schema.org\",");
            sb.Append("\"@type\": \"Product\",");
            sb.Append("\"description\": \"" + description + "\",");
            sb.Append("\"countryOfAssembly\": \"IRAN\",");
            sb.Append("\"logo\": \""+ logo + "\",");
            sb.Append("\"manufacturer\": \"" + manufacturer + "\",");
            sb.Append("\"releaseDate\": \"" + releaseDate + "\",");
            sb.Append("\"slogan\": \"" + slogan + "\",");
            sb.Append("\"alternateName\": \"" + alternateName + "\",");
            sb.Append("\"disambiguatingDescription\": \"" + disambiguatingDescription + "\",");
            sb.Append("\"keywords\": \"" + keywords + "\",");
            sb.Append("\"name\": \"" + productName + "\",");
            sb.Append("\"image\": \"" + image + "\",");
            sb.Append("\"offers\": {");
            sb.Append("\"@type\": \"AggregateOffer\",");
            sb.Append("\"priceCurrency\": \"IRR\",");
            sb.Append("\"highPrice\": \"" + highPrice + "\",");
            sb.Append("\"lowPrice\": \"" + lowPrice + "\",");
            sb.Append("\"offerCount\": \"" + offerUrl.Length.ToString() + "\",");
            sb.Append("\"offers\": [");
            foreach (var item in offerUrl)
            {
                sb.Append("{");
                sb.Append("\"@type\": \"Offer\",");
                sb.Append("\"url\": \"" + item + "\"");
                sb.Append("},");
            }

            var temp = sb.ToString().TrimEnd(',');
            temp += "]}}</script>";
            return temp;
        }

        return "هیچ محصولی وجود ندارد";
    }

    /// <summary>
    /// http://schema.org/WebPage
    /// </summary>
    /// <returns>json-LD string</returns>
    public static string Webpage(string specialty, string author, string name, string description, string[] ids, string[] names, string[] keywords)
    {
        string kword = "";
        int breadCount = ids.Length;
        string result = "<script type=\"application/ld+json\">{";
        result += "\"@context\":\"http://schema.org\"," + "\"@type\":\"WebPage\",";
        if (!string.IsNullOrWhiteSpace(name))
        {
            result += "\"name\" : " + "\"" + name + "\",";
        }
        if (!string.IsNullOrWhiteSpace(description))
        {
            result += "\"description\" : " + "\"" + description + "\",";
        }
        result += "\"inLanguage\": \"FA-IR\",";
        if (!string.IsNullOrWhiteSpace(author))
        {
            result += "\"author\" : " + "\"" + author + "\",";
        }
        result += "\"specialty\": \"" + specialty + "\",";
        if (keywords != null)
        {
            foreach (string i in keywords)
            {
                kword += "\"" + i + "\",";
            }
            kword = kword.TrimEnd(',');
        }
        if (!string.IsNullOrWhiteSpace(kword))
        {
            result += "\"keywords\" : " + "[" + kword + "],";
        }
        if (breadCount > 0)
        {
            result += "\"breadcrumb\":{"
                      + "\"@type\":\"BreadcrumbList\","
                      + "\"itemListElement\":[";
            for (int i = 0; i < breadCount; i++)
            {
                result += "{\"@type\": \"ListItem\","
                          + "\"position\":" + (i + 1).ToString() + ","
                          + "\"item\":{"
                          + "\"@id\":\"" + ids[i] + "\","
                          + "\"name\":\"" + names[i] + "\"}},";
            }
            result = result.TrimEnd(',');
        }

        result += "]}}</script>";
        return result;
    }

}
