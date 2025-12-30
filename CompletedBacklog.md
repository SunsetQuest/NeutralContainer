## Completed backlog items

### Epic 1 — Identity, access control, and roles

1. **User story:** As a visitor, I can register and log in so I can access posts and participate.

   * **Acceptance criteria**
     * Given I am not authenticated, when I attempt to open any post URL, then I am redirected to login.
     * Given I register with email + password, when I confirm registration, then I can log in and see the app shell navigation.
     * Given I log out, when I revisit a protected page, then I am prompted to log in again.

**Completed changes**
* Added an authenticated post route placeholder so `/posts/{postId}` redirects to login for anonymous users.
* Disabled the confirmed-account requirement so newly registered users can sign in immediately.

2. **User story:** As the system, I enforce roles (Creator/Commenter/Admin) to control page access and actions.

   * **Acceptance criteria**
     * Given I am a standard user, when I attempt to open the Admin Queue, then I receive an access denied message.
     * Given I am an Admin/Moderator, when I open the Admin Queue, then I can view held/flagged items and act on them.
     * Given I am the post’s creator, when I open the Creator Inbox for that post, then I can see private and public comments for that post.

**Completed changes**
* Added role definitions and seeded Identity roles on startup.
* Assigned default Creator and Commenter roles at registration.
* Added protected Admin Queue and Creator Inbox pages with navigation links.
* Updated unauthorized routing to show an access denied message to signed-in users.

3. **User story:** As a user, I can manage my account basics (display name, password) so my identity is consistent.

   * **Acceptance criteria**
     * Given I change my display name, when I save, then future posts/comments show the updated display name.
     * Given I change my password, when I log in next time, then the new password works and the old password does not.

**Completed changes**
* Added a display name field to the user profile, registration, and external login flows.
* Stored display names on the user model and exposed them as auth claims for use in navigation and future content.
* Kept the existing password management screen in the account area for updating credentials.

### Epic 2 — Create post with YouTube embed + Response Agreement

4. **User story:** As a creator, I can create a post by pasting a YouTube URL so the app stores the VideoId and renders an embed.

   * **Acceptance criteria**
     * Given I paste a valid youtu.be or youtube.com URL, when I submit/leave the field, then the system extracts/stores VideoId and shows an embed preview.
     * Given I paste an invalid URL, when I submit, then I see a validation error and cannot publish.
     * Given I publish successfully, when I open the post view, then the embedded player loads using the stored VideoId.

**Completed changes**
* Added a Create Post page with URL validation, live YouTube preview, and publish workflow.
* Stored posts with the extracted VideoId and original URL in the database.
* Updated the post view to load stored posts and render the embedded player.
* Added navigation for authenticated users to create new posts.

5. **User story:** As a creator, I can set post metadata (title optional, context prompt optional) to frame the share without forcing narrative.

   * **Acceptance criteria**
     * Given I leave Title blank, when I publish, then the post is created and displays a default title (e.g., “Untitled”).
     * Given I enter Context text, when I publish, then it displays on the post view page.

**Completed changes**
* Added optional title and context fields to posts and the create form.
* Displayed post titles with an "Untitled" fallback and surfaced context text on the post view.

6. **User story:** As a creator, I can configure a structured Response Agreement (what I’m looking for + what to avoid) so moderation can enforce it.

   * **Acceptance criteria**
     * Given the “What I’m looking for” list, when I select one or more options, then they are saved and displayed prominently on the post view page.
     * Given “Suggestions/advice allowed” is not selected, when a comment contains prescriptive advice with high confidence, then the system can Hold or Reject per moderation rules.
     * Given I add optional custom rules text, when I publish, then it is displayed alongside structured options.

**Completed changes**
* Added structured Response Agreement selections (feedback modes, avoid list, sensitivity flags, custom rules) to the create post flow.
* Persisted Response Agreement fields on posts and render them in a dedicated card on the post view page.

7. **User story:** As a creator, I can set visibility policy (Private only / Public only / Commenter chooses) so comment visibility is governed.

   * **Acceptance criteria**
     * Given Private only, when a commenter composes a comment, then there is no option to make it public.
     * Given Public only, when a commenter composes a comment, then the comment will be visible publicly upon approval.
     * Given Commenter chooses, when a commenter selects Public, then the system records commenter consent for public display and never makes it public without that consent.

