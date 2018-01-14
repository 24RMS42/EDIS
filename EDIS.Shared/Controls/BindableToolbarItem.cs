using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EDIS.Shared.Controls
{
    public class BindableToolbarItem : ToolbarItem
    {
        public static readonly BindableProperty IsVisibleProperty =
            BindableProperty.Create("BindableToolbarItem", typeof(bool), typeof(ToolbarItem),
                true, BindingMode.TwoWay, propertyChanged: OnIsVisibleChanged);

        public BindableToolbarItem()
        {
            InitVisibility();
        }

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        private void InitVisibility()
        {
            OnIsVisibleChanged(this, false, IsVisible);
        }

        private static void OnIsVisibleChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var item = bindable as BindableToolbarItem;

            if (item != null && item.Parent == null)
                return;

            if (item != null)
            {
                var items = ((ContentPage)item.Parent).ToolbarItems;

                if ((bool)newvalue && !items.Contains(item))
                {
                    Device.BeginInvokeOnMainThread(() => { items.Add(item); });
                }
                else if (!(bool)newvalue && items.Contains(item))
                {
                    Device.BeginInvokeOnMainThread(() => { items.Remove(item); });
                }
            }
        }
    }
}
