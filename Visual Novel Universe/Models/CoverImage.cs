using Caliburn.Micro;

namespace Visual_Novel_Universe.Models
{
    public class CoverImage : PropertyChangedBase
    {
        private string _Filepath;
        public string Filepath
        {
            get { return _Filepath; }
            set
            {
                _Filepath = value;
                NotifyOfPropertyChange(() => Filepath);
            }
        }

        private VisualNovel _ParentVisualNovel;
        public VisualNovel ParentVisualNovel
        {
            get { return _ParentVisualNovel; }
            set
            {
                _ParentVisualNovel = value;
                NotifyOfPropertyChange(() => ParentVisualNovel);
            }
        }
    }
}
