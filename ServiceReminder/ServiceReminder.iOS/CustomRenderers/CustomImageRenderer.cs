using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ServiceReminder.iOS.CustomRenderers;
using Xamarin.Forms;
using MonoTouch.CoreGraphics;

[assembly: ExportRenderer(typeof(ServiceReminder.CustomControls.CustomImageCell), typeof(CustomImageCellRenderer))]
namespace ServiceReminder.iOS.CustomRenderers
{
    public class CustomImageCellRenderer : Xamarin.Forms.Platform.iOS.ImageCellRenderer
    {
        public override UITableViewCell GetCell(Xamarin.Forms.Cell item, UITableView tv)
        {
             var cell = base.GetCell(item, tv);
             cell.ImageView.Layer.CornerRadius = 15.0f;
             cell.ImageView.Layer.MasksToBounds = true;
             cell.ImageView.Layer.BorderColor = UIColor.LightGray.CGColor;
            cell.ImageView.Layer.BorderWidth = 1.0f;
             return cell;
        }
    }
}