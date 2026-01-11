(() => {
    const storageKey = "neutral-theme";
    const mediaQuery = window.matchMedia("(prefers-color-scheme: dark)");
    const selectSelector = "[data-theme-select]";

    const resolveTheme = (preference) => {
        if (preference === "light" || preference === "dark") {
            return preference;
        }
        return mediaQuery.matches ? "dark" : "light";
    };

    const applyTheme = (theme) => {
        document.documentElement.setAttribute("data-theme", theme);
        document.documentElement.style.colorScheme = theme;
        if (document.body) {
            document.body.setAttribute("data-theme", theme);
            document.body.style.colorScheme = theme;
        }
    };

    const syncSelects = (preference) => {
        document.querySelectorAll(selectSelector).forEach((element) => {
            if (element instanceof HTMLSelectElement) {
                element.value = preference;
            }
        });
    };

    const getPreference = () => localStorage.getItem(storageKey) ?? "system";

    const setPreference = (preference) => {
        if (!preference || preference === "system") {
            localStorage.removeItem(storageKey);
            const theme = resolveTheme("system");
            applyTheme(theme);
            syncSelects("system");
            return "system";
        }

        localStorage.setItem(storageKey, preference);
        applyTheme(preference);
        syncSelects(preference);
        return preference;
    };

    const refresh = () => {
        const preference = getPreference();
        applyTheme(resolveTheme(preference));
        syncSelects(preference);
        return preference;
    };

    const ensureTheme = () => {
        const preference = getPreference();
        const theme = resolveTheme(preference);
        const htmlTheme = document.documentElement.getAttribute("data-theme");
        const bodyTheme = document.body ? document.body.getAttribute("data-theme") : null;

        if (htmlTheme !== theme || (document.body && bodyTheme !== theme)) {
            applyTheme(theme);
        }

        syncSelects(preference);
    };

    const init = () => {
        const preference = getPreference();
        applyTheme(resolveTheme(preference));
        syncSelects(preference);
        mediaQuery.addEventListener("change", () => {
            if (getPreference() === "system") {
                applyTheme(resolveTheme("system"));
            }
        });
        return preference;
    };

    window.neutralTheme = {
        init,
        refresh,
        getPreference,
        setPreference,
    };

    let bodyObserver;

    const observeBody = () => {
        if (!document.body) {
            return;
        }

        if (bodyObserver) {
            bodyObserver.disconnect();
        }

        bodyObserver = new MutationObserver(() => {
            if (document.body && document.body.getAttribute("data-theme") !== resolveTheme(getPreference())) {
                ensureTheme();
            }
        });

        bodyObserver.observe(document.body, {
            attributes: true,
            attributeFilter: ["data-theme", "style"],
        });
    };

    const rootObserver = new MutationObserver(() => {
        ensureTheme();
        observeBody();
    });

    const startObservers = () => {
        rootObserver.observe(document.documentElement, {
            attributes: true,
            attributeFilter: ["data-theme", "style"],
            childList: true,
        });
        observeBody();
    };

    const refreshAfterNavigation = () => {
        ensureTheme();
    };

    document.addEventListener("change", (event) => {
        const target = event.target;
        if (target instanceof HTMLSelectElement && target.matches(selectSelector)) {
            setPreference(target.value);
        }
    });

    document.addEventListener("enhancedload", refreshAfterNavigation);
    window.addEventListener("pageshow", refreshAfterNavigation);

    init();
    startObservers();
})();
