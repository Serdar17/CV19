using CV19.Infrastructure.Commands;
using CV19.Model;
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
        private IEnumerable<DataPoint> _TestDataPoints;
        /// <summary>
        /// Тустовый набор данных для визуализации данных
        /// </summary>
        public IEnumerable<DataPoint> TestDataPoints { get => _TestDataPoints; set => Set(ref _TestDataPoints, value); }
        #endregion


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
        public MainWindowViewModel()
        {
            CloseApplicationComand = new LambdaCommand(OnCloseApplicationComandExecuted, CanCloseApplicationComandExecute);


            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);
                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }
            TestDataPoints = data_points;
        }
    }
}