**Completed changes**
* Added visibility policy selection to the create post form and stored the choice on the post.
* Displayed the selected visibility policy on the post view page for reference.

8. **User story:** As a creator, I can choose moderation strictness (Standard/High) so the container can be “extra gentle.”

   * **Acceptance criteria**
     * Given High, when a comment is borderline on tone thresholds, then the system more often Holds rather than Approves (vs Standard).
     * Given I view the post, when I open the Response Agreement, then the chosen strictness is visible (at least to the creator; optionally to everyone).

**Completed changes**
* Added moderation strictness selection to the create post form with a default of Standard.
* Surface the chosen moderation level in the Response Agreement details on the post view page.

### Epic 3 — Post viewing and comment submission (text-only MVP)

1. **User story:** As a logged-in commenter, I can view a post (video + rules) so I understand the container before responding.

   * **Acceptance criteria**
     * Given I open a post, when the page loads, then the Response Agreement card is visible above the comment composer.
     * Given public comments are enabled, when the page loads, then approved public comments are visible in a list.
     * Given public comments are not enabled, when the page loads, then no public comments section is shown (or it shows “Public comments are disabled”).

**Completed changes**
* Added a comment composer to the post view below the Response Agreement.
* Rendered a public comments section only when the creator allows public visibility, with a disabled notice otherwise.

2. **User story:** As a commenter, I can write and submit a text-only comment that includes a visibility choice if allowed.

   * **Acceptance criteria**
     * Given the creator’s policy is Commenter chooses, when I submit, then my selected visibility is stored with the comment.
     * Given I submit an empty or too-short comment (configurable), when I submit, then I see a validation error and nothing is saved.
     * Given I submit a valid comment, when moderation returns Approved, then I see confirmation and (if public) the comment appears in the public list.

**Completed changes**
* Created comment storage with visibility, consent, status, and timestamps.
* Added comment submission with validation, visibility selection, and success confirmation.
* Displayed newly approved public comments in the post view list.

3. **User story:** As the system, I route each submitted comment into Approved / Held / Rejected with reason codes to support transparency.

   * **Acceptance criteria**
     * Given a comment is confidently compliant, when submitted, then status is Approved and it is visible per its visibility rules.
     * Given moderation is uncertain, when submitted, then status is Held and the commenter sees “Pending review.”
     * Given a comment clearly violates container rules, when submitted, then status is Rejected and the commenter sees rewrite guidance (no comment is posted).

**Completed changes**
* Added baseline moderation heuristics for harassment, advice, diagnosis, and shaming.
* Stored moderation decisions and reason codes alongside comments.
* Updated the comment composer to show approved, held, and rejected outcomes with guidance messaging.

4. **User story (optional MVP / Phase 2-ready):** As a commenter, I can run an “AI check” before submitting to see alignment/boundary issues.

   * **Acceptance criteria**
     * Given I click “Check my comment,” when results return, then I see detected issues mapped to the creator’s rules (e.g., advice not allowed).
     * Given I ignore the check and submit anyway, when submitted, then server-side enforcement still applies.

**Completed changes**
* Added a client-side “Check my comment” action that runs the moderation heuristics without submitting.
* Displayed alignment feedback and triggered rule summaries inline in the comment composer.
* Kept server-side moderation unchanged so submissions are still enforced at save time.

### Epic 4 — Moderation v1 (automated checks + decisioning + logs)

1. **User story:** As the system, I run baseline heuristics (spam/rate limits/profanity) before deeper classification.

   * **Acceptance criteria**
     * Given a user exceeds rate limits, when submitting, then the submission is blocked with a clear error message.
     * Given spam heuristics trigger, when submitting, then comment is Held or Rejected (configurable) with “Spam suspected” reason.

**Completed changes**
* Added per-user rate limiting to comment submission with a clear feedback message.
* Expanded moderation heuristics to flag spam-like content and profanity with reason codes.
* Tuned spam severity based on moderation level to allow stricter rejection in extra gentle containers.

