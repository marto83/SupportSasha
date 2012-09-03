using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Indexes;
using SupportSasha.Donations.Models;

namespace SupportSasha.Donations.Code
{
    public class DonationsIndex : AbstractIndexCreationTask<Donation, Donation>
    {
        //public DonationsIndex()
        //{
        //    Map = donations => from donation in donations select new { donation.Date };

        //    Sort(x => x.Date, Raven.Abstractions.Indexing.SortOptions.None);
        //}
    }
}