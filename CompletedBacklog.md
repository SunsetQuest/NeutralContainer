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

### Epic 7 — Non-functional hardening (privacy, abuse prevention, observability)

4. **User story:** As the system, I keep infrastructure secrets (like the SQL Server host/IP) out of source control while still allowing local development.

   * **Acceptance criteria**
     * Given production deployments, when the app starts, then the SQL connection string is sourced from environment variables (not committed to git).
     * Given local development, when appsettings.Development.json is present, then the app can connect using the dev connection string without exposing production values.
     * Given the connection string is missing, when the app starts, then it fails fast with an actionable error message pointing to how to configure it.

**Completed changes**
* Removed the production connection string from `appsettings.json` so production requires environment variables.
* Added a development-only connection string to `appsettings.Development.json`.
* Updated the connection-string error message in `Program.cs` to point to environment configuration.
