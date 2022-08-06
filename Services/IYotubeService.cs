using FunyMovieBackend.DbContexts.Entities;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace FunyMovieBackend.Services
{
    public interface IMovieService
    {
        Task<Movie> GetMovieDetailAsync(string url);
    }

    public class YoutubeMovieService : IMovieService
    {
        public async Task<Movie> GetMovieDetailAsync(string url)
        {
            using (var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ""
            }))
            {
                var searchRequest = youtubeService.Videos.List("snippet");
                searchRequest.Id = "CzvQxQYKO88";
                var searchResponse = await searchRequest.ExecuteAsync();

                var youTubeVideo = searchResponse.Items.FirstOrDefault();
                if (youTubeVideo != null)
                {
                    return new Movie
                    {
                        LinkId = youTubeVideo.Id,
                        Description = youTubeVideo.Snippet.Description,
                        Title = youTubeVideo.Snippet.Title,
                        Link = url
                    };
                }
                return null;
            }
        }
    }
}
