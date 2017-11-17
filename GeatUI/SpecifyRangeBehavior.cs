using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GeatUI
{
    [TypeConstraint(typeof(ItemsControl))]
    class SpecifyRangeBehavior : Behavior<ItemsControl>
    {
        /// <summary>
        /// 長方形追加モードになっているかのフラグ
        /// </summary>
        public static readonly DependencyProperty IsRectAdditionModeProperty =
            DependencyProperty.Register(
                "IsRectAdditionMode",
                typeof(bool),
                typeof(SpecifyRangeBehavior),
                new FrameworkPropertyMetadata(false, IsRectAdditionModePropertyChanged));

        /// <summary>
        /// 長方形追加イベント
        /// </summary>
        public static readonly DependencyProperty AddRectHandlerProperty =
            DependencyProperty.Register(
                "AddRectHandler",
                typeof(AddElementEventHandler),
                typeof(SpecifyRangeBehavior),
                new FrameworkPropertyMetadata((AddElementEventHandler)((rect) => { })));

        /// <summary>
        /// 作業領域のグリッドの単位サイズ
        /// </summary>
        public static readonly DependencyProperty GridSizeProperty =
            DependencyProperty.Register(
                "GridSize",
                typeof(Rect),
                typeof(SpecifyRangeBehavior),
                new FrameworkPropertyMetadata(new Rect { X = 0, Y = 0, Width = 0, Height = 0 }));

        private static void IsRectAdditionModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SpecifyRangeBehavior behavior = d as SpecifyRangeBehavior;

            if ((bool)e.OldValue)
            {
                behavior.isSpecifying = false;
                behavior.AssociatedObject.Cursor = Cursors.Arrow;
                var collection = behavior.AssociatedObject.ItemsSource as ObservableCollection<PlaceableElement>;
                collection.Remove(behavior.specifyingRange);
            }
            else
            {
                behavior.AssociatedObject.Cursor = Cursors.Cross;
            }
        }

        /// <summary>
        /// 依存関係プロパティIsRectAdditionModePropertyのラッパー
        /// </summary>
        public bool IsRectAdditionMode
        {
            get { return (bool)GetValue(IsRectAdditionModeProperty); }
            set { SetValue(IsRectAdditionModeProperty, value); }
        }

        /// <summary>
        /// 依存関係プロパティAddRectHandlerPropertyのラッパー
        /// </summary>
        public AddElementEventHandler AddRectHandler
        {
            get { return GetValue(AddRectHandlerProperty) as AddElementEventHandler; }
            set { SetValue(AddRectHandlerProperty, value); }
        }

        /// <summary>
        /// 依存関係プロパティGridSizePropertyのラッパー
        /// </summary>
        public Rect GridSize
        {
            get { return (Rect)GetValue(GridSizeProperty); }
            set { SetValue(GridSizeProperty, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SpecifyRangeBehavior()
        {
            this.isSpecifying = false;
            this.specifyingRange = new PlaceableElement
            {
                X = 0,
                Y = 0,
                Width = 0,
                Height = 0,
                Stroke = Brushes.Black
            };
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += StartSpecifying;
            AssociatedObject.MouseMove += Specifying;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= StartSpecifying;
            AssociatedObject.MouseMove -= Specifying;
            base.OnDetaching();
        }

        private bool isSpecifying;
        private Point clickPoint;
        private PlaceableElement specifyingRange;

        private void StartSpecifying(object sender, MouseButtonEventArgs e)
        {
            if (!IsRectAdditionMode)
                return;

            if (!isSpecifying)
            {
                isSpecifying = true;
                clickPoint = e.GetPosition(AssociatedObject);

                //グリッドに合わせて位置を調整
                clickPoint.X = Math.Round(clickPoint.X / GridSize.Width) * GridSize.Width;
                clickPoint.Y = Math.Round(clickPoint.Y / GridSize.Height) * GridSize.Height;

                specifyingRange.X = (int)clickPoint.X;
                specifyingRange.Y = (int)clickPoint.Y;
                specifyingRange.Width = 0;
                specifyingRange.Height = 0;
                var collection = AssociatedObject.ItemsSource as ObservableCollection<PlaceableElement>;
                collection.Add(specifyingRange);
            }
            else
            {
                IsRectAdditionMode = false;

                var rect = new Rect
                {
                    X = specifyingRange.X,
                    Y = specifyingRange.Y,
                    Width = specifyingRange.Width,
                    Height = specifyingRange.Height
                };
                AddRectHandler(rect);
            }
        }

        private void Specifying(object sender, MouseEventArgs e)
        {
            if (isSpecifying)
            {
                var p = e.GetPosition(AssociatedObject);

                //X方向の設定
                specifyingRange.Width = (int)(Math.Abs(p.X - clickPoint.X)) / (int)GridSize.Width * (int)GridSize.Width;
                if (p.X >= clickPoint.X)
                {
                    specifyingRange.X = (int)clickPoint.X;
                }
                else
                {
                    specifyingRange.X = (int)clickPoint.X - specifyingRange.Width;
                }

                //Y方向の設定
                specifyingRange.Height = (int)(Math.Abs(p.Y - clickPoint.Y)) / (int)GridSize.Height * (int)GridSize.Height;
                if (p.Y >= clickPoint.Y)
                {
                    specifyingRange.Y = (int)clickPoint.Y;
                }
                else
                {
                    specifyingRange.Y = (int)clickPoint.Y - specifyingRange.Height;
                }
            }
        }
    }
}
