using System;

namespace NeutralContainer.Data;

[Flags]
public enum FeedbackMode
{
    None = 0,
    PresenceOnly = 1,
    ReflectiveListening = 2,
    ClarifyingQuestions = 4,
    SharePerspective = 8,
    AdviceAllowed = 16,
    ResourcesAllowed = 32
}

[Flags]
public enum AvoidanceMode
{
    None = 0,
    DiagnosingOrLabeling = 1,
    MoralizingOrShaming = 2,
    PrescriptiveLanguage = 4,
    MinimizingOrDismissing = 8,
    PushingTowardResolution = 16
}

[Flags]
public enum SensitivityFlag
{
    None = 0,
    ExtraGentleContainer = 1,
    NoMentalHealthLabels = 2,
    NoRelationshipAdvice = 4,
    NoMedicalAdvice = 8
}

public enum VisibilityPolicy
{
    PrivateOnly = 0,
    PublicOnly = 1,
    CommenterChoice = 2
}

public enum ModerationLevel
{
    Standard = 0,
    High = 1
}
