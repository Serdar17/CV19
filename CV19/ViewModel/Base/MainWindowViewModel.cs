using CV19.Infrastructure.Commands;
using CV19.Model;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;


namespace CV19.ViewModel.Base
{
    internal class MainWindowViewModel : ViewModel
    {
        #region TestDataPoints : IEnumerable<DataPoint> - DESCRIPTION
        private IEnumerable<OxyPlot.DataPoint> _TestDataPoints;
        /// <summary>
        /// Тустовый набор данных для визуализации данных
        /// </summary>
        public IEnumerable<OxyPlot.DataPoint> TestDataPoints { get => _TestDataPoints; set => Set(ref _TestDataPoints, value); }
        #endregion


        #region SelectedPageIndex : int - Номер выбранной вкладки
        private int _SelectedPageIndex;
        /// <summary>
        /// Номер выбранной вкладки
        /// </summary>
        public int SelectedPageIndex { get => _SelectedPageIndex; set => Set(ref _SelectedPageIndex, value); }
        #endregion

        public PlotModel MyModel { get; private set; }

        #region Заголовок окна
        private string _Title = "Анализ статистики CV19";
        /// <summary>/// Заголовок окна/// </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Status: string - Статус программы

        private string _Status = "Готово!";

        /// <summary>
        /// Статус программы
        /// </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion

        #region Команды

        public ICommand CloseApplicationComand { get; }

        private void OnCloseApplicationComandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationComandExecute(object p) => true;

        #endregion

        #region Команды2
        public ICommand ChangeTabIndexCommand { get; }

        private bool CanChangeTabIndexCommandExecute(object p) => _SelectedPageIndex >= 0;

        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null)
                return;
            SelectedPageIndex += Convert.ToInt32(p);

        }
        #endregion
        public MainWindowViewModel()
        {
            CloseApplicationComand = new LambdaCommand(OnCloseApplicationComandExecuted, CanCloseApplicationComandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);


            var data_points = new List<OxyPlot.DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);
                //data_points.Add(new Model.DataPoint { XValue = x, YValue = y });
                data_points.Add(new OxyPlot.DataPoint(x, y));
            }
            //TestDataPoints = data_points;
            MyModel = new PlotModel { Title = "График" };
            MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            //MyModel.Series.Add(data_points);
        }
    }
}
