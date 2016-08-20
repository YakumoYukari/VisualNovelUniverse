using System.Collections.ObjectModel;
using Caliburn.Micro;
using Visual_Novel_Universe.Models;

namespace Visual_Novel_Universe.ViewModels
{
    public sealed class CoverImageGridViewModel : Screen
    {
        public ObservableCollection<CoverImage> CoverImages { get; set; } = new ObservableCollection<CoverImage>();

        private int _MaxDisplayableRows;
        public int MaxDisplayableRows
        {
            get { return _MaxDisplayableRows; }
            set
            {
                _MaxDisplayableRows = value;
                ColumnsDisplayed = _MaxDisplayableRows / 2;
                NotifyOfPropertyChange(() => MaxDisplayableRows);
            }
        }

        private int _ColumnsDisplayed;
        public int ColumnsDisplayed
        {
            get { return _ColumnsDisplayed; }
            set
            {
                _ColumnsDisplayed = value > MaxDisplayableRows ? MaxDisplayableRows : value < 1 ? 1 : value;
                NotifyOfPropertyChange(() => ColumnsDisplayed);
            }
        }

        public CoverImageGridViewModel()
        {
            DisplayName = "VN Cover Images";
        }

        public void AddCover(VisualNovel VnToAdd)
        {
            CoverImages.Add(new CoverImage
            {
                Filepath = VnToAdd.CoverImagePath,
                ParentVisualNovel = VnToAdd
            });
        }

        public void CoverImageClicked(CoverImage Cover)
        {
            Events.Aggregator.PublishOnUIThread(new CoverImageClickedMessage{ ClickedVisualNovel = Cover.ParentVisualNovel });
        }

        public void ZoomIn()
        {
            ColumnsDisplayed--;
        }

        public void ZoomOut()
        {
            ColumnsDisplayed++;
        }
    }
}