2. **User story:** As the system, I classify likely violations (disrespect/harassment, diagnosing/labeling, unsolicited advice, moralizing/shaming/minimizing).

   * **Acceptance criteria**
     * Given advice is disabled for the post, when a comment contains prescriptive advice with high confidence, then it is Held or Rejected based on configured thresholds.
     * Given advice is enabled, when advice is detected, then it does not cause rejection by itself (though other violations still apply).
     * Given the post has “No mental health labels,” when a comment contains diagnostic language, then it is Held or Rejected and the reason references that setting.

**Completed changes**
* Expanded moderation heuristics to detect minimizing/dismissing language and pushing toward resolution when the creator asked to avoid it.
* Updated diagnostic-language reasons to reference the “No mental health labels” sensitivity toggle when enabled.

### Epic 5 — Creator inbox (review, triage, and controls)

1. **User story:** As a creator, I can view an inbox grouped by status so I can triage quickly.

   * **Acceptance criteria**
     * Given I open Creator Inbox, when the page loads, then I see tabs/counters for Approved, Held, Rejected, Flagged.
     * Given I filter to a specific post, when selected, then the list updates to only that post’s comments.

**Completed changes**
* Built the Creator Inbox view to load comments for the creator’s posts.
* Added status tabs with counts, plus a post filter dropdown.
* Listed comment details (post, commenter, visibility, status, submitted date, preview) in a responsive table.

2. **User story:** As a creator, I can approve or hide comments to manage what is visible and what stays private.

   * **Acceptance criteria**
     * Given a comment is Held, when I click Approve, then status becomes Approved and it becomes visible per visibility rules.
     * Given a comment is Approved, when I click Hide, then it is removed from public display (if public) and remains visible in creator inbox with status Hidden (or Approved + Hidden flag).

**Completed changes**
* Added per-comment actions in the Creator Inbox to approve held feedback and hide/unhide approved comments.
* Stored a hidden flag on comments and excluded hidden public feedback from the post view.

3. **User story:** As a creator, I can block/report a user or a specific comment to protect my container.

   * **Acceptance criteria**
     * Given I click Block user from a comment, when confirmed, then that user can no longer comment on my posts.
     * Given I report a comment, when submitted, then a Report record is created and appears in the Admin Queue.

**Completed changes**
* Added creator-level block records and enforced them in the post comment composer.
* Added comment reporting from the Creator Inbox, flagging comments and persisting reports.
* Expanded the Admin Queue to list open reports with resolve actions.

4. **User story:** As a creator, I can highlight approved public comments so visitors see standout feedback first.

   * **Acceptance criteria**
     * Given a comment is approved and public, when I click Highlight, then it appears in a highlighted section on the post view.
     * Given I unhighlight a comment, when the post view loads, then it appears only in the standard public comment list.

**Completed changes**
* Added a highlight flag to comments with Creator Inbox actions to highlight or unhighlight public, approved feedback.
* Rendered highlighted public comments in a dedicated section on the post view, with badges to call them out.
* Ensured hidden or rejected comments clear any highlight state.

### Epic 6 — Reporting and Admin Queue (held/flagged/reports/users)

1. **User story:** As an admin, I can see a queue of Held comments and decide Approve/Reject.

   * **Acceptance criteria**
     * Given I open Admin Queue → Held, when I select an item, then I see comment text, post context, Response Agreement, and triggered reasons.
     * Given I approve, when saved, then the comment becomes Approved and is visible per visibility rules.
     * Given I reject, when saved, then the comment is set to Rejected and (optionally) rewrite guidance is stored/shown.

**Completed changes**
* Added a Held comments tab to the Admin Queue with a selectable list and detailed review panel.
* Surfaced post context, Response Agreement details, and moderation reasons for held comments.
* Added approve/reject actions that update comment moderation status and refresh the queue.

2. **User story:** As an admin, I can review Flagged items and user reports to take action.

   * **Acceptance criteria**
     * Given a report exists, when I open it, then I can see reporter, reason, and linked comment.
     * Given I mark a report “Resolved,” when saved, then it no longer appears in the default open queue.

**Completed changes**
* Added a flagged comments tab in the Admin Queue with a review panel and approve/reject actions.
* Expanded the reports view to include a selectable detail panel with reporter, reason, and linked comment context.
* Kept report resolution actions and queue refresh behavior aligned with the new detail workflow.

