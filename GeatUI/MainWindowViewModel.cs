using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GeatUI
{
    /// <summary>
    /// 要素追加を行うイベント用デリゲート
    /// </summary>
    /// <param name="rect">要素を追加する領域</param>
    delegate void AddElementEventHandler(Rect rect);

    /// <summary>
    /// MainWindowのViewModelクラス
    /// </summary>
    class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// 長方形追加モードに変更するコマンド
        /// </summary>
        public DelegateCommand SetRectAdditionModeCommand { get; private set; }

        /// <summary>
        /// 作業領域上にある要素のコレクション
        /// </summary>
        public ObservableCollection<PlaceableElement> ElementCollection { get; private set; }

        private bool isRectAdditionMode;
        /// <summary>
        /// 長方形追加モードになっているかのフラグ
        /// </summary>
        public bool IsRectAdditionMode {
            get { return this.isRectAdditionMode; }
            set { this.SetProperty(ref this.isRectAdditionMode, value); }
        }

        private AddElementEventHandler addElementEvent;
        /// <summary>
        /// 要素追加を行うイベントのハンドラ
        /// </summary>
        public AddElementEventHandler AddElementEvent
        {
            get { return this.addElementEvent; }
            set { this.SetProperty(ref this.addElementEvent, value); }
        }

        private Rect gridSize;
        /// <summary>
        /// 作業領域のグリッドの単位サイズ
        /// </summary>
        public Rect GridSize
        {
            get { return gridSize; }
            private set { this.SetProperty(ref this.gridSize, value); }
        }

        public PathGeometry GridGeometry
        {
            get
            {
                PathFigure pf = new PathFigure();
                pf.StartPoint = new Point(0, 0);
                pf.Segments.Add(new LineSegment(new Point(0, gridSize.Height), true));
                pf.Segments.Add(new LineSegment(new Point(gridSize.Width, gridSize.Height), true));
                PathGeometry pg = new PathGeometry();
                pg.Figures.Add(pf);
                return pg;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            this.SetRectAdditionModeCommand = new DelegateCommand(
                this.SetRectAdditionMode, () => true);
            this.ElementCollection = new ObservableCollection<PlaceableElement>();
            this.IsRectAdditionMode = false;
            this.AddElementEvent = new AddElementEventHandler(AddRect);
            this.GridSize = new Rect { X = 0, Y = 0, Width = 10, Height = 10 };
        }

        private void SetRectAdditionMode()
        {
            this.IsRectAdditionMode = !this.IsRectAdditionMode;
        }

        private void AddRect(Rect rect)
        {
            var element = new PlaceableElement
            {
                X = (int)rect.X,
                Y = (int)rect.Y,
                Width = (int)rect.Width,
                Height = (int)rect.Height,
                Fill = Brushes.Red,
                Stroke = Brushes.Black
            };

            ElementCollection.Add(element);
        }
    }
}
