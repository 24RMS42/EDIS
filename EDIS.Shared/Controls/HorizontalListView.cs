using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using EDIS.Domain.Circuits;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace EDIS.Shared.Controls
{
    public class HorizontalListView : ScrollView
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<object>), typeof(HorizontalListView), default(IEnumerable<object>), BindingMode.TwoWay, null, ListChanged);

        public static readonly BindableProperty RegularItemCommandProperty =
            BindableProperty.Create(nameof(RegularItemCommand), typeof(ICommand), typeof(HorizontalListView));

        public static readonly BindableProperty AddNewItemCommandProperty =
            BindableProperty.Create(nameof(AddNewItemCommand), typeof(ICommand), typeof(HorizontalListView));

        public static readonly BindableProperty ItemColorProperty = 
            BindableProperty.Create(nameof(SelectedItemColor), typeof(Color), typeof(HorizontalListView), default(Color));

        public static readonly BindableProperty SelectedItemColorProperty =
            BindableProperty.Create(nameof(ItemColor), typeof(Color), typeof(HorizontalListView), default(Color));


        private static void ListChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as HorizontalListView;

            control?.Render();
        }

        public Color ItemColor
        {
            get => (Color) GetValue(ItemColorProperty);
            set => SetValue(ItemColorProperty, value);
        }

        public Color SelectedItemColor
        {
            get => (Color)GetValue(SelectedItemColorProperty);
            set => SetValue(SelectedItemColorProperty, value);
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public ICommand RegularItemCommand
        {
            get { return (RelayCommand<CircuitPointsRcdTest>)GetValue(RegularItemCommandProperty); }
            set { SetValue(RegularItemCommandProperty, value); }
        }

        private ICommand InternalRegularItemCommand
        {
            get
            {
                return new RelayCommand<CircuitPointsRcdTest>(item =>
                {
                    RegularItemCommand?.Execute(item);

                    var items = Content as StackLayout;
                    if (items?.Children == null) return;
                    foreach (var child in items.Children)
                    {
                        var bc = child.BindingContext as CircuitPointsRcdTest;
                        if (bc == null) continue;
                        child.BackgroundColor = Color.Orange;
                        if (bc.CprTestId == item.CprTestId)
                        {
                            child.BackgroundColor = SelectedItemColor;
                        }
                    }
                });
            }
        }

        public ICommand AddNewItemCommand
        {
            get { return (ICommand)GetValue(AddNewItemCommandProperty); }
            set { SetValue(AddNewItemCommandProperty, value); }
        }

        private ICommand InternalAddNewItemCommand
        {
            get
            {
                return new RelayCommand(() =>
               {
                   AddNewItemCommand?.Execute(null);
               });
            }
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(HorizontalListView), default(DataTemplate));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        private bool _subscribed;

        private void Subscribe(bool enabled)
        {
            var itemsSourceINotifyCollectionChanged = ItemsSource as INotifyCollectionChanged;

            if (!enabled)
            {
                if (itemsSourceINotifyCollectionChanged != null)
                {
                    itemsSourceINotifyCollectionChanged.CollectionChanged -= CollectionChanged;
                }
            }
            else if (!_subscribed)
            {
                if (itemsSourceINotifyCollectionChanged != null)
                {
                    itemsSourceINotifyCollectionChanged.CollectionChanged += CollectionChanged;
                }
            }

            _subscribed = enabled;
        }

        public void Render()
        {
            if (ItemTemplate == null || ItemsSource == null)
                return;

            Subscribe(true);

            var layout = new StackLayout() { Spacing = 0 };
            layout.Orientation = Orientation == ScrollOrientation.Vertical
                ? StackOrientation.Vertical : StackOrientation.Horizontal;

            var hasNew = false;
            foreach (var item in ItemsSource)
            {
                var local = item as CircuitPointsRcdTest;
                if(local == null)
                    continue;
                if (string.IsNullOrEmpty(local.CircuitEndPoint))
                    hasNew = true;
            }

            var index = 0;
            foreach (var item in ItemsSource)
            {
                index++;
                var viewCell = ItemTemplate.CreateContent() as ViewCell;
                if (viewCell != null)
                {
                    viewCell.View.BindingContext = item;
                    viewCell.View.WidthRequest = 100;
                    if (!hasNew)
                    {
                        viewCell.View.BackgroundColor = index == 1 ? SelectedItemColor : ItemColor;
                    }
                    else
                    {
                        var local = item as CircuitPointsRcdTest;
                        if (local == null)
                            continue;
                        viewCell.View.BackgroundColor = string.IsNullOrEmpty(local.CircuitEndPoint)
                            ? SelectedItemColor
                            : ItemColor;
                    }
                    viewCell.View.GestureRecognizers.Add(new TapGestureRecognizer { Command = InternalRegularItemCommand, CommandParameter = item });
                    layout.Children.Add(viewCell.View);
                    layout.Children.Add(new BoxView { WidthRequest = 1, BackgroundColor = Color.Black });
                }
            }

            var grid = new Grid()
            {
                WidthRequest = 100,
                Padding = new Thickness(10),
                BackgroundColor = ItemColor,
                Children =
                    {
                        new Image
                        {
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Source = "add.png"
                        }
                    }
            };

            grid.GestureRecognizers.Add(new TapGestureRecognizer { Command = InternalAddNewItemCommand });
            layout.Children.Add(grid);

            Content = layout;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Render();
        }
    }
}