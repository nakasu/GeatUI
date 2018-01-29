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
        /// ElementCollectionをViewに公開するためのプロパティ
        /// </summary>
        public ObservableCollection<PlaceableElement> ElementCollection
        {
            get { return this.appContext.UIData.ElementCollection; }
        }

        /// <summary>
        /// 長方形追加モードに変更するコマンド
        /// </summary>
        public DelegateCommand SetRectAdditionModeCommand { get; private set; }

        /// <summary>
        /// 指定した長方形を削除するコマンド
        /// </summary>
        public DelegateCommand RemoveRectCommand { get; private set; }

        /// <summary>
        /// 指定した長方形を選択状態にするコマンド
        /// </summary>
        public DelegateCommand<PlaceableElement> SelectRectCommand { get; private set; }

        /// <summary>
        /// すべての長方形の選択を解除するコマンド
        /// </summary>
        public DelegateCommand DeselectAllRectCommand { get; private set; }

        /// <summary>
        /// 要素追加を行うイベントのハンドラ
        /// </summary>
        public AddElementEventHandler AddElementEvent { get; private set; }

        /// <summary>
        /// 作業領域のグリッドの単位サイズ
        /// </summary>
        public int GridSize { get; set; }

        private bool isRectAdditionMode;
        /// <summary>
        /// 長方形追加モードになっているかのフラグ
        /// </summary>
        public bool IsRectAdditionMode
        {
            get { return this.isRectAdditionMode; }
            set { this.SetProperty(ref this.isRectAdditionMode, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            this.SetRectAdditionModeCommand = new DelegateCommand(
                SetRectAdditionMode, () => true);
            this.RemoveRectCommand = new DelegateCommand(
                appContext.UIData.RemoveRect, () => ElementCollection.Any(e => e.IsSelected));
            this.SelectRectCommand = new DelegateCommand<PlaceableElement>(
                SelectRect, (obj) => true);
            this.DeselectAllRectCommand = new DelegateCommand(
                DeselectAllRect, () => true);
            this.AddElementEvent = new AddElementEventHandler(appContext.UIData.AddRect);
            this.GridSize = 10;
            this.IsRectAdditionMode = false;
        }

        /// <summary>
        /// アプリケーションのモデル
        /// </summary>
        private AppContext appContext = new AppContext();

        /// <summary>
        /// 長方形追加モードの切り替え
        /// </summary>
        private void SetRectAdditionMode()
        {
            this.IsRectAdditionMode = !this.IsRectAdditionMode;
        }

        /// <summary>
        /// RaiseCanExecuteChangedを呼び出すためのSelectRectのラッパー
        /// </summary>
        private void SelectRect(PlaceableElement rect)
        {
            appContext.UIData.SelectRect(rect);
            RemoveRectCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// RaiseCanExecuteChangedを呼び出すためのDeselectAllRectのラッパー
        /// </summary>
        private void DeselectAllRect()
        {
            appContext.UIData.DeselectAllRect();
            RemoveRectCommand.RaiseCanExecuteChanged();
        }
    }
}
