using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;


namespace AuvoMovies.Models
{
    public class Filme : ObservableObject
    {

        private bool adult;
        [JsonPropertyName("adult")]
        public bool Adult
        {
            get { return adult; }
            set => SetProperty(ref adult, value);
        }

        private string backdrop_path;
        [JsonPropertyName("backdrop_path")]
        public string BackdropPath
        {
            get { return backdrop_path; }
            set => SetProperty(ref backdrop_path, value);
        }

        private List<int> genre_ids;
        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds
        {
            get { return genre_ids; }
            set => SetProperty(ref genre_ids, value);
        }

        private int id;
        [JsonPropertyName("id")]
        public int Id
        {
            get { return id; }
            set => SetProperty(ref id, value);
        }

        private string original_language;
        [JsonPropertyName("original_language")]
        public string OriginalLanguage
        {
            get { return original_language; }
            set => SetProperty(ref original_language, value);
        }

        private string original_title;
        [JsonPropertyName("original_title")]
        public string OriginalTitle
        {
            get { return original_title; }
            set => SetProperty(ref original_title, value);
        }

        private string title;
        [JsonPropertyName("title")]
        public string Title
        {
            get { return title; }
            set => SetProperty(ref title, value);
        }

        private string overview;
        [JsonPropertyName("overview")]
        public string Overview
        {
            get { return overview; }
            set => SetProperty(ref overview, value);
        }

        private double popularity;
        [JsonPropertyName("popularity")]
        public double Popularity
        {
            get { return popularity; }
            set => SetProperty(ref popularity, value);
        }

        private string poster_path;
        [JsonPropertyName("poster_path")]
        public string PosterPath
        {
            get { return poster_path; }
            set => SetProperty(ref poster_path, value);
        }

        private string release_date;
        [JsonPropertyName("release_date")]
        public string ReleaseDate
        {
            get { return release_date; }
            set => SetProperty(ref release_date, value);
        }

        private bool video;
        [JsonPropertyName("video")]
        public bool Video
        {
            get { return video; }
            set => SetProperty(ref video, value);
        }

        private double vote_average;
        [JsonPropertyName("vote_average")]
        public double VoteAverage
        {
            get { return vote_average; }
            set => SetProperty(ref vote_average, value);
        }

        private int vote_count;
        [JsonPropertyName("vote_count")]
        public int VoteCount
        {
            get { return vote_count; }
            set => SetProperty(ref vote_count, value);
        }

    }
}


//using System.Text.Json.Serialization;
//using CommunityToolkit.Mvvm.ComponentModel;


//namespace AuvoMovies.Models
//{
//    public partial class Filme : ObservableObject
//    {

//        [ObservableProperty]
//        private bool adult;


//        [ObservableProperty]
//        private string backdrop_path;


//        [ObservableProperty]
//        private List<int> genre_ids;

//        [ObservableProperty]
//        private int id;

//        [ObservableProperty]
//        private string original_language;

//        [ObservableProperty]
//        private string original_title;

//        [ObservableProperty]
//        private string title;

//        [ObservableProperty]
//        private string overview;

//        [ObservableProperty]
//        private double popularity;

//        [ObservableProperty]
//        private string poster_path;

//        [ObservableProperty]
//        private string release_date;

//        [ObservableProperty]
//        private bool video;

//        [ObservableProperty]
//        private double vote_average;

//        [ObservableProperty]
//        private int vote_count;
//    }
//}
