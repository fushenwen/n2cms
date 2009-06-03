using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2.Web;
using N2.Web.UI.WebControls;

namespace N2.Edit
{
	/// <summary>
	/// Used internally to apply the preview publish button.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class ControlPanelPreviewPublishAttribute : ControlPanelLinkAttribute
	{
		public ControlPanelPreviewPublishAttribute(string toolTip, int sortOrder)
			: base("cpPreviewPublish", "~/edit/img/ico/disk.gif", null, toolTip, sortOrder, ControlPanelState.Previewing)
		{
		}

		public override Control AddTo(Control container, PluginContext context)
		{
			if (!IsAuthorized(container.Page.User))
				return null;

			if (!ActiveFor(container, context.State))
				return null;

			HyperLink hl = new HyperLink();
			hl.Text = GetInnerHtml(IconUrl, ToolTip, Title);
			hl.NavigateUrl = Url.Parse("~/Edit/PublishPreview.aspx").AppendQuery("selectedUrl", context.Selected.Url);
			hl.CssClass = "publish";
			hl.ToolTip = context.Format(ToolTip, false);
			container.Controls.Add(hl);

			return hl;
		}

		protected void RedirectTo(Page page, ContentItem item)
		{
			string url = page.Request["returnUrl"];
			if (string.IsNullOrEmpty(url))
				url = Context.Current.EditManager.GetPreviewUrl(item);

			page.Response.Redirect(url);
		}
	}
}