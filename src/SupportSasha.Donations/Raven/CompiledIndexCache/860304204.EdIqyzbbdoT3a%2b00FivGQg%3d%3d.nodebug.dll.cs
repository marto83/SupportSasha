using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;


public class Index_Temp_2fDonations_2fByCampaignNameAndConfirmedAndDateSortByDate : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Temp_2fDonations_2fByCampaignNameAndConfirmedAndDateSortByDate()
	{
		this.ViewText = @"from doc in docs.Donations
select new { CampaignName = doc.CampaignName, Confirmed = doc.Confirmed, Date = doc.Date }";
		this.ForEntityNames.Add("Donations");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "Donations", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				CampaignName = doc.CampaignName,
				Confirmed = doc.Confirmed,
				Date = doc.Date,
				__document_id = doc.__document_id
			});
		this.AddField("CampaignName");
		this.AddField("Confirmed");
		this.AddField("Date");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("CampaignName");
		this.AddQueryParameterForMap("Confirmed");
		this.AddQueryParameterForMap("Date");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("CampaignName");
		this.AddQueryParameterForReduce("Confirmed");
		this.AddQueryParameterForReduce("Date");
		this.AddQueryParameterForReduce("__document_id");
	}
}
