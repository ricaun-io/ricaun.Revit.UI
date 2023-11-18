using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

namespace ricaun.Revit.UI.Utils
{
    /// <summary>
    /// RibbonItemPanelExtension
    /// </summary>
    public static class RibbonItemPanelExtension
    {
        #region Row
        private const int MaxRowNumber = 3;
        internal static Autodesk.Windows.RibbonRowPanel[] CreateRowStackedItemsWithMax(this RibbonPanel ribbonPanel, params RibbonItem[] ribbonItems)
        {
            return ribbonPanel.CreateRowStackedItemsWithMax(MaxRowNumber, ribbonItems);
        }
        internal static Autodesk.Windows.RibbonRowPanel[] CreateRowStackedItemsWithMax(this RibbonPanel ribbonPanel, int maxRowNumber, params RibbonItem[] ribbonItems)
        {
            var ribbonFlowPanels = new List<Autodesk.Windows.RibbonRowPanel>();

            if (ribbonItems.Length == 0)
                return ribbonFlowPanels.ToArray();

            var list = new List<RibbonItem>();
            for (int i = 0; i < ribbonItems.Length; i++)
            {
                var ribbonItem = ribbonItems[i];
                list.Add(ribbonItem);
                if (list.Count == maxRowNumber)
                {
                    var ribbonFlowPanel = ribbonPanel.CreateRowStackedItems(list.ToArray());
                    ribbonFlowPanels.Add(ribbonFlowPanel);
                    list.Clear();
                }
            }

            if (list.Any())
                ribbonFlowPanels.Add(ribbonPanel.CreateRowStackedItems(list.ToArray()));

            return ribbonFlowPanels.ToArray();
        }
        internal static Autodesk.Windows.RibbonRowPanel CreateRowStackedItems(this RibbonPanel ribbonPanel, params RibbonItem[] ribbonItems)
        {
            if (ribbonItems.Length == 0) return null;

            var ribbonFlowPanel = new Autodesk.Windows.RibbonRowPanel();
            var ribbonItemLast = ribbonItems.LastOrDefault();
            foreach (var ribbonItem in ribbonItems)
            {
                var awRibbonItem = ribbonItem.GetRibbonItem();
                ribbonPanel.Remove(ribbonItem);
                ribbonFlowPanel.AddRibbonItem(awRibbonItem);

                if (ribbonItemLast != ribbonItem)
                    ribbonFlowPanel.Items.Add(new Autodesk.Windows.RibbonRowBreak());
            }
            ribbonPanel.GetRibbonPanel().Source.Items.Add(ribbonFlowPanel);
            return ribbonFlowPanel;
        }
        /// <summary>
        /// Add <paramref name="ribbonItem"/> to <paramref name="ribbonRowPanel"/> updating properties of the RibbonItem.
        /// </summary>
        /// <param name="ribbonRowPanel"></param>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonRowPanel AddRibbonItem(this Autodesk.Windows.RibbonRowPanel ribbonRowPanel, Autodesk.Windows.RibbonItem ribbonItem)
        {
            ribbonRowPanel.Items.Add(UpdateForRibbonRowPanel(ribbonItem));
            return ribbonRowPanel;
        }
        /// <summary>
        /// Set each <paramref name="ribbonRowPanels"/> items to <paramref name="ribbonItemSize"/>
        /// </summary>
        /// <param name="ribbonRowPanels"></param>
        /// <param name="ribbonItemSize"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonRowPanel[] SetRibbonItemSize(
            this Autodesk.Windows.RibbonRowPanel[] ribbonRowPanels,
            Autodesk.Windows.RibbonItemSize ribbonItemSize = Autodesk.Windows.RibbonItemSize.Large)
        {
            foreach (var ribbonRowPanel in ribbonRowPanels)
            {
                ribbonRowPanel.SetRibbonItemSize(ribbonItemSize);
            }
            return ribbonRowPanels;
        }

        /// <summary>
        /// Set <paramref name="ribbonRowPanel"/> items to <paramref name="ribbonItemSize"/>
        /// </summary>
        /// <param name="ribbonRowPanel"></param>
        /// <param name="ribbonItemSize"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonRowPanel SetRibbonItemSize(
            this Autodesk.Windows.RibbonRowPanel ribbonRowPanel,
            Autodesk.Windows.RibbonItemSize ribbonItemSize = Autodesk.Windows.RibbonItemSize.Large)
        {
            foreach (var ribbonItem in ribbonRowPanel.Items)
            {
                ribbonItem.Size = ribbonItemSize;
            }
            return ribbonRowPanel;
        }

        internal static Autodesk.Windows.RibbonItem UpdateForRibbonRowPanel(this Autodesk.Windows.RibbonItem ribbonItem)
        {
            ribbonItem.Size = Autodesk.Windows.RibbonItemSize.Standard;
            ribbonItem.AllowInStatusBar = false;
            ribbonItem.SetTextOrientation(System.Windows.Controls.Orientation.Horizontal);
            return ribbonItem;
        }
        #region Utils
        internal static T SetTextOrientation<T>(this T ribbonItem, System.Windows.Controls.Orientation orientation) where T : Autodesk.Windows.RibbonItem
        {
            try
            {
                var propriety = ribbonItem.GetType().GetProperty(nameof(Autodesk.Private.Windows.IRibbonTextProperties.Orientation));
                propriety?.SetValue(ribbonItem, orientation);
            }
            catch { }
            return ribbonItem;
        }

        #endregion
        #endregion
        #region Flow
        internal static Autodesk.Windows.RibbonFlowPanel CreateFlowStackedItems(this RibbonPanel ribbonPanel, params RibbonItem[] ribbonItems)
        {
            if (ribbonItems.Length == 0) return null;

            var ribbonFlowPanel = new Autodesk.Windows.RibbonFlowPanel();
            foreach (var ribbonItem in ribbonItems)
            {
                var awRibbonItem = ribbonItem.GetRibbonItem();
                ribbonPanel.Remove(ribbonItem);
                ribbonFlowPanel.AddRibbonItem(awRibbonItem);
            }

            ribbonPanel.GetRibbonPanel().Source.Items.Add(ribbonFlowPanel);
            return ribbonFlowPanel;
        }
        /// <summary>
        /// Add <paramref name="ribbonItem"/> to <paramref name="ribbonFlowPanel"/> updating properties of the RibbonItem.
        /// </summary>
        /// <param name="ribbonFlowPanel"></param>
        /// <param name="ribbonItem"></param>
        /// <returns></returns>
        public static Autodesk.Windows.RibbonFlowPanel AddRibbonItem(this Autodesk.Windows.RibbonFlowPanel ribbonFlowPanel, Autodesk.Windows.RibbonItem ribbonItem)
        {
            ribbonFlowPanel.Items.Add(UpdateForRibbonFlowPanel(ribbonItem));
            return ribbonFlowPanel;
        }
        internal static Autodesk.Windows.RibbonItem UpdateForRibbonFlowPanel(this Autodesk.Windows.RibbonItem ribbonItem)
        {
            ribbonItem.Size = Autodesk.Windows.RibbonItemSize.Standard;
            ribbonItem.ShowImage = true;
            ribbonItem.ShowText = false;
            ribbonItem.AllowInStatusBar = false;
            return ribbonItem;
        }

        #endregion
    }
}
