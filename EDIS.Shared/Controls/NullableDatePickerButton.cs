using System;
using Xamarin.Forms;

namespace EDIS.Shared.Controls
{
    public class DatePickerButton : Button
    {
        private DateTime? _OldDate;
        private DatePicker _Picker;
        private IViewContainer<View> _ParentLayout;

        public static readonly BindableProperty DateProperty =
            BindableProperty.Create<DatePickerButton, DateTime?>(p => p.Date, null, BindingMode.TwoWay);
        public DateTime? Date
        {
            get { return (DateTime?)GetValue(DateProperty); }
            set
            {
                SetValue(DateProperty, value);
                OnPropertyChanged("DefaultText");
            }
        }

        new private static readonly BindableProperty TextProperty = BindableProperty.Create<DatePickerButton, string>(p => p.Text, "...");
        new public string Text
        {
            get { return (string)GetValue(TextProperty); }
            private set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty DefaultTextProperty = BindableProperty.Create<DatePickerButton, string>(p => p.DefaultText, "Pick Date...");
        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }

        public static readonly BindableProperty TextFormatProperty =
            BindableProperty.Create<DatePickerButton, string>(p => p.TextFormat, "MM'/'dd'/'yyyy");
        public string TextFormat
        {
            get { return (string)GetValue(TextFormatProperty); }
            set { SetValue(TextFormatProperty, value); }
        }

        //hide the command so you don't accidentally override it
        new public Command Command
        {
            get { return (Command)GetValue(CommandProperty); }
            private set { SetValue(CommandProperty, value); }
        }

        public event EventHandler<DateChangedEventArgs> DateSelected;

        public DatePickerButton()
        {
            //create the datepicker, make it invisible on the form.
            _Picker = new DatePicker
            {
                IsVisible = false
            };

            //handle the focus/unfocus or rather the showing and hiding of the dateipicker
            _Picker.Focused += _Picker_Focused;
            _Picker.Unfocused += _Picker_Unfocused;

            //command for the button
            Command = new Command((obj) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.Focus();
                });
                //try to get the parent layout and add the datepicker
                if (_ParentLayout == null)
                {
                    _ParentLayout = _GetParentLayout(ParentView);
                    if (_ParentLayout != null)
                    {
                        //add the picker to the closest layout up the tree
                        _ParentLayout.Children.Add(_Picker);
                    }
                    else
                    {
                        throw new InvalidOperationException("The DatePickerButton needs to be inside an Layout type control that can have other children");
                    }
                }
                //show the picker modal
                Device.BeginInvokeOnMainThread(() =>
                {
                    _Picker.Focus();
                });
            });
            _UpdateText();
        }

        private IViewContainer<View> _GetParentLayout(VisualElement ParentView)
        {
            //StackLayout, RelativeLayout, Grid, and AbsoluteLayout all implement IViewContainer,
            //it would be very rare that this method would return null.
            IViewContainer<View> parent = ParentView as IViewContainer<View>;
            if (ParentView == null)
            {
                return null;
            }
            else if (parent != null)
            {
                return parent;
            }
            else
            {
                return _GetParentLayout(ParentView.ParentView);
            }
        }

        void _Picker_Focused(object sender, FocusEventArgs e)
        {
            //default the date to now if Date is empty
            _Picker.Date = Date ?? DateTime.Now;
        }

        void _Picker_Unfocused(object sender, FocusEventArgs e)
        {
            //this always sets.. can't cancel the dialog.
            Date = _Picker.Date;
            _UpdateText();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _UpdateText();
        }

        private void _UpdateText()
        {
            //the button has a default text, use that the first time.
            if (Date != null)
            {
                //default formatting is in the FormatProperty BindableProperty 
                base.Text = Date.Value.ToString(TextFormat);
            }
            else
            {
                base.Text = DefaultText;
            }
        }

        protected override void OnPropertyChanging(string propertyName = null)
        {
            //set this so there is an old date for the DateChangedEventArgs
            base.OnPropertyChanging(propertyName);
            _OldDate = Date;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == DateProperty.PropertyName)
            {
                //if the event isn't null, and the old date isn't null, and the date isn't null ... EVENT!
                if (DateSelected != null && _OldDate != null && Date != null)
                {
                    DateSelected(this, new DateChangedEventArgs((DateTime)_OldDate, (DateTime)Date));
                }
                //if the event isn't null, and the date isn't null ... EVENT!
                else if (DateSelected != null && Date != null)
                {
                    DateSelected(this, new DateChangedEventArgs((DateTime)Date, (DateTime)Date));
                }
                _OldDate = null;
            }
            _UpdateText();
        }
    }
}