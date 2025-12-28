# NeutralContainer

NeutralContainer is an early-stage prototype for a “neutral container”: a space where people can articulate complex, unresolved, or hard-to-classify experiences **without being forced into a narrative**, assigned a diagnosis, or pushed toward resolution.

The core idea is simple:
- People should be able to speak about difficult or nuanced experiences *calmly*.
- Listeners should be able to stay *present* without immediately classifying, correcting, rescuing, or moralizing.
- Ambiguity should be allowed—at least long enough to understand what someone actually means.

> This repo currently contains a prototype website implementation and planning artifacts. See `Plan.md`, `BacklogAndWireframes.md`, and `CompletedBacklog.md` for ongoing work tracking.

---

## What “Neutral Container” Means

Many spaces implicitly demand a “box” before you’re allowed to speak:
- “This is a tragedy” (and you must perform distress),
- “This is growth” (and you must perform resilience),
- “This is trauma/diagnosis” (and you must fit a known template),
- or “don’t talk about it”.

NeutralContainer is designed for the missing middle: experiences that are real, complex, and meaningful, but not necessarily dramatic, pathological, inspirational, or neatly resolved.

---

## Design Principles

### 1) Postpone judgment
A core interaction goal is to create a short window where responders **do not** immediately categorize:
- healthy/unhealthy
- safe/dangerous
- okay/not okay
- fixable/broken
- victim/survivor

Instead, responders first learn what the author *means*.

### 2) Author-defined intent (“What kind of response do you want?”)
Posts should be able to declare intent up front (examples):
- **Witness only** (no advice, no reframing)
- **Questions welcome** (help me clarify, don’t solve)
- **Perspective welcome** (share your view, lightly)
- **Advice welcome** (practical suggestions)
- **Resource suggestions** (books, tools, referrals—without diagnosis)

This prevents “helpfulness” from becoming a form of misattunement.

### 3) Neutral language by default
The platform should support writing that is:
- matter-of-fact,
- non-performative,
- not forced into positivity or despair,
- and not automatically treated as a crisis signal.

### 4) Context is optional, not demanded
People may share in fragments. The system should not force backstory, conclusions, or a “lesson learned.”

### 5) Presence over policing
Moderation and community norms should prioritize:
- being with what is said,
- clarifying meaning,
- respecting stated intent,
over instant correction, diagnosis, or “resolution pressure.”

---

## What This Project Is / Is Not

### This is:
- A product concept + prototype site exploring new interaction norms for nuanced personal sharing.
- A lightweight “container” for expression that does not demand immediate classification.

### This is not:
- Therapy, coaching, crisis support, or a substitute for professional care.
- A diagnostic or clinical labeling system.
- A place where responders are expected to “fix” other users.

---

## Safety, Boundaries, and Responsible Use

NeutralContainer aims to hold ambiguity, but it does **not** ignore safety.

Recommended platform boundaries:
- Clear messaging that this is **not crisis support**.
- Crisis resources surfaced when users indicate immediate danger or intent to self-harm.
- Community rules prohibiting harassment, coercion, and unsolicited diagnosis.
- Reporting tools and moderation workflows to address abuse.

If you are implementing these features, treat safety work as first-class product scope.

---

## Current Status

NeutralContainer is currently in an **early prototype / concept validation** stage.

Planning docs in this repo:
- `Plan.md`
- `BacklogAndWireframes.md`
- `CompletedBacklog.md`

---

## Repository Notes

From the repository root, NeutralContainer appears to include a web front-end and a C# project/solution structure:
- `NeutralContainer/` (project folder)
- `NeutralContainer.slnx` (solution entry point)

If you are onboarding:
- Start by reading `Plan.md`.
- Then review backlog/wireframes to understand intended user flows and feature sequencing.

---

## Local Development (High-level)

Because this repository is still evolving, local setup may change.

Typical workflow:
1. Clone the repo.
2. Open `NeutralContainer.slnx` in Visual Studio (or your preferred .NET-capable IDE).
3. Run the web project from the IDE run profile.

If the prototype is purely static in your current branch, you can also serve the site via a simple local static server. Prefer the method documented in `Plan.md` once finalized.

---

## Contributing

Contributions are welcome, especially in these areas:
- Interaction design that supports “presence without labeling”
- UX patterns for “intent selection” (witness-only, questions, advice, etc.)
- Moderation and safety workflows that avoid over-pathologizing
- Copywriting that preserves neutrality while remaining humane
- Accessibility and respectful-by-default UI

Suggested process:
1. Open an issue describing the change.
2. Link it to the relevant backlog item (or propose a new one).
3. Submit a PR with a short rationale and any screenshots/wireframe updates.

---

## Acknowledgements

This project is based on the concept of creating a container where people can express complex experiences without being corrected, diagnosed, or pushed toward premature resolution.

---

## License

Add a LICENSE file to clarify usage and contributions. Until then, treat the project as “all rights reserved” by default.
