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
