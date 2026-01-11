(() => {
    const storageKey = "neutral-theme";
    const mediaQuery = window.matchMedia("(prefers-color-scheme: dark)");

    const resolveTheme = (preference) => {
        if (preference === "light" || preference === "dark") {
            return preference;
        }
        return mediaQuery.matches ? "dark" : "light";
    };

    const applyTheme = (theme) => {
        document.documentElement.setAttribute("data-theme", theme);
        document.documentElement.style.colorScheme = theme;
    };

    const getPreference = () => localStorage.getItem(storageKey) ?? "system";

    const setPreference = (preference) => {
        if (!preference || preference === "system") {
            localStorage.removeItem(storageKey);
            const theme = resolveTheme("system");
            applyTheme(theme);
            return "system";
        }

        localStorage.setItem(storageKey, preference);
        applyTheme(preference);
        return preference;
    };

    const init = () => {
        const preference = getPreference();
        applyTheme(resolveTheme(preference));
        mediaQuery.addEventListener("change", () => {
            if (getPreference() === "system") {
                applyTheme(resolveTheme("system"));
            }
        });
        return preference;
    };

    window.neutralTheme = {
        init,
        getPreference,
        setPreference,
    };

    init();
})();
