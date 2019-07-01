using TPC_Chat.Test;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(TPC_Chat.iOS.Renderers.ExtendedViewCellRenderer))]

namespace TPC_Chat.iOS.Renderers
{
    public class ExtendedViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = UIColor.FromRGB(255, 255, 255),
            };

            return cell;
        }
    }
}