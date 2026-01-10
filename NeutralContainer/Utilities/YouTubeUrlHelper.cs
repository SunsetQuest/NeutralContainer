using Microsoft.AspNetCore.WebUtilities;

namespace NeutralContainer.Utilities;

public static class YouTubeUrlHelper
{
    public static bool TryExtractVideoId(string? url, out string? videoId)
    {
        videoId = null;

        if (string.IsNullOrWhiteSpace(url))
        {
            return false;
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            return false;
        }

        var host = uri.Host.ToLowerInvariant();
        if (host is "youtu.be")
        {
            var candidate = uri.AbsolutePath.Trim('/');
            if (!string.IsNullOrWhiteSpace(candidate))
            {
                videoId = candidate;
                return true;
            }

            return false;
        }

        if (host is "youtube.com" or "www.youtube.com" or "m.youtube.com" or "music.youtube.com" or "youtube-nocookie.com" ||
            host.EndsWith(".youtube.com", StringComparison.OrdinalIgnoreCase))
        {
            if (uri.AbsolutePath.Equals("/watch", StringComparison.OrdinalIgnoreCase))
            {
                var query = QueryHelpers.ParseQuery(uri.Query);
                if (query.TryGetValue("v", out var values))
                {
                    var candidate = values.FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(candidate))
                    {
                        videoId = candidate;
                        return true;
                    }
                }
            }

            if (TryGetVideoIdFromPath(uri.AbsolutePath, "/embed/", out var embeddedId) ||
                TryGetVideoIdFromPath(uri.AbsolutePath, "/shorts/", out embeddedId) ||
                TryGetVideoIdFromPath(uri.AbsolutePath, "/live/", out embeddedId))
            {
                videoId = embeddedId;
                return true;
            }
        }

        return false;
    }

    private static bool TryGetVideoIdFromPath(string path, string prefix, out string? videoId)
    {
        videoId = null;

        if (!path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var candidate = path[prefix.Length..].Trim('/');
        if (string.IsNullOrWhiteSpace(candidate))
        {
            return false;
        }

        videoId = candidate;
        return true;
    }
}
