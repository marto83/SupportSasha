using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace SupportSasha.Donations.Helpers
{
    public static class HtmlHelpers
    {
        public static dynamic FormatDate(this HtmlHelper helper, DateTime date)
        {
            return string.Format(@"{0:dd MMM yyyy HH:mm}", date);
        }

        public static dynamic HumanDate(this HtmlHelper helper, DateTimeOffset date)
        {
            var ts = new TimeSpan(DateTimeOffset.UtcNow.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            if (delta < 0)
            {
                return "not yet";
            }
            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 2 * MINUTE)
            {
                return "a minute ago";
            }
            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 90 * MINUTE)
            {
                return "an hour ago";
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 48 * HOUR)
            {
                return "yesterday";
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + " days ago";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

        public static dynamic WrapWith(this HtmlHelper helper, string text, string tagName)
        {
            if(string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var builder = new TagBuilder(tagName);
            builder.SetInnerText(text);
            return new MvcHtmlString(builder.ToString());
        }

        public static dynamic GetUserImageByEmail(this HtmlHelper helper, string email)
        {
            return GetGravatarUrl(email);
        }

        private static string GetGravatarUrl(string email)
        {
            string baseUrl = "http://www.gravatar.com/avatar/{0}?s=48&r=pg&d={1}";
            string processedEmail = email.Trim().ToLower();
            using (MD5 md5Hash = MD5.Create())
            {
                return String.Format(baseUrl, GetMd5Hash(md5Hash, processedEmail), WebHelpers.ResolveServerUrl("/Content/Images/happy-face.png"));
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}