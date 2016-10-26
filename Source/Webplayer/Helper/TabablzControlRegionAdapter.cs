﻿using System.Windows.Controls;
using Dragablz;
using Prism.Regions;

namespace Webplayer
{
    /// <summary>
    /// Fetched from https://github.com/tstephansen/PrismExample/blob/master/PrismUnity/PrismUnity/TabablzControlRegionAdapter.cs
    /// </summary>
    public class TabablzControlRegionAdapter : RegionAdapterBase<TabablzControl>
    {
        public TabablzControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory) { }
        protected override void Adapt(IRegion region, TabablzControl regionTarget)
        {
            region.ActiveViews.CollectionChanged += (s, e) =>
            {
                switch (e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        foreach (var t in e.NewItems)
                        {
                            var tb = new TabItem();
                            var iv = e.NewItems[0];
                            var titlePropertyInfo = iv.GetType().GetProperty("Title");
                            var title = titlePropertyInfo.GetValue(iv);
                            tb.Header = title;
                            tb.Content = e.NewItems[0];
                            regionTarget.Items.Insert(regionTarget.Items.Count, tb);
                            regionTarget.SelectedIndex = regionTarget.Items.Count - 1;
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        foreach (var t in e.OldItems)
                        {
                            for (var i = 0; i < regionTarget.Items.Count; i++)
                            {
                                var tab = (TabItem)regionTarget.Items[i];
                                if (tab.Content == e.OldItems[0])
                                {
                                    regionTarget.Items.Remove(tab);
                                }
                            }
                            regionTarget.SelectedIndex = regionTarget.Items.Count - 1;
                        }
                        break;
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}