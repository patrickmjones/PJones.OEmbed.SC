using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Sitecore.Web.UI.WebControls;
using Sitecore.Xml.Xsl;
using Sitecore.Data.Fields;

namespace PJones.OEmbed.UI.SC
{
	public class Link : Sitecore.Web.UI.WebControls.FieldControl
	{
		public int MaxWidth { get; set; }
		public int MaxHeight { get; set; }

		protected override void DoRender(System.Web.UI.HtmlTextWriter output)
		{
			if (!string.IsNullOrEmpty(this.Field))
			{
				Sitecore.Data.Items.Item i = this.GetItem();
				if (i != null)
				{
					LinkField lf = (LinkField)i.Fields[this.Field];
					if (lf != null && !lf.IsInternal && !lf.IsMediaLink)
					{
						PJones.OEmbed.OEmbedEngine eng = new OEmbedEngine();
						string embed = eng.Parse(lf.Url, MaxWidth, MaxHeight);
						output.Write(embed);
					}
				}
				return;
			}
			else
			{
				throw new InvalidOperationException("Field property is required. All field web controls require the field name to be set.");
			}
		}
	}
}
