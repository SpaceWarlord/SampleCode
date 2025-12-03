using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SampleCode.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SampleCode.Main;

public partial class Shell
{
    private void NavigationView_Loaded(object sender, RoutedEventArgs e)
    {
        SetCurrentNavigationViewItem(GetNavigationViewItems(typeof(HomePage)).First());
    }

    private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {            
        SetCurrentNavigationViewItem(args.SelectedItemContainer as NavigationViewItem);
    }

    public List<NavigationViewItem> GetNavigationViewItems()
    {
        List<NavigationViewItem> result = new();
        var items = NavigationView.MenuItems.Select(i => (NavigationViewItem)i).ToList();
        items.AddRange(NavigationView.FooterMenuItems.Select(i => (NavigationViewItem)i));
        result.AddRange(items);

        foreach (NavigationViewItem mainItem in items)
        {
            result.AddRange(mainItem.MenuItems.Select(i => (NavigationViewItem)i));
        }
        return result;
    }

    public List<NavigationViewItem> GetNavigationViewItems(Type type)
    {
        //return GetNavigationViewItems().Where(i => i.Tag.ToString() == type.FullName).ToList();
        return GetNavigationViewItems();
    }

    public List<NavigationViewItem> GetNavigationViewItems(Type type, string title)
    {
        return GetNavigationViewItems(type).Where(ni => ni.Content.ToString() == title).ToList();
    }

    public void SetCurrentNavigationViewItem(NavigationViewItem item)
    {
        if (item == null)
        {
            return;
        }

        if (item.Tag == null)
        {
            return;
        }
        if (item.MenuItems.Count > 0)
        {                             
            NavigationView.SelectedItem = item;                
            return;
        }            
        ContentFrame.Navigate(Type.GetType(item.Tag.ToString()), item.Content);
        NavigationView.Header = item.Content;
        NavigationView.SelectedItem = item;
    }

    public NavigationViewItem GetCurrentNavigationViewItem()
    {
        if (NavigationView != null)
        {
            if (NavigationView.SelectedItem != null)
            {
                return NavigationView.SelectedItem as NavigationViewItem;
            }
        }
        else
        {

        }
        return null;
    }

    void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
    {
        throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
    }
}