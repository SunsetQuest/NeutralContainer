using NeutralContainer.Data;

namespace NeutralContainer.Utilities;

public static class UiText
{
    public static string ResolveDisplayName(ApplicationUser? user, string fallback)
    {
        if (user is null)
        {
            return fallback;
        }

        if (!string.IsNullOrWhiteSpace(user.DisplayName))
        {
            return user.DisplayName;
        }

        return string.IsNullOrWhiteSpace(user.UserName) ? fallback : user.UserName;
    }

    public static string ResolvePostTitle(Post? post, string fallback = "Untitled")
    {
        if (post is null || string.IsNullOrWhiteSpace(post.Title))
        {
            return fallback;
        }

        return post.Title;
    }

    public static string ResolveContextText(Post? post, string fallback = "No additional context provided.")
    {
        if (post is null || string.IsNullOrWhiteSpace(post.ContextText))
        {
            return fallback;
        }

        return post.ContextText;
    }

    public static string BuildPreview(string? text, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        var normalized = text.Trim();
        if (maxLength <= 0)
        {
            return string.Empty;
        }

        if (normalized.Length <= maxLength)
        {
            return normalized;
        }

        if (suffix.Length >= maxLength)
        {
            return normalized[..maxLength];
        }

        var trimmedLength = maxLength - suffix.Length;
        return $"{normalized[..trimmedLength]}{suffix}";
    }

    public static string? BuildYouTubeLink(Post post)
    {
        if (post.PostType != PostType.YouTubeBacked)
        {
            return null;
        }

        if (!string.IsNullOrWhiteSpace(post.YouTubeUrl))
        {
            return post.YouTubeUrl;
        }

        if (!string.IsNullOrWhiteSpace(post.YouTubeVideoId))
        {
            return $"https://youtu.be/{post.YouTubeVideoId}";
        }

        return null;
    }

    public static IEnumerable<string> DescribeFeedbackModes(FeedbackMode modes)
    {
        if (modes.HasFlag(FeedbackMode.PresenceOnly))
        {
            yield return "Presence-only";
        }

        if (modes.HasFlag(FeedbackMode.ReflectiveListening))
        {
            yield return "Reflective listening";
        }

        if (modes.HasFlag(FeedbackMode.ClarifyingQuestions))
        {
            yield return "Clarifying questions";
        }

        if (modes.HasFlag(FeedbackMode.SharePerspective))
        {
            yield return "Share your perspective";
        }

        if (modes.HasFlag(FeedbackMode.AdviceAllowed))
        {
            yield return "Suggestions/advice allowed";
        }

        if (modes.HasFlag(FeedbackMode.ResourcesAllowed))
        {
            yield return "Resources allowed";
        }
    }

    public static IEnumerable<string> DescribeAvoidanceModes(AvoidanceMode modes)
    {
        if (modes.HasFlag(AvoidanceMode.DiagnosingOrLabeling))
        {
            yield return "Diagnosing or labeling";
        }

        if (modes.HasFlag(AvoidanceMode.MoralizingOrShaming))
        {
            yield return "Moralizing or shaming";
        }

        if (modes.HasFlag(AvoidanceMode.PrescriptiveLanguage))
        {
            yield return "Prescriptive language";
        }

        if (modes.HasFlag(AvoidanceMode.MinimizingOrDismissing))
        {
            yield return "Minimizing or dismissing";
        }

        if (modes.HasFlag(AvoidanceMode.PushingTowardResolution))
        {
            yield return "Pushing toward resolution";
        }
    }

    public static IEnumerable<string> DescribeSensitivityFlags(SensitivityFlag flags)
    {
        if (flags.HasFlag(SensitivityFlag.ExtraGentleContainer))
        {
            yield return "Extra gentle container";
        }

        if (flags.HasFlag(SensitivityFlag.NoMentalHealthLabels))
        {
            yield return "No mental health labels";
        }

        if (flags.HasFlag(SensitivityFlag.NoRelationshipAdvice))
        {
            yield return "No relationship advice";
        }

        if (flags.HasFlag(SensitivityFlag.NoMedicalAdvice))
        {
            yield return "No medical advice";
        }
    }

    public static string DescribeVisibilityPolicy(VisibilityPolicy policy) => policy switch
    {
        VisibilityPolicy.PrivateOnly => "Private feedback only",
        VisibilityPolicy.PublicOnly => "Public feedback only",
        VisibilityPolicy.CommenterChoice => "Commenter chooses (with consent)",
        _ => "Unspecified"
    };

    public static string DescribeModerationLevel(ModerationLevel level) => level switch
    {
        ModerationLevel.High => "High (extra gentle)",
        ModerationLevel.Standard => "Standard",
        _ => "Standard"
    };
}
