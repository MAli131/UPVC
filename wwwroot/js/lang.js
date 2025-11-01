// lang.js - site-wide language toggle for Views
// Behavior: store chosen language in localStorage, set <html lang/dir>, toggle body.rtl,
// and dynamically load/unload the project's Bootstrap RTL stylesheet (if present).
(function () {
    const STORAGE_KEY = 'site-lang';
    const RTL_BOOTSTRAP_PATH = '/lib/bootstrap/dist/css/bootstrap.rtl.min.css';
    const RTL_LINK_DATA = 'data-bootstrap-rtl';

    function setHtmlLangDir(lang) {
        const html = document.documentElement;
        const body = document.body;
        if (lang === 'ar') {
            html.lang = 'ar';
            html.dir = 'rtl';
            body.classList.add('rtl');
        } else {
            html.lang = 'en';
            html.dir = 'ltr';
            body.classList.remove('rtl');
        }
    }

    function ensureRtlStyles(shouldLoad) {
        const existing = document.querySelector('link[' + RTL_LINK_DATA + ']');
        if (shouldLoad) {
            if (!existing) {
                const link = document.createElement('link');
                link.rel = 'stylesheet';
                link.href = RTL_BOOTSTRAP_PATH;
                link.setAttribute(RTL_LINK_DATA, 'true');
                // insert after other styles if possible
                document.head.appendChild(link);
            }
        } else {
            if (existing) existing.remove();
        }
    }

    function applyLang(lang) {
        setHtmlLangDir(lang);
        // load site-level RTL (bootstrap rtl provided in the project)
        ensureRtlStyles(lang === 'ar');
        try { localStorage.setItem(STORAGE_KEY, lang); } catch (e) { /* ignore */ }
        // sync radio inputs if present
        const en = document.getElementById('en-lang');
        const ar = document.getElementById('ar-lang');
        if (en) en.checked = (lang === 'en');
        if (ar) ar.checked = (lang === 'ar');
    }

    document.addEventListener('DOMContentLoaded', function () {
        // read saved language or fallback to html lang
        let saved = null;
        try { saved = localStorage.getItem(STORAGE_KEY); } catch (e) { saved = null; }
        const fallback = (document.documentElement.lang && document.documentElement.lang.toLowerCase() === 'ar') ? 'ar' : 'en';
        const initial = saved || fallback;
        applyLang(initial);

        const en = document.getElementById('en-lang');
        const ar = document.getElementById('ar-lang');
        if (en) en.addEventListener('change', function () { if (en.checked) applyLang('en'); });
        if (ar) ar.addEventListener('change', function () { if (ar.checked) applyLang('ar'); });
    });
})();
// lang.js - simple language toggle (EN / AR)
(function () {
    const RTL_CSS = '/css/style-rtl.css';
    const STORAGE_KEY = 'site-lang';

    function safeGet(id) { return document.getElementById(id); }

    function applyLang(lang) {
        const html = document.documentElement;
        const body = document.body;

        if (lang === 'ar') {
            html.lang = 'ar';
            html.dir = 'rtl';
            body.classList.add('rtl');

            if (!document.querySelector('link[data-rtl]')) {
                const link = document.createElement('link');
                link.rel = 'stylesheet';
                link.href = RTL_CSS;
                link.setAttribute('data-rtl', 'true');
                document.head.appendChild(link);
            }
        } else {
            html.lang = 'en';
            html.dir = 'ltr';
            body.classList.remove('rtl');

            const rtlLink = document.querySelector('link[data-rtl]');
            if (rtlLink) rtlLink.remove();
        }

        // sync radios if present
        const en = safeGet('en-lang');
        const ar = safeGet('ar-lang');
        if (lang === 'ar' && ar) ar.checked = true;
        if (lang === 'en' && en) en.checked = true;

        try { localStorage.setItem(STORAGE_KEY, lang); } catch (e) { /* ignore if storage disabled */ }
    }

    document.addEventListener('DOMContentLoaded', function () {
        const en = safeGet('en-lang');
        const ar = safeGet('ar-lang');

        // restore saved or fallback to html lang
        let saved = null;
        try { saved = localStorage.getItem(STORAGE_KEY); } catch (e) { saved = null; }
        const fallback = (document.documentElement.lang && document.documentElement.lang.toLowerCase() === 'ar') ? 'ar' : 'en';
        const initial = saved || fallback;
        applyLang(initial);

        if (en) en.addEventListener('change', function () { if (en.checked) applyLang('en'); });
        if (ar) ar.addEventListener('change', function () { if (ar.checked) applyLang('ar'); });
    });
})();