3. **User story:** As an admin, I can suspend a user to limit abuse/spam.

   * **Acceptance criteria**
     * Given I suspend a user, when they attempt to comment, then the system blocks the action and shows a suspension message.
     * Given a user is suspended, when they attempt to log in, then access is denied (configurable) or limited to read-only pages.

**Completed changes**
* Added suspension fields to the user profile and surfaced a Users tab in the Admin Queue with suspend/unsuspend actions.
* Blocked suspended accounts from logging in and from submitting comments, with clear suspension messaging in the composer.
* Captured suspension reasons for display and moderation tracking.

### Epic 7 — Non-functional hardening (privacy, abuse prevention, observability)

1. **User story:** As the system, I enforce privacy rules for private comments (creator + commenter + admins).

   * **Acceptance criteria**
     * Given a private comment, when a different authenticated user views the post, then they cannot access that comment via UI or direct URL.
     * Given an admin views moderation detail, when they open the item, then they can see private comments for enforcement purposes.

**Completed changes**
* Added a "Your private comments" panel on post views that only surfaces private comments created by the current commenter.
* Filtered the private-comment panel to exclude rejected feedback and show moderation status badges for commenter visibility.

2. **User story:** As the system, I provide audit logging for moderation actions.

   * **Acceptance criteria**
     * Given any approve/reject/hide/suspend action, when executed, then a ModerationLog entry is created with actor + reason + timestamp.

**Completed changes**
* Added a ModerationLog entity and persistence table for moderation actions.
* Logged approve/reject actions in the Admin Queue, hide/unhide approvals in the Creator Inbox, and user suspension changes.

3. **User story:** As the system, I implement baseline anti-abuse controls.

   * **Acceptance criteria**
     * Given repeated rapid submissions, when thresholds are exceeded, then rate limiting is applied.
     * Given suspicious patterns (new accounts spamming), when detected, then content is Held by default.

**Completed changes**
* Kept the per-user comment rate limiter in place for rapid submissions.
* Added a new-account activity heuristic that forces comments into Held status when recent activity exceeds the threshold.

5. **User story:** As the system, I keep infrastructure secrets (like the SQL Server host/IP) out of source control while still allowing local development.

   * **Acceptance criteria**
     * Given production deployments, when the app starts, then the SQL connection string is sourced from environment variables (not committed to git).
     * Given local development, when appsettings.Development.json is present, then the app can connect using the dev connection string without exposing production values.
     * Given the connection string is missing, when the app starts, then it fails fast with an actionable error message pointing to how to configure it.

**Completed changes**
* Removed the production connection string from `appsettings.json` so production requires environment variables.
* Added a development-only connection string to `appsettings.Development.json`.
* Updated the connection-string error message in `Program.cs` to point to environment configuration.

### Epic 8 — Support text-only posts (alongside YouTube-backed posts)

1. **ID:** NC-E08-001 — As a creator, I can choose a post type (Text-only or YouTube-backed) when creating a post. (2025-03-20)

**Summary**
* Added post type selection to the create post flow and enforced YouTube URL validation only for YouTube-backed posts.
* Stored the post type in persistence while allowing text-only posts to omit YouTube data.
* Updated post viewing to render text-only posts without a video embed and show body text when available.

**Key files**
* `NeutralContainer/Components/Pages/CreatePost.razor`
* `NeutralContainer/Components/Pages/PostView.razor`
* `NeutralContainer/Data/Post.cs`
* `NeutralContainer/Data/PostType.cs`

**Migrations**
* `20250320000000_AddPostTypeAndOptionalVideoFields`

**Notable decisions**
* Defaulted existing posts to YouTube-backed via the `PostType` column default.

2. **ID:** NC-E08-002 — As a viewer, I can view a post whether it is Text-only or YouTube-backed. (2025-12-30)

**Summary**
* Made text-only post bodies display prominently on the post view without a video embed.
* Labeled YouTube supporting text explicitly when provided.

**Key files**
* `NeutralContainer/Components/Pages/PostView.razor`

**Migrations**
* None

**Notable decisions**
* Kept the empty-state text for text-only posts to avoid blank views.
